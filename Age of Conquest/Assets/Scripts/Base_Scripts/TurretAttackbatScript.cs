using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretAttackbatScript : MonoBehaviour
{
    [SerializeField] private Image _attackbarSprite;

    public void UpdateAttackBar(float maxAttack, float currentAttack)
    {
        _attackbarSprite.fillAmount = currentAttack / maxAttack;
    }
}
