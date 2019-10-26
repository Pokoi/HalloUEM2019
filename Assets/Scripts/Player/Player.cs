using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Transform weaponCannon;


    [Space(10)]
    [Header("Movement:")]
    [SerializeField] public Transform trMesh;
    [SerializeField] LayerMask layerPlane;

    PlayerMovement movement;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;       
        movement = new PlayerMovement(this.transform, trMesh, layerPlane);


    }


   

    private void Update()
    {
        movement.update();
    }

}
