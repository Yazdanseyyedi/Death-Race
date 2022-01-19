using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlacer : MonoBehaviour
{
    public GameObject box;
    public EventSystemCustom eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem.PlayerOneCycleEnter.AddListener(box_gen);
        eventSystem.PlayerTwoCycleEnter.AddListener(box_gen);
    }

    public void box_gen()
    {
        Debug.Log("box generation called");
        GameObject go;
        go = Instantiate(box);
        go.transform.position = new Vector3(transform.position.x,
            transform.position.y + UnityEngine.Random.Range(-1, 2) * 1.5f, transform.position.z);
    }
}
