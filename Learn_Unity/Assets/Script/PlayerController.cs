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

public class PlayerController : MonoBehaviour
{
    public Transform topLeftTarget;
    public Transform topRightTarget;
    public Transform bottomLeftTarget;
    public Transform bottomRightTarget;

    public int Speed = 10;
    public Vector3[] CheckPoints;

    private Transform currentTarget;
    private targetEnum nextTarget= targetEnum.topLeft;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = topRightTarget;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition= currentTarget.position;
        Vector3 moveDirection= targetPosition-transform.position;
        float distance = (moveDirection).magnitude;

        if (distance > 0.1f) {
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
            case targetEnum.topRight:
                currentTarget = topRightTarget;
                nextTarget = targetEnum.topLeft;
                break;
            case targetEnum.topLeft:
                currentTarget = topLeftTarget;
                nextTarget = targetEnum.bottomLeft;
                break;
            case targetEnum.bottomLeft:
                currentTarget = bottomLeftTarget;
                nextTarget = targetEnum.bottomRight;
                break;
            case targetEnum.bottomRight:
                currentTarget = bottomRightTarget;
                nextTarget = targetEnum.topRight;
                break;
            

        }
    }
}
