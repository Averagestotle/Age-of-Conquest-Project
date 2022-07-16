using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Properties : MonoBehaviour
{
    public ScriptableTurretObjects turretObject;
    public float attackSpeed;
    public int attackDamage;
    public double turretPrice;

    // Start is called before the first frame update
    void Start()
    {
        attackSpeed = turretObject.attackSpeed;
        attackDamage = turretObject.attackDamage;
        turretPrice = turretObject.turretPrice;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
