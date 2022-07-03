using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHidePanels : MonoBehaviour
{
    public void ShowPanel(GameObject gameObject)
    {
        if(gameObject != null && gameObject.activeSelf != true)
        {
            gameObject.SetActive(true);
        }
        else if (gameObject != null && gameObject.activeSelf == true)
        {
            HidePanel(gameObject);
        }
        else
        {
            Debug.Log("Bruh, you can't set an empty object's active state!");
        }
        
    }

    public void HidePanel(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }

    }
}
