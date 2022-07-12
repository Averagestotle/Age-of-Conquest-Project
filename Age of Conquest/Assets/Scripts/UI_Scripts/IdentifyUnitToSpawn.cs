using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdentifyUnitToSpawn : MonoBehaviour
{
    private SpawnUnitScript spawnUnitScript;
    private BaseCollisionScript baseCollisionScript = new BaseCollisionScript();
    private Unit_Properties playerBaseProp = new Unit_Properties();
    private Unit_Properties aiBaseProp = new Unit_Properties();
    private GameObject playerSpawnPoint;
    private GameObject aiSpawnPoint;
    Unit_Properties basePropCheck;
    bool m_Started = true;

    private void Awake()
    {
        playerBaseProp = GameObject.Find("Player_Base").GetComponent<Unit_Properties>();
        aiBaseProp = GameObject.Find("AI_Base").GetComponent<Unit_Properties>();        
    }

    public void FindUnitToInstantiate(EraEnums eraEnum, GameObject unitObject, Vector3 spawnPosition, TeamEnum teamEnum = TeamEnum.AI, double unitCost = 0d)
    {
        spawnUnitScript = new SpawnUnitScript();
        

        playerBaseProp = GameObject.Find("Player_Base").GetComponent<Unit_Properties>();
        aiBaseProp = GameObject.Find("AI_Base").GetComponent<Unit_Properties>();
        Player_Controller playerController = new Player_Controller();
        playerSpawnPoint = GameObject.Find("Base/Player_Unit_Spawn_Point");
        aiSpawnPoint = GameObject.Find("Base/AI_Unit_Spawn_Point");
        GameObject assignedSpawnPoint;

        if (playerBaseProp == null && aiBaseProp == null)
        {
            // We don't want to spawn new units if we can't tell which base they should spawn in.
            Debug.Log("Cannot locate Player_Base or AI_Base in scene.");
            return;
        }
        if (teamEnum == TeamEnum.PLAYER)
        {
            basePropCheck = playerBaseProp;
            assignedSpawnPoint = playerSpawnPoint;
            playerController = basePropCheck.GetComponent<Player_Controller>();
        }
        else
        {
            basePropCheck = aiBaseProp;
            assignedSpawnPoint = aiSpawnPoint;
        }

        Collider[] unitColliders;

        if (assignedSpawnPoint == null)
        {
            Debug.Log("No spawn point assigned.");
            return;
        }

        unitColliders = Physics.OverlapBox(assignedSpawnPoint.transform.position, new Vector3(2, 1, 5));

        if (unitColliders.Length > 0)
        {
            Debug.Log("-----------------------------------------------------------------");
            List<GameObject> unitList = new List<GameObject>();

            for (int i = 0; i < unitColliders.Length; i++)
            {                
                Debug.Log("Game Object: " + unitColliders[i].gameObject.name);
                Unit_Properties unitObj = unitColliders[i].gameObject.GetComponent<Unit_Properties>();
                
                if (unitObj != null && unitObj.teamEnum == teamEnum && unitObj.typeEnums == ObjectTypeEnums.UNIT_TYPE)
                {
                    unitList.Add(unitObj.gameObject);                    
                }
            }

            if (unitList.Count > 0)
            {
                Debug.Log("Cannot spawn new unit.");
                return;
            }

            if (playerController != null)
            {
                playerController.SubtractNewCurrency(unitCost);
            }
        }         

        switch(eraEnum)
        {
            case EraEnums.Era01:
                if (unitObject != null)
                {
                    spawnUnitScript.SpawnUnit((GameObject)unitObject, spawnPosition, teamEnum);
                }

                break;
            default:

                break;
        }        
    }    
}
