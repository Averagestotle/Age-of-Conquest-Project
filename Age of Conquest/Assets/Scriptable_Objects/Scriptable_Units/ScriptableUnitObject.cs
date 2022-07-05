using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Assets/Scriptable_Objects/Scriptable_Units")]
public class ScriptableUnitObject : ScriptableObject
{
    public string unitName;
    public int unitAssignedHealth;
    public int unitAssignedCost;
    public int unitAssignedDamage;
    public int unitAssignedSpeed;
    public AttackTypeEnums attackType;
    public float attackSpeed;
    public int bounty;
}
