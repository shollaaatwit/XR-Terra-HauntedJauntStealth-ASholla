using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
  public Transform[] waypoints;
  private NavMeshAgent _agent;
  private int _currentIndex;

  private void Start()
  {
      _agent = GetComponent<NavMeshAgent>();
      _agent.SetDestination(waypoints[0].position);
      _currentIndex = 0;
  }

  private void Update()
  {
    if(_agent.remainingDistance < _agent.stoppingDistance)
    {
      
      _currentIndex++;
      if(_currentIndex == waypoints.Length)
      {
        _currentIndex = 0;
      }
        StartCoroutine(waitAndTurn());  
      }
  }
  private IEnumerator waitAndTurn()
  {
    yield return new WaitForSeconds(2f);
    _agent.SetDestination(waypoints[_currentIndex].position);
  }






}
