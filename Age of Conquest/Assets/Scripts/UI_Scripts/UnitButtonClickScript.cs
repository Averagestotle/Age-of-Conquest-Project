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
        unitSpawn = GameObject.Find("Player_Unit_Spawn_Point");
        // curButton = gameObject.AddComponent(typeof(UnitButtonClickScript)) as UnitButtonClickScript;
    }

    private void Start()
    {
        
    }

    public void UserClickedUnitButton(GameObject unitObject)
    {
        //GameObject playerBase = new GameObject();
       // GameObject unitSpawn = new GameObject();

        
        if (playerBase != null) 
        {
            playerController = playerBase.GetComponent<Player_Controller>();
        }
        if (gameObject != null)
        {
            buttonController = gameObject.GetComponent<ButtonControllerScript>();
        }            

        if (playerController != null && buttonController != null && unitSpawn != null)
        {
            unitToSpawn.FindUnitToInstantiate(playerController.currentEra, unitObject, unitSpawn.transform.position, TeamEnum.PLAYER);
        }
        
    }
}
