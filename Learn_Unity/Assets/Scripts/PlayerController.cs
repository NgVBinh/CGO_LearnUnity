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
}
public class PlayerController : MonoBehaviour
{
    public Transform topLeftTarget;
    public Transform topRightTarget;
    public Transform bottomLeftTarget;
    public Transform bottomRightTarget;

    public int Speed = 10;
    public float speedRotate = 100f;
    public bool check;

    private Transform currentTarget;
    private targetEnum nextTarget= targetEnum.topLeft;

    private DriveMode mode= DriveMode.Manual;
    // Start is called before the first frame update
    void Start()
    {
        currentTarget = topLeftTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == DriveMode.Manual)
        {
            Manual();
        }
        else
        {
            AutoDrive();
        }
        SetMode();
    }

    private void SetMode()
    {
        
        if (check)
        {
            mode = DriveMode.Automatic;
        }
        else
        {
            mode = DriveMode.Manual;
        }

        check = Input.GetKey(KeyCode.A);
    }
    private void AutoDrive()
    {
        Vector3 targetPosition = currentTarget.position;
        Vector3 moveDirection = targetPosition - transform.position;
        float distance = (moveDirection).magnitude;

        if (distance > 0.1f)
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
}
