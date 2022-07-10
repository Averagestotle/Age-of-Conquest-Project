using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionScript : MonoBehaviour
{
    private bool isBaseEmpty;
    private Unit_Properties baseProp;

    public bool GetisBaseEmpty(Unit_Properties baseProp)
    {
        if(baseProp != null)
        {
            return this.isBaseEmpty;
        }
        else
        {
            return false;
        }        
    }

    private void Awake()
    {
         baseProp = this.gameObject.GetComponentInParent<Unit_Properties>();
    }

    private void FixedUpdate()
    {
        this.isBaseEmpty = true;
    }

    private void OnTriggerStay(Collider other)
    {
        Unit_Properties otherProp = other.gameObject.GetComponentInParent<Unit_Properties>();

        if (otherProp != null && baseProp != null)
        {
            if (baseProp.teamEnum == otherProp.teamEnum && otherProp.typeEnums == ObjectTypeEnums.UNIT_TYPE)
            {
                isBaseEmpty = false;
            }
        }
    }
}
