//
// File: BoidSettings.cs
// Project: HalloUEM2019
// Description : GameJam Project
// Author: Original File from Sebastian Lague at https://github.com/SebLague/Boids
// Modificated by Jesús Fermín Villar Ramírez (pokoidev)


// MIT License
// © Copyright (C) 2019  pokoidev

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidSettings : ScriptableObject {
    // Settings
    public float minSpeed = 2;
    public float maxSpeed = 10;
    public float perceptionRadius = 2f;
    public float avoidanceRadius = 322.3f;
    public float maxSteerForce = 13;

    public float alignWeight = 115.4f;
    public float cohesionWeight = 1;
    public float seperateWeight = 50;

    public float targetWeight = 2;

    [Header ("Collisions")]
    public LayerMask obstacleMask;
    public float boundsRadius = .1f;
    public float avoidCollisionWeight = 500;
    public float collisionAvoidDst = 500;

}