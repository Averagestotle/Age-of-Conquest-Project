using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButtonClickScript : MonoBehaviour
{
    IdentifyUnitToSpawn unitToSpawn = new IdentifyUnitToSpawn();
    Player_Controller playerController = new Player_Controller();
    ButtonControllerScript buttonController = new ButtonControllerScript();

    GameObject playerBase;
    GameObject unitSpawn;

    private void Awake()
    {
        playerBase = GameObject.Find("Player_Base");
        //unitSpawn = GameObject.Find("Player_Unit_Spawn_Point");
        unitSpawn = GameObject.Find("Base/Player_Unit_Spawn_Point");
        // curButton = gameObject.AddComponent(typeof(UnitButtonClickScript)) as UnitButtonClickScript;
    }

    private void Start()
    {
        
    }

    public void UserClickedUnitButton(GameObject unitObject)
    {
       int unitCost = 0;
        
        if (playerBase != null) 
        {
            playerController = playerBase.GetComponent<Player_Controller>();            
        }

        if (unitObject != null)
        {
            unitCost = unitObject.GetComponent<Unit_Properties>().unitObject.unitAssignedCost;
        }

        if (gameObject != null)
        {
            buttonController = gameObject.GetComponent<ButtonControllerScript>();
        }            

        if (playerBase != null && playerController != null && buttonController != null && unitSpawn != null)
        {
            if (CheckIfAffordable(unitCost))
            {
                unitToSpawn.FindUnitToInstantiate(playerController.currentEra, unitObject, unitSpawn.transform.position, TeamEnum.PLAYER, unitCost);
                //playerController.SubtractNewCurrency(unitCost);
            }
            else
            {
                Debug.Log("Could not afford: " + unitObject.name);
            }           
        }
    }
    public bool CheckIfAffordable(double unitCost)
    {
        bool isAffordable = false;

        if (unitCost > 0 && playerController.currentCurrency >= unitCost)
        {
            isAffordable = true;
        }
        return isAffordable;
    }
}
