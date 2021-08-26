using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carcontroller : MonoBehaviour
{
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;
    public float reverseSpeed = 0.5f;

    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

   public Rigidbody2D carRigidbody2D;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }

    void ApplyEngineForce()
    {
        //lasekee auton nopeuden
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //rajoittaa nopeuden eteen p‰in
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;
        //rajoittaa nopeuden taaksep‰in
        if (velocityVsUp < -maxSpeed * reverseSpeed && accelerationInput < 0)
            return;
        //rajoittaa sivusuuntausen nopeuden
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        //pys‰itt‰‰ auton kun kiihdytys lakkaa
        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

        //Luo voiman
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //lis‰‰ voiman ja tyˆnt‰‰ autoa eteen p‰in
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }
    void ApplySteering()
    {
        float minspeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minspeedBeforeAllowTurningFactor = Mathf.Clamp01(minspeedBeforeAllowTurningFactor);
        rotationAngle -= steeringInput * turnFactor * minspeedBeforeAllowTurningFactor;
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
        public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }
    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
        {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }
        //sivusuunnan m‰‰r‰ ett‰ rendaa j‰let
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;

        return false;
     }



}
