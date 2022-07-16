using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public EraEnums currentEra = EraEnums.Era01;
    public double currentCurrency;
    public double incrementalIncome = 50d;
    private int currentBuildableTurrets = 0;
    private int maxBuildableTurrets = 3;
    private float curTime;
    private float currencyTicker = 2f;
    private bool canUpdateCurrency = false;
    private bool canBuild;
    private bool isInBuildMode;

    public bool CheckIfBuildable()
    {
        return canBuild;
    }

    public bool SetIfBuildable(bool isBuildable)
    {
        canBuild = isBuildable;
        return canBuild;
    }

    public bool CheckIfInBuildMode()
    {
        return isInBuildMode;
    }

    public bool SetIfInBuildMode(bool inBuildMode)
    {
        isInBuildMode = inBuildMode;
        return isInBuildMode;
    }

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

    public int AddNewTurretBaseSlot()
    {
        if (currentBuildableTurrets < maxBuildableTurrets)
        {
            currentBuildableTurrets++;
            return currentBuildableTurrets;
        }
        else
        {
            return currentBuildableTurrets;
        }
        
    }

    public int GetCurrentBuildableTurretBases()
    {
        return currentBuildableTurrets;
    }
    public bool IsMaxedOutOnBuildableTurretBases()
    {
        return currentBuildableTurrets >= maxBuildableTurrets;
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