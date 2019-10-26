
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
    [SerializeField] List<GameObject> bulletPool = new List<GameObject>();

    [SerializeField]
    private float currentTime = 0;

    private void Start()
    {
        // Creating bullets inside the pool
        for (int i = 0; i < initialPoolSize; ++i)
        {
            bulletPool.Add(CreateBullet());
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= fireRate && Input.GetButtonDown("Fire1"))
        {
            currentTime = 0f;
            Shoot();
        }
        else if (currentTime >= fireRate)
        {
            currentTime = fireRate;
        }


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

    private GameObject CreateBullet()
    {
        var b = Instantiate(bullet);
        b.SetActive(false);

        return b;
    }


    public float range = 100f;


    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player.instance.weaponCannon.position, Player.instance.weaponCannon.forward, out hit, range))
        {
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                var indexBullet = GetBullet();
                GameObject bull = bulletPool[indexBullet];
                PlayerBullet playerBullet = bull.GetComponent<PlayerBullet>();
                playerBullet.gameObject.SetActive(true);
                playerBullet.transform.position = Player.instance.weaponCannon.position;
                playerBullet.moveToTarget(hit.transform.position, Player.instance.weaponCannon.position);

                //currentBullet.StartCoroutine(currentBullet.MoveTo(enemy.transform.position, enemy));
            }

        }
    }
}