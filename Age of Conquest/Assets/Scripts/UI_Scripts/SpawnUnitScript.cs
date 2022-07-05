using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnitScript : MonoBehaviour
{
    public static List<Unit_Properties> unitList = new List<Unit_Properties>();

    public static List<Unit_Properties> GetUnitList()
    {
        return unitList;
    }

    public void FindUnitsToAdd()
    {
        Unit_Properties[] unitListAdd = GameObject.FindObjectsOfType<Unit_Properties>();
        unitList = new List<Unit_Properties>();
        foreach (Unit_Properties unit in unitListAdd)
        {
            if (unit.enabled)
            {
                unitList.Add(unit);
                // Debug.Log("Adding unit " + unit.gameObject.name + "...");
            }
        }
    }

    public void SpawnUnit(GameObject unitObject, Vector3 spawnPosition, TeamEnum teamEnum = TeamEnum.AI)
    {
        if (unitObject != null && spawnPosition != null)
        {
            GameObject newUnitObj = Instantiate(unitObject, spawnPosition, Quaternion.identity);
            if (newUnitObj != null)
            {
                Unit_Properties newUnitObjProp = newUnitObj.GetComponent<Unit_Properties>();
                newUnitObjProp.teamEnum = teamEnum;
                unitList.Add(newUnitObjProp);
            }                   
        }
    }
}
