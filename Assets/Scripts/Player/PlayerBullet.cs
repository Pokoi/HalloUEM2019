
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

    Vector3 normalizedDir;


     TrailRenderer fxBullet;

    public TrailRenderer getTrailRenderer() { return fxBullet; }

    [SerializeField] float speed = 20f;

    private float lifeTime = 4f;

    private void Awake()
    {
        fxBullet = GetComponentInChildren<TrailRenderer>();

        Invoke("Die",lifeTime);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
    public void moveToTarget(Vector3 dir)
    {
        normalizedDir = dir;
    }
    private void Update()
    {
        transform.position += normalizedDir * speed * Time.deltaTime;
    }

   
}
