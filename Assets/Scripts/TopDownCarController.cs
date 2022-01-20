using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 10.0f;
    public float turnFactor = 3f;
    public float maxSpeed = 10;

    public string ActiveCombo;

    public GameObject RocketObject;
    public GameObject RocketPrefab;
    GameObject Rocket;
    
    public GameObject MineObject;
    public GameObject MinePrefab;
    GameObject Mine;
    // Local variables
    float accelrationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    // Components
    Rigidbody2D carRigidbody2D;

    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveCombo != "")
        {
            if (ActiveCombo == "rocket")
            {
                RocketObject.SetActive(true);
            }
            if (ActiveCombo == "mine")
            {
                MineObject.SetActive(true);
            }
        }
        else
        {
            RocketObject.SetActive(false);
            MineObject.SetActive(false);
        }
    }

    public void LunchMine()
    {
        if (ActiveCombo == "mine")
        {
            RocketObject.SetActive(false);
            Mine = Instantiate(MinePrefab);
            Mine.transform.position = transform.position - new Vector3(3*carRigidbody2D.GetRelativeVector(Vector2.up).x, 3*carRigidbody2D.GetRelativeVector(Vector2.up).y, -1);
            ActiveCombo = "";
        }
    }
    public void LunchRocket()
    {
        if (ActiveCombo == "rocket")
        {
            RocketObject.SetActive(false);
            Rocket = Instantiate(RocketPrefab);
            Rocket.transform.position = transform.position + new Vector3(carRigidbody2D.GetRelativeVector(Vector2.up).x, carRigidbody2D.GetRelativeVector(Vector2.up).y, -1);
            ActiveCombo = "";
        }
    }
    // Frame-rate independent for physics calculations
    private void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed && accelrationInput > 0)
            return;

        if (velocityVsUp < -maxSpeed * 0.5f && accelrationInput < 0)
            return;

        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelrationInput > 0)
            return;

        if (accelrationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 2.0f, Time.fixedDeltaTime * 80);
        else carRigidbody2D.drag = 0;

        // Create a force for the engine
        Vector2 engineForceVector = transform.up * accelrationInput * accelerationFactor;

        // Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        // Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        // Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelrationInput = inputVector.y;
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;

    }
}
