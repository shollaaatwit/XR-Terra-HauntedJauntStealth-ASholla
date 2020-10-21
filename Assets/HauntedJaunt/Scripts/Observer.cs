using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding ending;

    private bool _isInRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            _isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            _isInRange = false;
        }
    }

    void Update()
    {
        if(_isInRange)
        {
            Vector3 direction = player.position - transform.position;

            direction.y += 1f;

            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.transform == player)
                {
                    ending.CaughtPlayer();
                }
            }
        }
    }
}
