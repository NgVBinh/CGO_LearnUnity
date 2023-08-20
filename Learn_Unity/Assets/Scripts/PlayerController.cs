using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

enum targetEnum
{
    topLeft,
    topRight,
    bottomLeft,
    bottomRight
}

enum DriveMode
{
    Automatic,
    Manual,
    PhysicalManual
}
public class PlayerController : MonoBehaviour
{
    public Transform topLeftTarget;
    public Transform topRightTarget;
    public Transform bottomLeftTarget;
    public Transform bottomRightTarget;

    public int Speed = 10;
    public float speedRotate = 100f;

    [SerializeField] private WheelCollider frontLeftWheel;
    [SerializeField] private WheelCollider frontRightWheel;
    [SerializeField] private WheelCollider rearLeftWheel;
    [SerializeField] private WheelCollider rearRightWheel;

    [SerializeField] private float maxSteerAngle = 30f;

    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float breakForce = 3000f;

    private Transform currentTarget;
    private targetEnum nextTarget= targetEnum.topLeft;

    private DriveMode mode= DriveMode.Manual;// trạng thái ban đầu xe chạy bằng điều khiển
    // Start is called before the first frame update
    void Start()
    {
        currentTarget = topLeftTarget;
    }

    // Update is called once per frame
    void Update()
    {
        SetMode();
        if (mode == DriveMode.Automatic)
        {
            AutoDrive();
        }
        if(mode == DriveMode.Manual)
        {
            Manual();
        }
        if(mode == DriveMode.PhysicalManual) { 
            PhysicalDrive();
        }
    }

    private void SetMode()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            mode = DriveMode.Automatic;
        }
        if (Input.GetKey(KeyCode.M))
        {
            mode = DriveMode.Manual;
        }
        if (Input.GetKey(KeyCode.P))
        {
            mode = DriveMode.PhysicalManual;
        }


    }
    private void AutoDrive()
    {
        Vector3 targetPosition = currentTarget.position;
        Vector3 moveDirection = targetPosition - transform.position;
        float distance = (moveDirection).magnitude;

        if (distance > 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, Speed * Time.deltaTime);
        }
        else
        {
            setNextTarget(nextTarget);
        }

        Vector3 direction = currentTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = targetRotation;
    }
    private void setNextTarget(targetEnum target)
    {
        switch (target)
        {
            case targetEnum.topLeft:
                currentTarget = topLeftTarget;
                nextTarget = targetEnum.topRight;
                break;
            case targetEnum.topRight:
                currentTarget = topRightTarget;
                nextTarget = targetEnum.bottomRight;
                break;
            case targetEnum.bottomRight:
                currentTarget = bottomRightTarget;
                nextTarget = targetEnum.bottomLeft;
                break;
            case targetEnum.bottomLeft:
                currentTarget = bottomLeftTarget;
                nextTarget = targetEnum.topLeft;
                break;
        }
    }

    private void Manual()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0f,0f, vertical)*Speed*Time.deltaTime; 
        transform.Translate(movement);

        float rotationAmount = horizontal * speedRotate * Time.deltaTime;
        // Tạo một vector quay quanh trục Y
        Vector3 rotationVector = new Vector3(0f, rotationAmount, 0f);

        // Áp dụng quay vào transform của đối tượng
        transform.Rotate(rotationVector);
    }
    private void PhysicalDrive()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

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
