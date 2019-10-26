
// +----------------------------------------------------------------------------+
// | Created by Alejandro Benitez Lopez, benitezdev@gmail.com (benitezdev.com)  |
// | On 26th of October 2019                                                    |
// | Copyright © 2019 Benitezdev. Creative Commons License:                     |
// | Attribution 4.0 International (CC BY 4.0)                                  |
// +----------------------------------------------------------------------------+


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    [SerializeField] float speed = 5.0f;

    Transform transformWASD;
    Transform transformMouse;
    LayerMask layerMask;
    public PlayerMovement(Transform _trWASD, Transform _trMouse, LayerMask _plane)
    {
        transformWASD = _trWASD;
        transformMouse = _trMouse;
        layerMask = _plane;
    }


    public void update()
    {
        Vector3 direction = transformWASD.forward * Input.GetAxis("Vertical") +
                            transformWASD.right * Input.GetAxis("Horizontal");

        transformWASD.position += direction * speed * Time.deltaTime;



        ////

        Vector3 mouse = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            var v = new Vector3(hit.point.x, transformMouse.position.y, hit.point.z);
            transformMouse.LookAt(v);
            
        }
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100000);
    }


    
}
