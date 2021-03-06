using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAiHandler : MonoBehaviour
{
    public enum AIMode {followPlayer,followWaypoints };

    [Header("AI Settings")]
    public AIMode aiMode;

    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    carcontroller carController;
    
    void Awake()
    {
        carController = GetComponent<carcontroller>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;
        switch (aiMode)
        {
            case AIMode.followPlayer:       
            FollowPlayer();
          break;
        }
        inputVector.x = TurnTowardTarget();
        inputVector.y = 1.0f;

        carController.SetInputVector(inputVector);
    }

    void FollowPlayer()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (targetTransform != null)
            targetPosition = targetTransform.position; 
    }
        float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / 45.0f;

        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);
        return steerAmount;
    }

}
