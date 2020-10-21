using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleRotater : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject gargoyle;
    public float axisY;
    Quaternion currentRotation;
    Vector3 currentEulerAngles;


    void Update()
    {
        axisY = Mathf.Lerp(-101, 100, Mathf.PingPong(Time.time, 1));
        currentEulerAngles += new Vector3(0, axisY, 0) * Time.deltaTime * rotationSpeed;
        currentRotation.eulerAngles = currentEulerAngles;
        gargoyle.transform.rotation = currentRotation;     
    }
}
