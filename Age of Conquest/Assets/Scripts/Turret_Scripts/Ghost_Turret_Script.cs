using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Turret_Script : MonoBehaviour
{
    RaycastHit hit;
    Vector3 newPos;
    public GameObject prefab;
    public Material buildableMat;
    public Material blockedBuildableMat;
    public bool isCollided;
    public float scrollSpeed;
    int layerMask;

    public int buildingCost;

    public float horizontalSpeed = 1000.0f;

    public GameObject UICanvas;
    public GameObject playerBase;
    private Player_Controller playerController = new Player_Controller();
    private TurretBase_Properties turretBaseProperties = new TurretBase_Properties();

    // Start is called before the first frame update
    void Start()
    {
        playerBase = GameObject.Find("Player_Base");
        playerController = playerBase.GetComponent<Player_Controller>();

        isCollided = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50000000.0f, (1 << 8)))
        {
            transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Vector3 placeableObjectPosition = new Vector3();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            transform.position = hit.point;
            // transform.position = Input.mousePosition;

            if (hit.collider.gameObject.name.Contains("TurretBase"))
            {

                turretBaseProperties = hit.collider.gameObject.GetComponent<TurretBase_Properties>();

                if (turretBaseProperties.CheckIfTurretBaseIsEmpty()) 
                {
                    playerController.SetIfBuildable(true);
                    ChangeMaterial(buildableMat);
                    placeableObjectPosition = hit.collider.gameObject.transform.position;
                }
                else 
                {
                    playerController.SetIfBuildable(false);
                    ChangeMaterial(blockedBuildableMat);
                    placeableObjectPosition = new Vector3();
                }
            }
            else
            {
                playerController.SetIfBuildable(false);
                ChangeMaterial(blockedBuildableMat);
            }            
        }

        if (Input.GetMouseButtonDown(0) && !isCollided && playerController.CheckIfBuildable())
        {
            playerController.SubtractNewCurrency(buildingCost);
            playerController.SetIfInBuildMode(false);
            turretBaseProperties.SetIfTurretBaseIsEmpty(false);
            Instantiate(prefab, placeableObjectPosition, transform.rotation);
            Destroy(gameObject);

        }
        if (Input.GetMouseButtonDown(1))
        {
            playerController.SetIfInBuildMode(false);
            Destroy(gameObject);
        }
    }

    void ChangeMaterial(Material newMat)
    {
        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = newMat;
            }
            rend.materials = mats;
        }
    }
}
