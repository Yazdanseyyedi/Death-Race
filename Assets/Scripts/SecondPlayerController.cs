using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerController : MonoBehaviour
{
    TopDownCarController topDownCarController;
    public EventSystemCustom eventSystem;

    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;


    public string[] prefabs;
    public string itemPrefab;

    public int pathCounter = 0;

    private void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        Vector2 inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            inputVector.x = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
            inputVector.y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Debug.Log("second player health: " + currentHealth);
            currentHealth -= 13;
            eventSystem.onRocketDamage.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            topDownCarController.LunchRocket();
        }
        topDownCarController.SetInputVector(inputVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Muddy"))
        {
            Debug.Log("car two in muddy part");
            topDownCarController.maxSpeed = 4;
            topDownCarController.accelerationFactor = 4;
        }

        if (collision.gameObject.CompareTag("Main"))
        {
            topDownCarController.maxSpeed = 10;
            topDownCarController.accelerationFactor = 10;
        }
        if (collision.gameObject.CompareTag("checkPath"))
        {
            pathCounter += 1;
            Debug.Log("checkpath enter...");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            itemPrefab = prefabs[GetRandomPrefabType(prefabs.Length)];
            if (itemPrefab == "rocket")
            {
                topDownCarController.ActiveCombo = "rocket";
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("rocket"))
        {
            Destroy(collision.gameObject);
            currentHealth -= 30;
            eventSystem.onRocketDamage.Invoke();
        }
    }
    int GetRandomPrefabType(int max)
    {
        return UnityEngine.Random.Range(0, max);
    }
}
