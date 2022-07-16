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
        //StartCoroutine(SelfDestruct());

        
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
            if (gameObject != null)
            {
                Player_Controller playerController = new Player_Controller();
                if (teamEnum == TeamEnum.AI)
                {
                    playerController = GameObject.Find("Player_Base").GetComponent<Player_Controller>();
                }

                if (teamEnum == TeamEnum.PLAYER)
                {
                    playerController = GameObject.Find("AI_Base").GetComponent<Player_Controller>();
                }

                if (playerController != null)
                {
                    playerController.currentCurrency = playerController.AddNewCurrency(bounty);
                }
            }
            Destroy(gameObject);
        }
    }
}
