using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBaseScripts : MonoBehaviour
{
    private GameObject turretBase;
    private GameObject turretBase02;
    private GameObject turretBase03;
    private GameObject playerBase;
    private Player_Controller playerController;

    public double costToBuyTurretBase = 10d;

    // Start is called before the first frame update
    private void Awake()
    {

        turretBase = (GameObject)Resources.Load("TurretBase/TurretBase", typeof(GameObject));

        playerBase = GameObject.Find("Player_Base");
        playerController = playerBase.GetComponent<Player_Controller>();
    }

    public void ActivateBaseTurret()
    {
        if(playerController == null || turretBase == null)
        {
            return;
        }

        if(playerController.currentCurrency >= costToBuyTurretBase)
        {
            if (playerController.IsMaxedOutOnBuildableTurretBases())
            {
                return;
            }

            playerController.AddNewTurretBaseSlot();            
            
            switch (playerController.GetCurrentBuildableTurretBases())
            {
                case 1:
                    Instantiate(turretBase, playerBase.transform.position + new Vector3(2, 2.5f, 0), Quaternion.identity);
                    playerController.currentCurrency = playerController.SubtractNewCurrency(costToBuyTurretBase);
                    //turretBase01.SetActive(true);
                    break;
                case 2:
                    Instantiate(turretBase, playerBase.transform.position + new Vector3(2, 3.5f, 0), Quaternion.identity);
                    playerController.currentCurrency = playerController.SubtractNewCurrency(costToBuyTurretBase);
                    //turretBase02.SetActive(true);
                    break;
                case 3:
                    Instantiate(turretBase, playerBase.transform.position + new Vector3(2, 4.5f, 0), Quaternion.identity);
                    playerController.currentCurrency = playerController.SubtractNewCurrency(costToBuyTurretBase);
                    //turretBase03.SetActive(true);
                    break;
                default:
                    // Do Nothing
                    break;
            }
                

        }
    }
}
