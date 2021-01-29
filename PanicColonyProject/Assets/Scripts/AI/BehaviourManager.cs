using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [System.Serializable]
    public class BehaviourManager
    {
        public List<PathNode> m_path;
        [SerializeField]
        private float m_closeEnoughToTarget = 1;
        [SerializeField]
        private BehaviourState m_behaviourState;

        private int m_posInPath = 0;


        public void Run(AI_Enemy entity)
        {
            Vector3 moveToPosition = m_path[m_posInPath].position;
            entity.m_meshAgent.destination = moveToPosition;
            if (Vector3.Distance(entity.transform.position, moveToPosition) < m_closeEnoughToTarget)
            {
                m_posInPath++;
                if (m_posInPath > m_path.Count - 1)
                {
                    m_posInPath = 0;
                }
            }
        }
    }



    [System.Serializable]
    public sealed class PathNode
    {
        public Vector3 position;
    }
    public enum BehaviourState
    {
        stationary,
        patrol
    }

}
