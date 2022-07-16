using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnScript : MonoBehaviour
{
    IdentifyUnitToSpawn unitToSpawn = new IdentifyUnitToSpawn();
    Player_Controller playerController = new Player_Controller();

    private List<GameObject> listOfSpawnableUnits = new List<GameObject>();

    GameObject playerBase;
    GameObject unitSpawn;

    private float curTime;
    private bool canSpawn = false;

    private void Awake()
    {
        playerBase = GameObject.Find("AI_Base");
        unitSpawn = GameObject.Find("Base/AI_Unit_Spawn_Point");
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Object[] subListObjects = Resources.LoadAll("Era01_Prefabs", typeof(GameObject)); // Assets/Prefabs/Unit_Prefabs/Resources/Era01_Prefabs/Unit01.prefab

        foreach (GameObject indexListObject in subListObjects)
        {
            GameObject iObj = (GameObject)indexListObject;
            listOfSpawnableUnits.Add(iObj);
        }

        if (playerBase != null)
        {
            playerController = playerBase.GetComponent<Player_Controller>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        AISpawnUnitMethod();
    }

    public void AISpawnUnitMethod()
    {
        int listLength = listOfSpawnableUnits.Count;

        if (listLength > 0)
        {
            SpawnTimer(5f);
            if (playerBase != null && playerController != null && unitSpawn != null && canSpawn)
            {
                int randIndex = Random.Range(0, listLength - 1);
                GameObject spawnedUnit = listOfSpawnableUnits[randIndex];

                if(spawnedUnit != null)
                {
                    unitToSpawn.FindUnitToInstantiate(playerController.currentEra, spawnedUnit, unitSpawn.transform.position, TeamEnum.AI);
                }
                
                curTime = 0f;
                canSpawn = false;
            }
        }
    }

    private bool SpawnTimer(float timer)
    {
        if (curTime < timer)
        {
            curTime = curTime + Time.deltaTime;
            if (curTime >= timer)
            {
                canSpawn = true;
                curTime = 0f;
            }
            else
            {
                canSpawn = false;
            }
        }
        return canSpawn;
    }
}
