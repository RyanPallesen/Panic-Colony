using System;
using System.Collections.Generic;
using System.IO;
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

        [Tooltip("The distance this entity should move onto the next node in the list")]
        private float m_closeEnoughToTarget = 1;

        private int m_posInPath = 0;

        [Header("Spinner Variables")]
        public float m_rotateSpeed;

        [Header("Snatcher Variables")]
        private int NDR = 0;

        public void Initialize(AI_Enemy entity)
        {

            entity.m_meshAgent.angularSpeed = 0; // standard speed is 120
            switch (entity.AI_Type)
            {
                case BehaviourState.Spinner:
                    break;
                case BehaviourState.Smacker:
                    break;
                case BehaviourState.Snatcher:
                    break;
                case BehaviourState.idle:
                    break;
                default:
                    break;
            }
        }


        public void Run(AI_Enemy entity)
        {
            Vector3 directionToPlayer = entity.playerTransform.position - entity.transform.position;
            Vector3 lookDirectionToPlayer = Vector3.RotateTowards(entity.transform.forward, directionToPlayer, 15 * Time.deltaTime, 0);

            switch (entity.AI_Type)
            {
                case BehaviourState.Spinner:
                    if (m_path.Count > 0)
                    {
                        MoveOnPath(entity);
                    }
                    entity.transform.Rotate(entity.transform.up, m_rotateSpeed);
                    break;

                case BehaviourState.Snatcher:
                    //rotate towards player, fires shot toward cursor
                    if (m_path.Count > 0)
                    {
                        MoveOnPath(entity);
                    }
                    if (!entity.CanShoot)
                    {
                        entity.transform.rotation = Quaternion.LookRotation(lookDirectionToPlayer);
                    }
                    else
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        Physics.Raycast(ray, out hit);
                        Vector3 lookPoint = hit.point - entity.transform.position;
                        Vector3 hitDirection = Vector3.RotateTowards(entity.transform.forward, lookPoint, 15 * Time.deltaTime, 0);
                        entity.transform.rotation = Quaternion.LookRotation(hitDirection);
                    }
                    break;

                case BehaviourState.Smacker:
                    if (m_path.Count > 0)
                    {
                        MoveOnPath(entity);
                    }
                    entity.transform.rotation = Quaternion.LookRotation(lookDirectionToPlayer);
                    break;
                case BehaviourState.idle:
                    if (m_path.Count > 0)
                    {
                        MoveOnPath(entity);
                    }
                    break;


                default:
                    break;
            }
        }

        private void MoveOnPath(AI_Enemy entity)
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
        Spinner, //direction of bot forward | does spin
        Smacker, //back at player
        Snatcher, //direction of mouse
        idle // does nothing - probably don't use
    }

}
