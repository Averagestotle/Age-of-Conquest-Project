using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Movement : MonoBehaviour
{
    private Unit_Properties unitProp = new Unit_Properties();
    private int unitSpeed;
    private bool enemyCollided = false;
    private bool canMoveForward = false;
    private Rigidbody rb;
    private Transform attackBar;

    private void Awake()
    {
        unitProp = this.gameObject.GetComponentInParent<Unit_Properties>();
        rb = this.gameObject.GetComponentInChildren<Rigidbody>();
        attackBar = unitProp.gameObject.transform.Find("Healthbar_Canvas/Attackbar_Background"); // /Healthbar_Canvas/Attackbar_Background
        unitSpeed = unitProp.speed;
    }

    private void Update()
    {
        
        //if (attackBar != null)
        //{
        //    if (enemyCollided)
        //    {
        //        attackBar.gameObject.SetActive(true);  
        //    }
        //    else
        //    {
        //        attackBar.gameObject.SetActive(false);
        //    }
        //}

        MoveUnit(unitProp.speed, unitProp.teamEnum);      
    }

    public void MoveUnit(int speed, TeamEnum teamEnum)
    {
        Vector3 vector = new Vector3();
        Vector3 anchor;
        float movementRayRange = 1.25f;

        if (teamEnum == TeamEnum.PLAYER)
        {
            vector = Vector3.right;
        }

        if (teamEnum == TeamEnum.AI)
        {
            vector = Vector3.left;
        }

        canMoveForward = true;

        Ray ray = new Ray(transform.position, vector);
        RaycastHit hitInfo;
        Debug.DrawRay(transform.position, new Vector3(vector.x * movementRayRange, 0, 0), Color.red);
        if (Physics.Raycast(ray, out hitInfo, movementRayRange))
        {
            // Debug.Log(this.gameObject.name + " collided with: " + hitInfo.collider.gameObject.name);
            Unit_Properties otherProp = hitInfo.collider.gameObject.GetComponentInParent<Unit_Properties>();
            if (otherProp != null && unitProp != null && unitProp.teamEnum == otherProp.teamEnum && otherProp.typeEnums == ObjectTypeEnums.BASE_TYPE)
            {
                canMoveForward = true;
                Physics.IgnoreCollision(hitInfo.collider, GetComponent<Collider>());
            }

            if (hitInfo.collider.gameObject != gameObject)
            {
                canMoveForward = false;
            }
        }

        if (canMoveForward)
        {
            transform.Translate(vector * Time.deltaTime * speed);
        }        
    }
}
