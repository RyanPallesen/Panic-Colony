using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

namespace Assets.Scripts.AI
{
    public sealed class AI_Enemy : MonoBehaviour
    {
        [Header("Entity Properties")]
        [SerializeField]
        private bool m_showPath = false;
        public BehaviourState AI_Type;
        public bool CanShoot = false;

        public Transform firePoint;

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



        Vector3[] corners;
        [SerializeField]
        int AimAssistRendererSteps;
        [SerializeField]
        LayerMask BallReclamationLayerMask;
        [SerializeField]
        LineRenderer lineRenderer;

        void Start()
        {
            m_meshAgent = GetComponent<NavMeshAgent>();

            lineRenderer = GetComponent<LineRenderer>();

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
                storedProjectile.GetComponent<Projectile>().lastAttachedAI = this.gameObject;
            }

            if (CanShoot)
            {
                lineRenderer.enabled = true;
                AimAssistRender();
            }
            else
            {
                lineRenderer.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                CanShoot = false;
            }
        }



        #region Collision
        private void OnTriggerEnter(Collider collision)
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null && projectile.lastAttachedAI != this.gameObject)
            {
                switch (AI_Type)
                {
                    case BehaviourState.Spinner:

                        FireProjectile(m_behaviourProperties.GetSpinnerDirection(transform));


                        break;
                    case BehaviourState.Smacker:
                        Vector3 directionToPlayer = playerTransform.position - transform.position;
                        directionToPlayer.Normalize();
                        FireProjectile(directionToPlayer); // needs fixing
                        break;
                    case BehaviourState.Snatcher:
                        if (!CanShoot && projectile.lastAttachedAI != this.gameObject)
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
            storedProjectile.transform.position = firePoint.position;
            storedProjectile.GetComponent<Renderer>().enabled = true;
            storedProjectile.GetComponent<Projectile>().velocity = (directionToShoot * velocityMultiplier);
            storedProjectile.GetComponent<Collider>().enabled = true;
            storedProjectile.GetComponent<Projectile>().ResetRicochets();

            CanShoot = false;
        }


        private void DisableProjectile(Projectile projectile)
        {
            projectile.GetComponent<Collider>().enabled = false;
            projectile.GetComponent<Projectile>().velocity = Vector3.zero;
            projectile.GetComponent<Renderer>().enabled = false;
            storedProjectile = projectile.gameObject;
        }
        public void AimAssistRender()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo, 100f, BallReclamationLayerMask);
            Vector3 alignedHitPoint = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);// hitInfo.point.y = transform.position.y;
            Vector3 directionToShoot = alignedHitPoint - transform.position;
            Vector3 origin = transform.position;

            corners = new Vector3[AimAssistRendererSteps + 1];

            for (int i = 0; i < AimAssistRendererSteps; i++)
            {
                corners[i] = origin;

                ray = new Ray(origin, directionToShoot);

                if (Physics.Raycast(ray, out RaycastHit hitInfoInner, 100f, BallReclamationLayerMask))
                {
                    origin = hitInfoInner.point;
                    directionToShoot = Vector3.Reflect(directionToShoot, hitInfoInner.normal);
                }
                else
                {
                    origin = origin + directionToShoot;
                }
            }

            lineRenderer.positionCount = corners.Length - 1;
            lineRenderer.SetPositions(corners);
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
