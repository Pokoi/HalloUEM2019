
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
    [SerializeField] List<PlayerBullet> bulletPool = new List<PlayerBullet>();

    [SerializeField]
    private float currentTime = 0;

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


    public float range = 100f;

    public PlayerBullet GetBullet()
    {
        int i = 0;
        while (i < bulletPool.Count)
        {
            if (!bulletPool[i].gameObject.activeSelf) return bulletPool[i].gameObject.GetComponent<PlayerBullet>();
            else i++;
        }
        GameObject go = Instantiate(bullet);
        PlayerBullet bulletComponent = go.GetComponent<PlayerBullet>();
        bulletPool.Add(bulletComponent);
        return bulletComponent;
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player.instance.weaponCannon.position, Player.instance.weaponCannon.forward, out hit, range))
        {



            PlayerBullet playerBullet = GetBullet();
            playerBullet.gameObject.SetActive(true);
            playerBullet.transform.position = Player.instance.weaponCannon.position;
            playerBullet.transform.rotation = Player.instance.weaponCannon.rotation;
            playerBullet.moveToTarget(hit.transform.position, Player.instance.weaponCannon.position);

        }
    }
}