using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontLeftWheel;
    [SerializeField] private WheelCollider frontRightWheel;
    [SerializeField] private WheelCollider rearLeftWheel;
    [SerializeField] private WheelCollider rearRightWheel;

    [SerializeField] private float maxSteerAngle = 30f;

    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float breakForce = 3000f;
    public float horizontal;
    public float vertical;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");

        float steerAngle = horizontal * maxSteerAngle;
        float force = vertical * motorForce;

        frontLeftWheel.steerAngle = steerAngle;
        frontRightWheel.steerAngle = steerAngle;

        rearLeftWheel.motorTorque = force;
        rearRightWheel.motorTorque = force;

        if (Input.GetKey(KeyCode.Space))
        {
            rearRightWheel.brakeTorque = breakForce;
            rearLeftWheel.brakeTorque = breakForce;
        }
        else
        {
            rearRightWheel.brakeTorque = 0;
            rearLeftWheel.brakeTorque = 0;
        }
    }
}
