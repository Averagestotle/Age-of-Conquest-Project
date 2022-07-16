using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit_Attack : MonoBehaviour
{
    private Unit_Properties unitProp = new Unit_Properties();
    private BoxCollider boxCollider = new BoxCollider();
    [SerializeField] private Healthbar_Script attackbarScript;
    private Transform attackBar;
    private bool canAttack = false;
    private float curTime;

    Vector3 vector = new Vector3();
    Vector3 anchor = new Vector3();
    float attackRayRange = 0f;
    int targetMask;

    GameObject playerBase;
    Player_Controller playerController;

    private void Awake()
    {
        unitProp = this.gameObject.GetComponentInParent<Unit_Properties>();
        attackBar = unitProp.gameObject.transform.Find("Healthbar_Canvas/Attackbar_Background"); // /Healthbar_Canvas/Attackbar_Background
        boxCollider = this.gameObject.GetComponentInParent<BoxCollider>();
        attackRayRange = unitProp.attackRange;
    }

    private void Update()
    {
        AttackMethod();
    }

    public void AttackMethod()
    {


        if (unitProp != null && unitProp.teamEnum == TeamEnum.PLAYER)
        {
            vector = Vector3.right;
            // Player should target AI.
            targetMask = LayerMask.GetMask("AI Mask"); // "AI Mask"
            anchor = transform.position + new Vector3(boxCollider.size.x / 2 + 0.02f, 0f, 0f);
        }

        if (unitProp != null && unitProp.teamEnum == TeamEnum.AI)
        {
            vector = Vector3.left;
            // Player should target Player.
            targetMask = LayerMask.GetMask("Player Mask"); // "Player Mask"
            anchor = transform.position - new Vector3(boxCollider.size.x / 2 + 0.02f, 0f, 0f);
        }

        Ray ray = new Ray(transform.position, vector);

        if (unitProp.attackType == AttackTypeEnums.RANGE)
        {
            RangeMethod(ray, vector, unitProp.attackRange, targetMask);
        }
        else
        {
            MeleeMethod(ray, vector, unitProp.attackRange);
        }  
    }

    private void MeleeMethod(Ray ray, Vector3 vector, float attackRange)
    {
        //Ray ray = new Ray(transform.position, vector);
        RaycastHit hitInfo;
        Debug.DrawRay(anchor, new Vector3(vector.x * attackRange, 0, 0), Color.blue);

        if (Physics.Raycast(ray, out hitInfo, attackRange))
        {
            Unit_Properties otherProp = hitInfo.collider.gameObject.GetComponentInParent<Unit_Properties>();

            // We don't attack our friends here, or if nobody is there...
            if (otherProp != null && unitProp != null && unitProp.teamEnum != otherProp.teamEnum)
            {
                AttackTimer(unitProp.attackSpeed);
                attackBar.gameObject.SetActive(true);
                if (canAttack)
                {
                    //this.gameObject.GetComponentInParent<Unit_Properties>().teamEnum - Current object

                    Debug.Log(unitProp.gameObject.name + " attacks: " + otherProp.gameObject.name);
                    otherProp.currentHealth = AttackObject(unitProp, otherProp);
                    canAttack = false;
                    curTime = 0f;
                }
            }
            else
            {
                attackBar.gameObject.SetActive(false);
            }
        }
        else
        {
            this.attackBar.gameObject.SetActive(false);
        }
    }

    private void RangeMethod(Ray ray, Vector3 vector, float attackRange, LayerMask layerMask)
    {
        //Ray ray = new Ray(transform.position, vector);
        RaycastHit hitInfo;
        Debug.DrawRay(anchor, new Vector3(vector.x * attackRange, 0, 0), Color.blue);

        if (Physics.Raycast(ray, out hitInfo, attackRange, layerMask))
        {
            Unit_Properties otherProp = hitInfo.collider.gameObject.GetComponentInParent<Unit_Properties>();

            // We don't attack our friends here, or if nobody is there...
            if (otherProp != null && unitProp != null && unitProp.teamEnum != otherProp.teamEnum)
            {
                AttackTimer(unitProp.attackSpeed);
                attackBar.gameObject.SetActive(true);
                if (canAttack)
                {
                    //this.gameObject.GetComponentInParent<Unit_Properties>().teamEnum - Current object

                    Debug.Log(unitProp.gameObject.name + " attacks: " + otherProp.gameObject.name);
                    otherProp.currentHealth = AttackObject(unitProp, otherProp);
                    canAttack = false;
                    curTime = 0f;
                }
            }
            else
            {
                attackBar.gameObject.SetActive(false);
            }
        }
    }

    private int AttackObject(Unit_Properties curUnitProp, Unit_Properties collidedUnitProp)
    {
        if (curUnitProp != null && collidedUnitProp != null)
        {
            collidedUnitProp.currentHealth = collidedUnitProp.currentHealth - curUnitProp.damage;
        }
        return collidedUnitProp.currentHealth;
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
