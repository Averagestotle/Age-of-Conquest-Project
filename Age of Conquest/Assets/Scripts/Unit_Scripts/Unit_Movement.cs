using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Movement : MonoBehaviour
{
    private Unit_Properties unitProp = new Unit_Properties();
    private int unitSpeed;
    private bool isCollided = false;
    private Rigidbody rb;
    private Transform attackBar;

    private void Awake()
    {
        unitProp = this.gameObject.GetComponentInParent<Unit_Properties>();
        rb = this.gameObject.GetComponentInChildren<Rigidbody>();
        attackBar = unitProp.gameObject.transform.Find("Healthbar_Canvas/Attackbar_Background"); // /Healthbar_Canvas/Attackbar_Background
        unitSpeed = unitProp.speed;
    }

    private void FixedUpdate()
    {
        
        if (attackBar != null)
        {
            if (isCollided)
            {
                attackBar.gameObject.SetActive(true);  
            }
            else
            {
                attackBar.gameObject.SetActive(false);
            }
        }
        if(rb != null && unitProp != null && unitProp.speed > 0)
        {
            MoveUnit(rb, unitProp.speed, unitProp.teamEnum);
        }
        // Unfortunantely OnTriggerExit does NOT get called if an object within a trigger is destroyed.
        // Need to reset the bool value every fixed frame then... 
        isCollided = false;
    }

    public void MoveUnit(Rigidbody rb, int speed, TeamEnum teamEnum)
    {
        if (isCollided)
        {
            speed = 0;
        }
        else
        {
            speed = unitSpeed = unitProp.speed;
        }

        if (speed != null && teamEnum == TeamEnum.PLAYER)
        {
            //rb.AddForce((Vector3.right * speed) * Time.deltaTime);
            this.gameObject.transform.Translate((Vector3.right * speed) * Time.deltaTime);
        }

        if (speed != null && teamEnum == TeamEnum.AI)
        {
            // rb.AddForce((Vector3.left * speed) * Time.deltaTime);
            this.gameObject.transform.Translate((Vector3.left * speed) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit_Properties otherProp = other.gameObject.GetComponentInParent<Unit_Properties>();

        isCollided = true;

        if (unitProp.teamEnum == otherProp.teamEnum && otherProp.typeEnums != ObjectTypeEnums.BASE_TYPE)
        {
            isCollided = false;
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }
        if (unitProp.teamEnum != otherProp.teamEnum) {
            isCollided = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Unit_Properties otherProp = other.gameObject.GetComponentInParent<Unit_Properties>();

        isCollided = true;

        if (unitProp.teamEnum == otherProp.teamEnum && otherProp.typeEnums == ObjectTypeEnums.BASE_TYPE)
        {
            isCollided = false;
            Collider getChildRigidBody = unitProp.gameObject.transform.Find("Cube").GetComponent<Collider>();
            Physics.IgnoreCollision(other, getChildRigidBody);
        }
        if (unitProp.teamEnum != otherProp.teamEnum)
        {
            isCollided = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isCollided = false;
    }
}
