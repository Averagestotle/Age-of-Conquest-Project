using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EraDisplayScripts : MonoBehaviour
{
    public TMP_Text eraText;
    private string cString;
    Player_Controller playerController = new Player_Controller();
    GameObject playerBase;

    // Start is called before the first frame update
    void Start()
    {
        playerBase = GameObject.Find("Player_Base");
        playerController = playerBase.GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GenerateEraText();
    }

    private void GenerateEraText()
    {
        if (playerBase != null)
        {
            

            if (playerController != null)
            {
                cString = "Era: " + (((int)playerController.currentEra) + 1);
                eraText.text = cString;
            }
        }
    }
}
