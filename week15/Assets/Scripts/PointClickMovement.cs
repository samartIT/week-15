﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


 [RequireComponent(typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float moveSpeed = 6.0f;
    public float rotSpeed = 15.0f;
    public float deceleration = 20f;
    public float targetBuffer = 1.5f;

    public CharacterController _charController;
    public Animator _animator;
    public float _curSpeed = 0f;
    public Vector3 _targetPos = Vector3.one;
    

    // Start is called before the first frame update
    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mousehit;
            if(Physics.Raycast(ray, out mousehit))
            {
                _targetPos = mousehit.point;
                _curSpeed = moveSpeed;
            }
        }

        if (_targetPos != Vector3.one)
        {
            Vector3 adjustedPos = new Vector3(_targetPos.x, transform.position.y, _targetPos.z);
            Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            movement = _curSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);
        }

        if(Vector3.Distance(_targetPos, transform.position) < targetBuffer)
        {
            _curSpeed -= deceleration * Time.deltaTime;
            if (_curSpeed <= 0)
                _targetPos = Vector3.one;

        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);
        movement *= Time.deltaTime;
        _charController.Move(movement);
        
    }
}
