﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerController : MonoBehaviour
{
    TopDownCarController topDownCarController;
    public EventSystemCustom eventSystem;
    public GameObject explosionPrefab;

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
    int combo;

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
        int P2 = PlayerPrefs.GetInt("P2");
        if (P2 == 1)
        {
            topDownCarController.maxSpeed = 30;
            topDownCarController.accelerationFactor = 20;
        }
        if (P2 == 2)
        {
            topDownCarController.ActiveCombo = "mine";
            PlayerPrefs.SetInt("P2",0);
        }
        if (P2 == 3)
        {
            topDownCarController.ActiveCombo = "rocket";
            PlayerPrefs.SetInt("P2",0);
        }

        //Debug.Log(topDownCarController.maxSpeed);
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
            topDownCarController.LunchMine();

        }
        if (shieldActivate)
        {
            shieldObject.SetActive(true);
        }
        else
        {
            shieldObject.SetActive(false);
        }
        topDownCarController.SetInputVector(inputVector);
        if (currentHealth <= 0)
        {
            //Debug.Log("player two has died");
            //eventSystem.playeroneWine.Invoke();
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            FindObjectOfType<GameManager>().playerOneWin();
            FindObjectOfType<GameManager>().GameEnd();
            // Destroy(gameObject);
        }
        if (cycleCounter >= 5)
        {
            FindObjectOfType<GameManager>().playerTwoWin();
            FindObjectOfType<GameManager>().GameEnd();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Muddy"))
        {
            //Debug.Log("car two in muddy part");
            topDownCarController.maxSpeed = 4;
            topDownCarController.accelerationFactor = 4;
        }

        if (collision.gameObject.CompareTag("Main"))
        {
            try
            {
                combo = PlayerPrefs.GetInt("P2");
            }
            catch (System.Exception) { }
            if (combo == 1)
            {
                topDownCarController.maxSpeed = 20;
                topDownCarController.accelerationFactor = 15;
            }
            else
            {
                topDownCarController.maxSpeed = 10;
                topDownCarController.accelerationFactor = 10;
            }
        }
        if (collision.gameObject.CompareTag("checkPath"))
        {
            pathCounter += 1;
            Debug.Log("checkpath enter...");
        }
        if (collision.gameObject.CompareTag("mine"))
        {
            Destroy(collision.gameObject);
            if (!shieldActivate)
            {
                FindObjectOfType<AudioManager>().Play("mine");
                currentHealth -= 30;
                damage += 30;
            }
            else
            {
                shieldHealth -= 30;
                damage += 30;
                if (shieldHealth >= 0) return;
                currentHealth += shieldHealth;
                shieldActivate = false;
                FindObjectOfType<AudioManager>().Play("shieldBrok");
            }
            eventSystem.onRocketDamage.Invoke();
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            FindObjectOfType<AudioManager>().Play("box");
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("rocket"))
        {
            Destroy(collision.gameObject);
            if (!shieldActivate)
            {
                currentHealth -= 30;
                damage += 30;
            }
            else
            {
                shieldHealth -= 30;
                damage += 30;
                if (shieldHealth >= 0) return;
                currentHealth += shieldHealth;
                shieldActivate = false;
                FindObjectOfType<AudioManager>().Play("shieldBrok");
            }
            eventSystem.onRocketDamage.Invoke();
        }
    }
    int GetRandomPrefabType(int max)
    {
        return UnityEngine.Random.Range(0, max);
    }
}
