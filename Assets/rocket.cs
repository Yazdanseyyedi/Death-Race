using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    //public greenTank greenTank;
    public Rigidbody2D car;
    public Vector3 moveDirection;

    public float rocketSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveDirection = new Vector3(car.GetRelativeVector(Vector2.up).x, car.GetRelativeVector(Vector2.up).y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * rocketSpeed;
    }
}
