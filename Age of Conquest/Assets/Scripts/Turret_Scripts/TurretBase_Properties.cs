using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase_Properties : MonoBehaviour
{
    private bool isBaseEmpty = true;

    public bool CheckIfTurretBaseIsEmpty()
    {
        return isBaseEmpty;
    }
    public bool SetIfTurretBaseIsEmpty(bool isEmpty)
    {
        isBaseEmpty = isEmpty;
        return isBaseEmpty;
    }
}
