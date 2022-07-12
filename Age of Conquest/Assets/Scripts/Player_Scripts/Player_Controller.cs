using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public EraEnums currentEra = EraEnums.Era01;
    public double currentCurrency;
    public double incrementalIncome = 50d;
    private float curTime;
    private float currencyTicker = 2f;
    private bool canUpdateCurrency = false;

    public double SubtractNewCurrency(double newCurrency)
    {
        currentCurrency -= newCurrency;
        return currentCurrency;
    }

    public double AddNewCurrency(double newCurrency)
    {
        currentCurrency += newCurrency;
        return currentCurrency;
    }

    private bool CurrencyTimer(float timer)
    {
        if (curTime < timer)
        {
            curTime = curTime + Time.deltaTime;
            
            if (curTime >= timer)
            {
                canUpdateCurrency = true;
                curTime = 0f;
            }
            else
            {
                canUpdateCurrency = false;
            }
        }
        return canUpdateCurrency;
    }

    private void Update()
    {
        if (CurrencyTimer(currencyTicker))
        {
            AddNewCurrency(incrementalIncome);
        }
    }
}