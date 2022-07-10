using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Properties : MonoBehaviour
{
    public ScriptableUnitObject unitObject;
    public string unitName;
    public int cost;
    public int damage;
    public int health;
    public int speed;
    public AttackTypeEnums attackType;
    public int bounty;
    public TeamEnum teamEnum = TeamEnum.AI;
    public ObjectTypeEnums typeEnums;
    public float attackSpeed;
    public float attackRange;
    private GameObject playerSpawnPoint;

    [SerializeField] private Healthbar_Script healthbarScript;

    public int currentHealth;

    private void Start()
    {
        unitName = unitObject.unitName;
        cost = unitObject.unitAssignedCost;
        damage = unitObject.unitAssignedDamage;
        health = unitObject.unitAssignedHealth;
        speed = unitObject.unitAssignedSpeed;
        attackType = unitObject.attackType;
        bounty = unitObject.bounty;
        currentHealth = health;
        attackSpeed = unitObject.attackSpeed;
        attackRange = unitObject.attackRange;
        typeEnums = unitObject.objectTypeEnums;

        healthbarScript.UpdateHealthBar(health, currentHealth);
        StartCoroutine(SelfDestruct());

        
    }

    private void Update()
    {
        healthbarScript.UpdateHealthBar(health, currentHealth);
        OnDeath(currentHealth);
    }

    public IEnumerator SelfDestruct()
    {
        // Temporary, to prevent an infinite amount from being spawned.
        yield return new WaitForSeconds(100f);
        Destroy(gameObject);
    }

    private void OnDeath(float unitHealth)
    {
        if (unitHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        playerSpawnPoint = GameObject.Find("Base/Player_Unit_Spawn_Point");
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (playerSpawnPoint != null)
        {
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
                //Gizmos.DrawWireCube(playerSpawnPoint.transform.position, new Vector3(4, 1, 5));
        }

    }
}
