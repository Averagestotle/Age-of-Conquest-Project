using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Scripts : MonoBehaviour
{
    GameObject playerBase;
    Player_Controller playerController;

    private void Start()
    {
        playerBase = GameObject.Find("Player_Base");

        if (playerBase != null)
        {
            playerController = playerBase.GetComponent<Player_Controller>();
        }
    }

    public void SpawnTurret(GameObject turretGhostObj)
    {
        Turret_Properties turretProp = new Turret_Properties();

        if (turretGhostObj == null)
        {
            return;
        }
        else
        {
            turretProp = turretGhostObj.GetComponent<Turret_Properties>();

            if (turretProp == null)
            {
                return;
            }
        }

        if (gameObject != null && !playerController.CheckIfInBuildMode() && CheckIfAffordable(turretProp.turretObject.turretPrice, playerController.currentCurrency))
        {
            Instantiate(turretGhostObj);
            playerController.SetIfInBuildMode(true);
        }
    }

    public bool CheckIfAffordable(double turretCost, double playerCurrency)
    {
        bool isAffordable = false;

        if (turretCost > 0 && playerCurrency >= turretCost)
        {
            isAffordable = true;
        }
        return isAffordable;
    }
}
