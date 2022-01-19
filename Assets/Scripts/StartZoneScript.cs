using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZoneScript : MonoBehaviour
{
    public EventSystemCustom eventSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("player 1");
            eventSystem.PlayerOneCycleEnter.Invoke();

        }
        if (collision.gameObject.CompareTag("Car2"))
        {
            Debug.Log("player 2");
            eventSystem.PlayerTwoCycleEnter.Invoke();
        }
    }
}
