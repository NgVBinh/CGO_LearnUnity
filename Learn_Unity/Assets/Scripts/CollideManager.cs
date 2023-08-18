using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollideManager : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] private float damaged;
    [SerializeField] protected float fuel;
    [SerializeField] protected float capacity;
    [SerializeField] protected int lap;

    // Start is called before the first frame update
    void Start()
    {
        lap = 0;
        damaged = 0;
        fuel = 0;
        capacity = 100;
        rb=player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged >= 100)
        {
            SceneManager.LoadScene("Lesson6");
        }
        if (fuel > capacity)
        {
            fuel = capacity;
        }
        if (damaged < 0)
        {
            damaged = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("point1")  && fuel<capacity)
        {
            fuel += 30;
            
        }
        if (other.gameObject.CompareTag("point2") )
        {
            capacity += 10;
        }
        if (other.gameObject.CompareTag("point3") && damaged>0)
        {
            damaged -= 30;
            
        }
        if (other.gameObject.name== "Street_el1")
        {
            lap += 1;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("struggle"))
        {
            damaged += 20;
        }
        
    }
}
