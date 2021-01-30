using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    public sealed class AI_Enemy : MonoBehaviour
    {
        [Header("Entity Properties")]
        [SerializeField]
        private bool m_showPath = false;
        public BehaviourState AI_Type;
        public bool CanShoot = false;

        [Header("Projectile Settings")]
        public GameObject projectilePrefab;
        public float velocityMultiplier = 1;

        [SerializeField]
        private AI_Stats m_stats; //initialized in editor
        [SerializeField]
        private BehaviourManager m_behaviourProperties;

        [HideInInspector]
        public NavMeshAgent m_meshAgent;
        [HideInInspector]
        public Transform playerTransform;

        private GameObject storedProjectile;



        void Start()
        {
            m_meshAgent = GetComponent<NavMeshAgent>();

            playerTransform = FindObjectOfType<PlayerLocomotion>()?.transform;
            m_behaviourProperties.Initialize(this);
        }



        void Update()
        {
            m_behaviourProperties.Run(this);
            if (AI_Type == BehaviourState.Snatcher && Input.GetMouseButtonDown(0) && CanShoot)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit hitInfo);
                Vector3 alignedHitPoint = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z); // hitInfo.point.y = transform.position.y;
                Vector3 directionToShoot = alignedHitPoint - transform.position;
                directionToShoot.Normalize();
                FireProjectile(directionToShoot);
                GetComponentInChildren<Animator>().SetTrigger("Throw");

            }

            if (Input.GetKeyDown(KeyCode.E) && AI_Type != BehaviourState.Spinner)
            {
                CanShoot = !CanShoot;
            }
        }


        #region Collision
        private void OnTriggerEnter(Collider collision)
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                switch (AI_Type)
                {
                    case BehaviourState.Spinner:
                        if (CanShoot)
                        {
                            FireProjectile(transform.forward);
                        }
                        else
                        {
                            CanShoot = true;
                        }
                        break;
                    case BehaviourState.Smacker:
                        Vector3 directionToPlayer = playerTransform.position - transform.position;
                        directionToPlayer.Normalize();
                        FireProjectile(directionToPlayer); // needs fixing
                        break;
                    case BehaviourState.Snatcher:
                        if (!CanShoot)
                        {
                            DisableProjectile(projectile);
                            GetComponentInChildren<Animator>().SetTrigger("Catch");
                            CanShoot = true;
                        }
                        break;
                    case BehaviourState.idle:
                        break;
                }
                OnHit?.Invoke();
            }
        }


        #endregion

        #region Projectile Helper Methods
        private void FireProjectile(Vector3 directionToShoot)
        {
            storedProjectile.transform.position = transform.position;
            storedProjectile.GetComponent<Renderer>().enabled = true;
            storedProjectile.GetComponent<Projectile>().velocity = (directionToShoot * velocityMultiplier);
            Collider projCollider = storedProjectile.GetComponent<Collider>();
            foreach (var collider in GetComponents<Collider>())
            {
                if (projCollider != collider)
                {
                    Physics.IgnoreCollision(collider, projCollider);
                }
            }
            CanShoot = false;
            storedProjectile = null;
        }

        private void DisableProjectile(Projectile projectile)
        {
            projectile.GetComponent<Collider>().enabled = false;
            projectile.GetComponent<Projectile>().velocity = Vector3.zero;
            projectile.GetComponent<Renderer>().enabled = false;
            storedProjectile = projectile.gameObject;
        }
        #endregion

        #region Player Interactions

        /// <summary>
        /// Remove health from this entity and check if it should "die"
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(int amount)
        {
            m_stats.currentHealth -= amount;
            if (m_stats.currentHealth < 0)
            {
                //die
            }
        }
        #endregion


        #region UI/Audio Events
        public delegate void HitEventHandler();
        public event HitEventHandler OnHit;
        #endregion



        private void OnDrawGizmos()
        {
            if (m_showPath)
            {
                if (m_behaviourProperties.m_path.Count > 0)
                {
                    foreach (var node in m_behaviourProperties.m_path)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawSphere(node.position, .25f);
                    }
                }
            }
        }
    }
}
