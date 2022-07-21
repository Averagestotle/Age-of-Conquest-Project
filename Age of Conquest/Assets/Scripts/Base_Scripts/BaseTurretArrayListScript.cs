using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurretArrayListScript : MonoBehaviour
{
    [SerializeField] private float range;
    public List<GameObject> enemyUnitList = new List<GameObject>();
    Unit_Properties baseProp = new Unit_Properties();

    public List<GameObject> GetEnemyUnitList()
    {
        return enemyUnitList;
    }

    public GameObject GetFirstUnitInList()
    {
        return enemyUnitList[0];
    }

    public void RemoveEnemyUnitFromList(GameObject gameObj)
    {
        Debug.Log("Removing dead object for: " + gameObject.name + " Team: " + baseProp.teamEnum);
        enemyUnitList.Remove(gameObj);
    }

    

    private void Awake()
    {
        baseProp = gameObject.GetComponent<Unit_Properties>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject == null)
        {
            return;
        }

        if (baseProp == null)
        {
            baseProp = gameObject.GetComponent<Unit_Properties>();
        }

        CheckIfUnitIsNearby();
        // CheckIfUnitIsDead();
    }

    private void CheckIfUnitIsNearby()
    {
        if (baseProp != null)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent<Unit_Properties>(out Unit_Properties unitProp))
                {
                    if (unitProp.teamEnum != baseProp.teamEnum)
                    {
                        if (!enemyUnitList.Contains(collider.gameObject))
                        {
                            Debug.Log("Base " + baseProp.teamEnum + " has detected: " + collider.gameObject.name);
                            enemyUnitList.Add(collider.gameObject);
                        }

                    }
                }
            }
        }
    }

    private void CheckIfUnitIsDead()
    {
        if (baseProp != null)
        {
            if (enemyUnitList.Count > 0)
            {
                List<GameObject> modifiedEnemyUnitList = new List<GameObject>();
                modifiedEnemyUnitList = enemyUnitList;

                foreach (GameObject gameObjectCheck in enemyUnitList)
                {
                    int index = enemyUnitList.IndexOf(gameObjectCheck);
                    if (gameObjectCheck == null)
                    {
                        
                        modifiedEnemyUnitList.RemoveAt(index);
                    }
                }

                enemyUnitList = modifiedEnemyUnitList;
            }
        }
    }
}
