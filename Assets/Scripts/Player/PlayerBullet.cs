
// +----------------------------------------------------------------------------+
// | Created by Alejandro Benitez Lopez, benitezdev@gmail.com (benitezdev.com)  |
// | On 26th of October 2019                                                    |
// | Copyright © 2019 Benitezdev. Creative Commons License:                     |
// | Attribution 4.0 International (CC BY 4.0)                                  |
// +----------------------------------------------------------------------------+


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] float speed = 20f;


    // Bullet damage in PlayerState.DamageBullet


    private Vector3 dirNorm;
    
    public IEnumerator MoveTo(Vector3 pos, Enemy enemy)
    {
        var dir = pos - Player.instance.weaponCannon.position;
        dirNorm = dir.normalized;

        transform.LookAt(dirNorm);

        while(dir.sqrMagnitude > 1f)
        {
            dir = pos - transform.position;


            transform.position += transform.forward * speed * Time.deltaTime;
            yield return null;
        }

        if(enemy != null)
        {
            enemy.ReceiveDamage(PlayerState.instance.DamageBullet);
        }

        Debug.Log("LLEGO!");
    }


    private void OnDisable()
    {

    }

    //IEnumerator CheckBulletOnScreen()
    //{
        //Vector3 viewPos = cam.WorldToViewportPoint(checkedObject.position);
        //if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        //{
        //    // Your object is in the range of the camera, you can apply your behaviour
        //    isMoving = false;
        //}
        //else
        //    isMoving = true;

    //}


    private void OnEnable()
    {
        transform.position = Player.instance.weaponCannon.position;
        transform.rotation = Player.instance.weaponCannon.rotation;
    }



    private void DisableBullet()
    {
        transform.gameObject.SetActive(false);
    }
}
