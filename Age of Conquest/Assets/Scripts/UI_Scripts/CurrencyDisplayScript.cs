using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplayScript : MonoBehaviour
{
    public TMP_Text currencyText;
    private string cString;
    Player_Controller playerController = new Player_Controller();
    GameObject playerBase;

    // Start is called before the first frame update
    void Start()
    {
        playerBase = GameObject.Find("Player_Base");
        //currencyText = currencyTextObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GenerateCurrencyText();
    }

    private void GenerateCurrencyText()
    {
        if (playerBase != null)
        {
            playerController = playerBase.GetComponent<Player_Controller>();

            if (playerController != null)
            {
                cString = "Currency: " + playerController.currentCurrency;
                currencyText.text = cString;
            }
        }
    }
}
