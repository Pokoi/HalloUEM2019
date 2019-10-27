//
// File: BoidManager.cs
// Project: HalloUEM2019
// Description : GameJam Project
// Author: Original File from Sebastian Lague at https://github.com/SebLague/Boids
// Modificated by Jesús Fermín Villar Ramírez (pokoidev)


// MIT License
// © Copyright (C) 2019  pokoidev

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {

    const int threadGroupSize = 1024;

    public BoidSettings  settings;
    public ComputeShader compute;
    public Transform target; 
    List<Boid> boids = new List<Boid>();

    public static BoidManager Instance { get { return instance; } }
    static BoidManager instance;

    private void Awake()
    {
        if (Instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    public void RemoveBoid( Boid b)
    {
        b.velocityModifier = 0;
    }
    public void InitializeBoid(Boid b)
    {
        b.Initialize(settings, target);
    }

    public void AddBoidToArray(Boid b)
    {
        boids.Add(b);
    }

    void Update () 
    {
        if (boids != null) 
        {

            int numBoids = boids.Count;
            var boidData = new BoidData[numBoids];

            for (int i = 0; i < boids.Count; i++) {
                boidData[i].position  = boids[i].position;
                boidData[i].direction = boids[i].forward;
            }

            var boidBuffer = new ComputeBuffer (numBoids, BoidData.Size);
            boidBuffer.SetData (boidData);

            compute.SetBuffer (0, "boids", boidBuffer);
            compute.SetInt    ("numBoids", boids.Count);
            compute.SetFloat  ("viewRadius", settings.perceptionRadius);
            compute.SetFloat  ("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt (numBoids / (float) threadGroupSize);
            compute.Dispatch (0, threadGroups, 1, 1);

            boidBuffer.GetData (boidData);

            for (int i = 0; i < boids.Count; i++) {
                boids[i].avgFlockHeading        = boidData[i].flockHeading;
                boids[i].centreOfFlockmates     = boidData[i].flockCentre;
                boids[i].avgAvoidanceHeading    = boidData[i].avoidanceHeading;
                boids[i].numPerceivedFlockmates = boidData[i].numFlockmates;

                boids[i].UpdateBoid ();
            }

            boidBuffer.Release ();
        }
    }

    public struct BoidData 
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int     numFlockmates;

        public static int Size {
            get {
                return sizeof (float) * 3 * 5 + sizeof (int);
            }
        }
    }

    public bool ContainsBoid(ref Boid b)
    {
        return boids.Contains(b);
    }
}