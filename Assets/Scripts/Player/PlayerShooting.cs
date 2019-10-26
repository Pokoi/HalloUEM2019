
// +----------------------------------------------------------------------------+
// | Created by Alejandro Benitez Lopez, benitezdev@gmail.com (benitezdev.com)  |
// | On 26th of October 2019                                                    |
// | Copyright © 2019 Benitezdev. Creative Commons License:                     |
// | Attribution 4.0 International (CC BY 4.0)                                  |
// +----------------------------------------------------------------------------+


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] float fireRate = 0.2f;

    [SerializeField] uint initialPoolSize;
    [SerializeField] GameObject bullet;
    [SerializeField] List<GameObject> bulletPool;

    [SerializeField]
    private float currentTime = 0;

    private void Start()
    {
        // Creating bullets inside the pool
        for(int i = 0; i < initialPoolSize; ++i)
        {
            bulletPool.Add(CreateBullet());
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= fireRate && Input.GetKeyDown(KeyCode.Z))
        {
            currentTime = 0f;
            ShootBullet();
        }
        else if(currentTime >= fireRate)
        {
            currentTime = fireRate;
        }


        //if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    var indexBullet = GetBullet();
        //    var currentBullet = bulletPool[indexBullet].GetComponent<PlayerBullet>();
        //    currentBullet.gameObject.SetActive(true);
        //    currentBullet.StartCoroutine(currentBullet.MoveTo(sitiodisparo.position));
        //}

    }


    private int GetBullet()
    {
        // Search for the first unactive bullet
        var found = false;
        var i = 0;
        var bulletIndex = 0;


        while (!found && i < bulletPool.Count)
        {
            if (!this.bulletPool[i].activeSelf)
            {
                found = true;
                bulletIndex = i;
            }
            else
            {
                ++i;
            }
        }

        if (!found)
        {
            // All the bullets are active: create a new one
            bulletPool.Add(CreateBullet());
            bulletIndex = bulletPool.Count - 1;
        }

        return bulletIndex;
    }


    /// <summary>
    /// Create a inactive bullet
    /// </summary>
    /// <returns>Inactive bullet</returns>
    private GameObject CreateBullet()
    {
        var b = Instantiate(bullet);
        b.SetActive(false);

        return b;
    }


    private void ShootBullet()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            var enemy = hit.collider.gameObject.GetComponent<Enemy>();

            var indexBullet = GetBullet();
            var currentBullet = bulletPool[indexBullet].GetComponent<PlayerBullet>();
            currentBullet.gameObject.SetActive(true);

            currentBullet.StartCoroutine(currentBullet.MoveTo(hit.point, enemy));

           
        }
        
       
    }

}