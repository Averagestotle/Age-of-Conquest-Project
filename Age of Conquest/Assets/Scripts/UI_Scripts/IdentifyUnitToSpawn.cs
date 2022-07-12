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
    private BaseUnitQueueScript playerQueueScript = new BaseUnitQueueScript();
    private BaseUnitQueueScript aiQueueScript = new BaseUnitQueueScript();
    BaseUnitQueueScript baseUnitQueue = new BaseUnitQueueScript();
    private GameObject playerSpawnPoint;
    private GameObject aiSpawnPoint;
    Unit_Properties basePropCheck;
    bool m_Started = true;    

    private void Awake()
    {
        playerBaseProp = GameObject.Find("Player_Base").GetComponent<Unit_Properties>();
        aiBaseProp = GameObject.Find("AI_Base").GetComponent<Unit_Properties>();
        playerQueueScript = GameObject.Find("Player_Base").GetComponent<BaseUnitQueueScript>();
        aiQueueScript = GameObject.Find("AI_Base").GetComponent<BaseUnitQueueScript>();
    }

    public void FindUnitToInstantiate(EraEnums eraEnum, GameObject unitObject, Vector3 spawnPosition, TeamEnum teamEnum = TeamEnum.AI, double unitCost = 0d)
    {
        spawnUnitScript = new SpawnUnitScript();
        

        playerBaseProp = GameObject.Find("Player_Base").GetComponent<Unit_Properties>();
        aiBaseProp = GameObject.Find("AI_Base").GetComponent<Unit_Properties>();
        playerQueueScript = GameObject.Find("Player_Base").GetComponent<BaseUnitQueueScript>();
        aiQueueScript = GameObject.Find("AI_Base").GetComponent<BaseUnitQueueScript>();
        
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
            baseUnitQueue = GameObject.Find("Player_Base").GetComponent<BaseUnitQueueScript>();
        }
        else
        {
            basePropCheck = aiBaseProp;
            assignedSpawnPoint = aiSpawnPoint;
            baseUnitQueue = GameObject.Find("AI_Base").GetComponent<BaseUnitQueueScript>();
        }

        if (playerController != null && !baseUnitQueue.IsQueueFull())
        {
            playerController.SubtractNewCurrency(unitCost);
        }       

        switch (eraEnum)
        {
            case EraEnums.Era01:
                if (unitObject != null)
                {
                    if(!baseUnitQueue.IsQueueFull())
                    {
                        baseUnitQueue.AddUnitToQueue(unitObject);
                    }
                }

                break;
            default:

                break;
        }        
    }    
}
