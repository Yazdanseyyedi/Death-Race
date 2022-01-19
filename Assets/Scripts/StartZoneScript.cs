using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZoneScript : MonoBehaviour
{
    public EventSystemCustom eventSystem;
    public PlayerController pc;
    public SecondPlayerController spc;

    private void Start()
    {
        eventSystem.PlayerOneCycleEnter.Invoke();
        eventSystem.PlayerTwoCycleEnter.Invoke();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        { 
            if (pc.pathCounter == 3){
                pc.pathCounter = 0;
                eventSystem.PlayerOneCycleEnter.Invoke();
            }

        }
        if (collision.gameObject.CompareTag("Car2"))
        {
            if(spc.pathCounter == 3)
            {
                spc.pathCounter = 0;
                eventSystem.PlayerTwoCycleEnter.Invoke();
            }
        }
    }
}
