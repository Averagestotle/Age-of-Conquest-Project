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

    Unit_Movement movementScript = new Unit_Movement();

    [SerializeField] private Healthbar_Script healthbarScript;

    public int currentHealth;
    public bool isPlayer = false;

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

        healthbarScript.UpdateHealthBar(health, currentHealth);
        StartCoroutine(SelfDestruct());
    }

    private void Update()
    {
        movementScript.MoveUnit(gameObject, speed, false);
    }

    public IEnumerator SelfDestruct()
    {
        // Temporary, to prevent an infinite amount from being spawned.
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
