using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit_Attack : MonoBehaviour
{
    private Unit_Properties unitProp = new Unit_Properties();
    [SerializeField] private Healthbar_Script attackbarScript;
    private bool canAttack = false;
    private float curTime;

    private void Awake()
    {
        unitProp = this.gameObject.GetComponentInParent<Unit_Properties>();
    }

    public int AttackMethod(Unit_Properties curUnitProp, Unit_Properties collidedUnitProp)
    {
        if (curUnitProp != null && collidedUnitProp != null)
        {
            collidedUnitProp.currentHealth = collidedUnitProp.currentHealth - curUnitProp.damage;
        }
        return collidedUnitProp.currentHealth;
    }

    private void OnTriggerStay(Collider other)
    {

        Unit_Properties otherProp = other.gameObject.GetComponentInParent<Unit_Properties>();
        if (unitProp.teamEnum != otherProp.teamEnum)
        {
            AttackTimer(unitProp.attackSpeed);
            if (canAttack) 
            {
                //this.gameObject.GetComponentInParent<Unit_Properties>().teamEnum - Current object

                // We don't attack our friends here.

                Debug.Log(unitProp.gameObject.name + " attacks: " + otherProp.gameObject.name);
                otherProp.currentHealth = AttackMethod(unitProp, otherProp);
                canAttack = false;
                curTime = 0f;
            }           
        }       
    }

    private bool AttackTimer(float timer)
    {
        if (curTime < timer)
        {
            curTime = curTime + Time.deltaTime;
            attackbarScript.UpdateAttackBar(unitProp.attackSpeed, curTime);
            if (curTime >= timer)
            {
                canAttack = true;
                curTime = 0f;
            }
            else
            {
                canAttack = false;
            }
        }
        return canAttack;
    }
}
