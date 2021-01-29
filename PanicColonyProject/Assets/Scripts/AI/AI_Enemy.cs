using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    public sealed class AI_Enemy : MonoBehaviour
    {
        [SerializeField]
        private bool m_showPath = false;

        public BehaviourState AI_Type;
        [SerializeField]
        private AI_Stats m_stats; //initialized in editor

        [HideInInspector]
        public CharacterController m_characterController;
        [HideInInspector]
        public NavMeshAgent m_meshAgent;

        public bool CanShoot = false;


        [HideInInspector]
        public Transform playerTransform;

        [SerializeField]
        private BehaviourManager m_behaviourManager;
        public GameObject projectilePrefab;
        public float velocityMultiplier = 1;
        void Start()
        {
            m_characterController = GetComponent<CharacterController>();
            m_meshAgent = GetComponent<NavMeshAgent>();

            playerTransform = FindObjectOfType<PlayerLocomotion>()?.transform;
            m_behaviourManager.Initialize(this);
        }



        void Update()
        {
            m_behaviourManager.Run(this);
            if (AI_Type == BehaviourState.Snatcher && Input.GetMouseButtonDown(0) && CanShoot)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit hitInfo);
                Vector3 alignedHitPoint = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z); // hitInfo.point.y = transform.position.y;
                Vector3 directionToShoot = alignedHitPoint - transform.position;
                directionToShoot.Normalize();
                FireProjectile(directionToShoot);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                CanShoot = !CanShoot;
            }
        }


        #region Collision
        private void OnCollisionEnter(Collision collision)
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
                        FireProjectile(directionToPlayer);
                        break;
                    case BehaviourState.Snatcher:
                        CanShoot = true;
                        break;
                    case BehaviourState.idle:
                        break;
                    default:
                        break;
                }
                //projectile.Reset
                OnHit?.Invoke();
                projectile.DestroyThis();
            }

        }
        #endregion

        private void FireProjectile(Vector3 directionToShoot)
        {
            GameObject firedProjectile = Instantiate(projectilePrefab, transform.position + directionToShoot, transform.rotation);
            firedProjectile.GetComponent<Projectile>().velocity = (directionToShoot * velocityMultiplier);
            CanShoot = false;   
        }

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
                foreach (var node in m_behaviourManager.m_path)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(node.position, .25f);
                }
            }
        }
    }
}
