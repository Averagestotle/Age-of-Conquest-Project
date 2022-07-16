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

    private List<GameObject> listOfSpawnableEra01Units = new List<GameObject>();

    private void Awake()
    {
        playerBase = GameObject.Find("Player_Base");
        //unitSpawn = GameObject.Find("Player_Unit_Spawn_Point");
        unitSpawn = GameObject.Find("Base/Player_Unit_Spawn_Point");
        // curButton = gameObject.AddComponent(typeof(UnitButtonClickScript)) as UnitButtonClickScript;

        Object[] subListObjects = Resources.LoadAll("", typeof(GameObject));

        foreach (GameObject indexListObject in subListObjects)
        {
            GameObject iObj = (GameObject)indexListObject;
            listOfSpawnableEra01Units.Add(iObj);
        }
    }

    private void Start()
    {
        
    }

    public void UserClickedRecruitButton(ButtonControllerScript unitButtonComponent)
    {
        if (listOfSpawnableEra01Units.Count > 0)
        {
            switch (unitButtonComponent.unitButton)
            {
                case UnitButtonEnums.UnitButton01:
                    BeginUnitSpawn(listOfSpawnableEra01Units[0]);
                    break;
                case UnitButtonEnums.UnitButton02:
                    BeginUnitSpawn(listOfSpawnableEra01Units[1]);
                    break;
                case UnitButtonEnums.UnitButton03:
                    BeginUnitSpawn(listOfSpawnableEra01Units[2]);
                    break;
                case UnitButtonEnums.UnitButton04:
                    BeginUnitSpawn(listOfSpawnableEra01Units[3]);
                    break;
                case UnitButtonEnums.UnitButton05:
                    BeginUnitSpawn(listOfSpawnableEra01Units[4]);
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("Could not find list of units to put in an array!");
        }
    }

    public void TestMethod()
    {

    }

    public void BeginUnitSpawn(GameObject unitObject)
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
