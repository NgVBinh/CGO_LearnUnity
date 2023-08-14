using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   // [SerializeField] protected GameObject target;

    public Transform targetObject;
    public float smooth = 2f;
    public Vector3 cameraOffset; // Khoảng cách theo trục y và z

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newCameraPosition = targetObject.position + targetObject.forward * cameraOffset.z + targetObject.up * cameraOffset.y;
        Vector3 cameraPosition = Vector3.Lerp(transform.position, newCameraPosition, smooth*Time.deltaTime);
        transform.position = cameraPosition;

        transform.LookAt(targetObject);
    }
}
