using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public int Speed = 10;
    public Vector3[] CheckPoints;

    [SerializeField] public float distance;

    public Vector3 moveDirection = Vector3.forward;
    [SerializeField] public int i = 0;

    private void Awake()
    {

        CheckPoints = new Vector3[4];
        CheckPoints[0].Set(16, 0, 48);
        CheckPoints[1].Set(-104, 0, 48);
        CheckPoints[2].Set(-104, 0, -60);
        CheckPoints[3].Set(16, 0, -60);
    }
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        distance = (transform.position - CheckPoints[i]).magnitude;

        if (i == 0) moveDirection.Set(0, 0, 1);
        else if (i == 1) moveDirection.Set(-1, 0, 0);
        else if (i == 2) moveDirection.Set(0, 0, -1);
        else moveDirection.Set(1, 0, 0);

        if (distance < 0.15)
        {
            i++;
            Debug.Log("Đã đến điểm " + i);
            transform.Rotate(0, -90, 0);
            if (i > 3) i = 0;
        }

        Vector3 newPosition = transform.position + moveDirection * Speed * Time.deltaTime;
        transform.position = newPosition;
    }
    
}
