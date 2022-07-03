using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Movement : MonoBehaviour
{
    private bool playerUnit = false;
    public int speed;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnit(playerUnit);
    }

    private void MoveUnit(bool isPlayer)
    {
        //if (speed != null && isPlayer)
        //{
        //    transform.Translate((Vector3.right * speed) * Time.deltaTime);
        //}
        if (speed != null)
        {
            transform.Translate((Vector3.right * speed) * Time.deltaTime);
        }
    }

    IEnumerator SelfDestruct()
    {
        // Temporary, to prevent an infinite amount from being spawned.
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
