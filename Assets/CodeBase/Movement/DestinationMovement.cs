using UnityEngine;
using UnityEngine.AI;

namespace Assets.CodeBase.ThiefStates
{
    public class DestinationMovement
    {
        private NavMeshAgent _agent;
        private Vector3 _destination;

        public DestinationMovement (NavMeshAgent agent, Vector3 destination)
        {
            _agent = agent;
            _destination = destination;
        }

        public void Move() 
        { 
            _agent.SetDestination(_destination);
        }
    }
}
