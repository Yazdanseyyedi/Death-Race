using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TopDownCarController topDownCarController;
    public EventSystemCustom eventSystem;

    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;

    public string[] prefabs;
    public string itemPrefab;

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

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            inputVector.x = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            inputVector.y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 13;
            eventSystem.onRocketDamage.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            topDownCarController.LunchRocket();
        }
        topDownCarController.SetInputVector(inputVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Muddy"))
        {
            Debug.Log("car one in muddy part");
            topDownCarController.maxSpeed = 4;
            topDownCarController.accelerationFactor = 4;
        }

        if (collision.gameObject.CompareTag("Main"))
        {
            topDownCarController.maxSpeed = 10;
            topDownCarController.accelerationFactor = 10;
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
        
        if (collision.gameObject.CompareTag("second rocket"))
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
