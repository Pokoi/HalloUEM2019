//
// File: Bomb.cs
// Project: HalloUEM2019
// Description : GameJam Project
// Author: Jesús Fermín Villar Ramírez (pokoidev)


// MIT License
// © Copyright (C) 2019  pokoidev

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    int  damage;
    bool exploted = false;
    Transform cached_transform;
    float speed = 25;


    private void Awake()
    {
        cached_transform = transform;
    }
       
    private void OnTriggerEnter(Collider other)
    {
        if (exploted) 
        {
            Enemy e = other.GetComponent<Enemy>();
            if (e)
            {
                e.ReceiveDamage(this.damage);
            }
        }
    }

    IEnumerator Falling() 
    {
        while (cached_transform.position.y >= 0)
        {
            cached_transform.position += -Vector3.up * speed * Time.deltaTime;
            yield return null;
        }

        exploted    = true;
        float ticks = 2;
        
        while (ticks >= 0)
        {
            ticks -= Time.deltaTime;
            yield return Time.deltaTime;
        }

        this.gameObject.SetActive(false);
    }

}
