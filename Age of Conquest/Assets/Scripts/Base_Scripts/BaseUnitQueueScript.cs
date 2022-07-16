using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitQueueScript : MonoBehaviour
{
    List<GameObject> unitQueue = new List<GameObject>();
    private SpawnUnitScript spawnUnitScript = new SpawnUnitScript();
    Unit_Properties basePropCheck;

    private GameObject playerSpawnPoint;
    private GameObject aiSpawnPoint;

    private int queueLimit = 5;
    private float curTime;
    private bool canBuildUnit;

    [SerializeField] private RecruitbarScript recruitbarScript;
    private Transform recruitBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        basePropCheck = this.gameObject.GetComponent<Unit_Properties>();
        spawnUnitScript = new SpawnUnitScript();
        playerSpawnPoint = GameObject.Find("Base/Player_Unit_Spawn_Point");
        aiSpawnPoint = GameObject.Find("Base/AI_Unit_Spawn_Point");
        if (basePropCheck != null && basePropCheck.teamEnum == TeamEnum.PLAYER)
        {
            recruitBar = GameObject.Find("Player Canvas/Panel/recruitbar_Base_Canvas/Recruitbar_Background").transform;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFirstUnitInQueue();
    }

    public List<GameObject> AddUnitToQueue(GameObject unitObject)
    {
        unitQueue.Add(unitObject);
        Debug.Log("Adding unit: " + unitObject.gameObject.name);
        return unitQueue;
    }

    public List<GameObject> RemoveUnitToQueue()
    {
        if (unitQueue.Count > 0)
        {
            unitQueue.RemoveAt(0);
        }
        return unitQueue;
    }

    public bool IsQueueFull()
    {
        if (unitQueue.Count < queueLimit)
        {
            return false;
        }
        else
        {
            Debug.Log("Queue Full");
            return true;
        }        
    }

    private void SpawnFirstUnitInQueue()
    {
        GameObject assignedSpawnPoint;

        if (basePropCheck == null)
        {
            return;
        }

        if (basePropCheck.teamEnum == TeamEnum.PLAYER)
        {
            assignedSpawnPoint = playerSpawnPoint;
        }
        else
        {
            assignedSpawnPoint = aiSpawnPoint;
        }

        if (unitQueue.Count > 0)
        {
            Unit_Properties newUnitObjProp = unitQueue[0].GetComponent<Unit_Properties>();
            if (newUnitObjProp != null)
            {
                UnitBuildTimer(newUnitObjProp.unitObject.unitBuildSpeed);

                if (basePropCheck != null && basePropCheck.teamEnum == TeamEnum.PLAYER)
                {
                    if (recruitBar != null)
                    {
                        recruitBar.gameObject.SetActive(true);
                    }
                }


                if (canBuildUnit && IsSpawnEmpty(assignedSpawnPoint, basePropCheck.teamEnum))
                {

                    spawnUnitScript.SpawnUnit(unitQueue[0].gameObject, assignedSpawnPoint.transform.position, basePropCheck.teamEnum);
                    RemoveUnitToQueue();
                }
            }                
        }
        else
        {
            if (basePropCheck != null && basePropCheck.teamEnum == TeamEnum.PLAYER)
            {
                if (recruitBar != null)
                {
                    recruitBar.gameObject.SetActive(false);
                }
            }
        }
    }

    private bool UnitBuildTimer(float timer)
    {
        if (curTime < timer)
        {
            curTime = curTime + Time.deltaTime;

            if (basePropCheck != null && basePropCheck.teamEnum == TeamEnum.PLAYER)
            {
                if (recruitBar != null)
                {
                    recruitbarScript.UpdateRecruitBar(timer, curTime);
                    recruitbarScript.UpdateRecruitQueueText(queueLimit, unitQueue.Count);
                }
            }
            
            if (curTime >= timer)
            {
                canBuildUnit = true;
                curTime = 0f;
            }
            else
            {
                canBuildUnit = false;
            }
        }
        return canBuildUnit;
    }

    private bool IsSpawnEmpty(GameObject assignedSpawnPoint, TeamEnum teamEnum)
    {
        Collider[] unitColliders;

        if (assignedSpawnPoint == null)
        {
            Debug.Log("No spawn point assigned.");
            return false;
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
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }
}
