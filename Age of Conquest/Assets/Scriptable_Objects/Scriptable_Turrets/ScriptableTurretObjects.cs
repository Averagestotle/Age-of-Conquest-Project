using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Assets/Scriptable_Objects/Scriptable_Turrets")]
public class ScriptableTurretObjects : ScriptableObject
{
    public float attackSpeed;
    public int attackDamage;
    public double turretPrice;
    public TeamEnum teamEnum;
}
