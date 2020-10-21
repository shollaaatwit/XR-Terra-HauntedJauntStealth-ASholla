using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 1f;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private Vector3 _movement;
    private Quaternion _rotation = Quaternion.identity;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //grabs horizontal and vertical directions

        bool hasHorizontalInput = !Mathf.Approximately(h, 0f);
        bool hasVerticalInput = !Mathf.Approximately(v, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        

        _movement.Set(h, 0, v);
        _movement.Normalize();

        Vector3 desiredForwardDirection = Vector3.RotateTowards(
            transform.forward, _movement, 
            turnSpeed * Time.deltaTime, 0);

        _rotation = Quaternion.LookRotation(desiredForwardDirection);

        _animator.SetBool("IsWalking", isWalking);
    }

    private void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * _animator.deltaPosition.magnitude);

        _rigidbody.MoveRotation(_rotation);
    }
}
