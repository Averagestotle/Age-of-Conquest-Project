using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Attack_Script : MonoBehaviour
{
    Unit_Properties baseProp = new Unit_Properties();
    Turret_Properties turProp = new Turret_Properties();
    BaseTurretArrayListScript baseTurretArrayListScript = new BaseTurretArrayListScript();
    [SerializeField] private TurretAttackbatScript attackbarScript;

    private Transform attackBar;

    private bool canAttack = false;
    private float curTime;

    // Start is called before the first frame update
    private void Awake()
    {
        baseProp = GameObject.Find("Player_Base").gameObject.GetComponent<Unit_Properties>();
        turProp = gameObject.GetComponent<Turret_Properties>();
        attackBar = this.gameObject.transform.Find("Attackbar_Turret_Canvas/Attackbar_Background");
        baseTurretArrayListScript = baseProp.gameObject.GetComponent<BaseTurretArrayListScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject == null)
        {
            return;
        }

        if (baseProp == null)
        {
            baseProp = gameObject.GetComponent<Unit_Properties>();
            if (baseTurretArrayListScript == null)
            {
                baseTurretArrayListScript = baseProp.gameObject.GetComponent<BaseTurretArrayListScript>();
            }            
        }

        if (turProp == null)
        {
            turProp = gameObject.GetComponent<Turret_Properties>();
        }

        TurretCheckIfCanAttack();
    }

    private void TurretCheckIfCanAttack()
    {
        if (baseTurretArrayListScript.GetEnemyUnitList().Count > 0)
        {
            TurretAttack(baseTurretArrayListScript.GetFirstUnitInList());
        }
        else
        {
            attackBar.gameObject.SetActive(false);
        }
    }

    private void TurretAttack(GameObject targetObject)
    {
        if (targetObject == null)
        {
            return;
        }

        //Vector3 dir = targetObject.transform.position - gameObject.transform.position;
        //Quaternion rot = Quaternion.LookRotation(dir);
        //gameObject.transform.rotation = rot;

        AttackTimer(turProp.turretObject.attackSpeed);
        attackBar.gameObject.SetActive(true);

        Unit_Properties otherProp = targetObject.gameObject.GetComponentInParent<Unit_Properties>();
        if (canAttack)
        {
            Debug.Log(this.gameObject.name + " attacks: " + targetObject.gameObject.name);
            otherProp.currentHealth = AttackTarget(turProp, otherProp);
            canAttack = false;
            curTime = 0f;
        }
    }

    private int AttackTarget(Turret_Properties turProp, Unit_Properties collidedUnitProp)
    {
        if (turProp != null && collidedUnitProp != null)
        {
            collidedUnitProp.currentHealth = collidedUnitProp.currentHealth - turProp.attackDamage;
        }
        return collidedUnitProp.currentHealth;
    }

    private bool AttackTimer(float timer)
    {
        if (curTime < timer)
        {
            curTime = curTime + Time.deltaTime;

            if (attackBar != null)
            {
                attackbarScript.UpdateAttackBar(turProp.attackSpeed, curTime);
            }
            
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
