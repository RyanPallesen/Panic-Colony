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
        [SerializeField]
        private AI_Stats m_stats; //initialized in editor
        [SerializeField]
        private BehaviourManager m_behaviourManager;

        [HideInInspector]
        public CharacterController m_characterController;
        public NavMeshAgent m_meshAgent;


        void Start()
        {
            m_characterController = GetComponent<CharacterController>();
            m_meshAgent = GetComponent<NavMeshAgent>();
        }



        void Update()
        {
            m_behaviourManager.Run(this);
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
        public delegate void HitEventHandler(int damage);
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
