using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StoreControllerScript : MonoBehaviour
{

    public int[,] storeItems = new int[2, 4];
    public GameObject player1Text;
    public Text player1;
    public GameObject player2Text;
    public Text player2;

    bool p1selected = false;
    bool p2selected = false;

    // Start is called before the first frame update
    void Start()
    {
        storeItems[1, 1] = 1;
        storeItems[1, 2] = 2;
        storeItems[1, 3] = 3;
    }

    // Update is called once per frame
    public void GetItem()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if (!p1selected)
        {
            p1selected = true;
            PlayerPrefs.SetInt("P1", ButtonRef.GetComponent<ButtonController>().ItemID);
            player1.text = "Player one choose item " + ButtonRef.GetComponent<ButtonController>().ItemID;
            player1Text.SetActive(true);
            Debug.Log("Player one choose" + ButtonRef.GetComponent<ButtonController>().ItemID);
        }
        else if (!p2selected)
        {
            p2selected = true;
            PlayerPrefs.SetInt("P2", ButtonRef.GetComponent<ButtonController>().ItemID);
            player2.text = "Player two choose item " + ButtonRef.GetComponent<ButtonController>().ItemID;
            player2Text.SetActive(true);
            Debug.Log("Player two choose" + ButtonRef.GetComponent<ButtonController>().ItemID);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
