using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Script : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;

    [SerializeField] private Image _attackbarSprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateAttackBar(float maxAttack, float currentAttack)
    {
        _attackbarSprite.fillAmount = currentAttack / maxAttack;
    }

    public int TestHealthbar(int currentHealth)
    {
        return currentHealth--;
    }
}
