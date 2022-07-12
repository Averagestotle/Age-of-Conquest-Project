using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitbarScript : MonoBehaviour
{
    [SerializeField] private Image _recruitbarSprite;
    public TMP_Text recruitQueueText;
    private string cString;

    public void UpdateRecruitBar(float maxRucruitTime, float currentRecruitTime)
    {
        _recruitbarSprite.fillAmount = currentRecruitTime / maxRucruitTime;
    }

    public void UpdateRecruitQueueText(int queueLimit, int currentlyQueued)
    {
        if (recruitQueueText != null)
        {
            recruitQueueText.text = "Queue: " + currentlyQueued + " / " + queueLimit;
        }
    }
}
