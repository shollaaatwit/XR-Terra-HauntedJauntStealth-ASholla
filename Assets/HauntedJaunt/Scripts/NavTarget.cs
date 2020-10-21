using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTarget : MonoBehaviour
{

    public NavMeshAgent agent;

    void Update()
    {
        agent.SetDestination(transform.position);
    }

        
    
}
