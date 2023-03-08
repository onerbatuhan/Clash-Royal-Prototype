using System.Collections.Generic;
using System.Linq;
using Teams.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Events
{
    public class AITargetEvent : MonoBehaviour
    {
        public GameObject FindClosestTargetObject(NavMeshAgent currentAgent)
        {
            float _minDistance = Mathf.Infinity;
            GameObject _closestTarget = null;
            Transform _targetTransform = null;
            
            foreach (GameObject target in TeamController.Instance.allPlayer)
            {
                if (currentAgent.gameObject.GetComponent<Teams.Manager.Team>().teamData.teamID != target.GetComponent<Teams.Manager.Team>().teamData.teamID)
                {
                    float distance = Vector3.Distance(currentAgent.gameObject.transform.position, target.transform.position);
                    if (distance < _minDistance)
                    {
                        _minDistance = distance;
                        _closestTarget = target.gameObject;
                    }
                }
               
            }
            
            // if (_closestTarget!=null && currentAgent.enabled)
            // {
            //     _targetTransform = _closestTarget.transform;
            // }
            
            return _closestTarget;
        }

        
        
        
    }
}
