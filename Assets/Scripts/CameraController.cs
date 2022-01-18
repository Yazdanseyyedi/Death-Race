using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject car;
    void Update()
    {
        transform.position = new Vector3(car.transform.position.x, car.transform.position.y, transform.position.z);
    }
}
