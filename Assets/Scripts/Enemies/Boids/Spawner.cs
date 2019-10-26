//
// File: Spawner.cs
// Project: HalloUEM2019
// Description : GameJam Project
// Author: Original File from Sebastian Lague at https://github.com/SebLague/Boids
// Modificated by Jesús Fermín Villar Ramírez (pokoidev)


// MIT License
// © Copyright (C) 2019  pokoidev

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Boid prefab;
    public float spawnRadius = 10;
    public int   spawnCount  = 10;
    
    void Awake () {
        
        for (int i = 0; i < spawnCount; i++) 
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            
            //Reset the y offset position to 2D movement
            pos.y       = BoidManager.Instance.target.position.y;
            
            Boid boid   = Instantiate (prefab);
            boid.transform.position = pos;
            boid.transform.forward = Random.insideUnitSphere;

            BoidManager.Instance.AddBoidToArray(ref boid);
        }
    }
   

}