﻿using System;
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
    public int shieldHealth;
    public bool shieldActivate;
    public GameObject shieldObject;
    
    public string[] prefabs;
    public string itemPrefab;

    public int pathCounter = 0;
    public int cycleCounter = 0;
    public int score = 0;
    public int damage = 0;

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
        if (shieldActivate)
        {
            shieldObject.SetActive(true);
        }
        else
        {
            shieldObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            topDownCarController.LunchRocket();
            topDownCarController.LunchMine();
        }
        topDownCarController.SetInputVector(inputVector);

        if (currentHealth <= 0)
        {
            Debug.Log("player one has died");
            eventSystem.playertwoWine.Invoke();
            FindObjectOfType<GameManager>().GameEnd();
            //Destroy(gameObject);
        }
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
        if (collision.gameObject.CompareTag("checkPath"))
        {
            pathCounter += 1;
        }
        if (collision.gameObject.CompareTag("mine"))
        {
            Destroy(collision.gameObject);
            if (!shieldActivate)
            {
                currentHealth -= 30;
            }
            else
            {
                shieldHealth -= 30;
                if (shieldHealth >= 0) return;
                currentHealth += shieldHealth;
                shieldActivate = false;
            }
            eventSystem.onRocketDamage.Invoke();

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            itemPrefab = prefabs[GetRandomPrefabType(prefabs.Length)];
            if (topDownCarController.ActiveCombo == "")
            {
                if (itemPrefab == "rocket")
                {
                    topDownCarController.ActiveCombo = "rocket";
                }
                if (itemPrefab == "mine")
                {
                    topDownCarController.ActiveCombo = "mine";
                }
            }
            
            if (itemPrefab == "shield")
            {
                //currentHealth += 40;
                shieldHealth = 40;
                eventSystem.onRocketDamage.Invoke();
                shieldActivate = true;
                //topDownCarController.ActiveCombo = "shield";
            }
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("second rocket"))
        {
            Destroy(collision.gameObject);
            if (!shieldActivate)
            {
                currentHealth -= 30;
            }
            else
            {
                shieldHealth -= 30;
                if (shieldHealth >= 0) return;
                currentHealth += shieldHealth;
                shieldActivate = false;
            }
            eventSystem.onRocketDamage.Invoke();

        }
    }
    int GetRandomPrefabType(int max)
    {
        return UnityEngine.Random.Range(0, max);
    }
}
