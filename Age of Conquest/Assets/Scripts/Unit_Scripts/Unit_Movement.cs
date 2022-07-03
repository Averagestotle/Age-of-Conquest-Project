using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Movement : MonoBehaviour
{
    public void MoveUnit(GameObject gameObject,int speed,bool isPlayer)
    {
        //if (speed != null && isPlayer)
        //{
        //    transform.Translate((Vector3.right * speed) * Time.deltaTime);
        //}
        if (speed != null)
        {
            gameObject.transform.Translate((Vector3.right * speed) * Time.deltaTime);
        }
    }
}
