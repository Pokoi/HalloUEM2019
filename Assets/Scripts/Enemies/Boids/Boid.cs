//
// File: Boids.cs
// Project: HalloUEM2019
// Description : GameJam Project
// Author: Original File from Sebastian Lague at https://github.com/SebLague/Boids
// Modificated by Jesús Fermín Villar Ramírez (pokoidev)


// MIT License
// © Copyright (C) 2019  pokoidev


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    BoidSettings settings;

    // State
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    // To update:
    Vector3 acceleration;
    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    // Cached
    Material material;
    Transform cachedTransform;
    Transform target;


    void Awake () {
        material        = transform.GetComponentInChildren<MeshRenderer> ().material;
        cachedTransform = transform;

        OnEnable();
        BoidManager.Instance.AddBoidToArray(this);
    }

    private void OnEnable()
    {
        BoidManager.Instance.InitializeBoid(this);
    }

    public void Initialize (BoidSettings settings, Transform target) {
        this.target   = target;
        this.settings = settings;
        
        position = cachedTransform.position;
        forward  = cachedTransform.forward;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity         = transform.forward * startSpeed;
    }

    public void UpdateBoid () 
    {
        Vector3 acceleration = Vector3.zero;
        if (target != null) 
        {
            Vector3 offsetToTarget = (target.position - position);
            acceleration           = SteerTowards (offsetToTarget) * settings.targetWeight;
        }

        if (numPerceivedFlockmates != 0)
        {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce  = SteerTowards (avgFlockHeading) * settings.alignWeight;
            var cohesionForce   = SteerTowards (offsetToFlockmatesCentre) * settings.cohesionWeight;
            var seperationForce = SteerTowards (avgAvoidanceHeading) * settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision ())
        {
            Vector3 collisionAvoidDir   = ObstacleRays ();
            Vector3 collisionAvoidForce = SteerTowards (collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp (speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = dir;
        position = cachedTransform.position;
       // position = new Vector3(cachedTransform.position.x, target.position.y, cachedTransform.position.z);
        forward = dir;

        
    }

    bool IsHeadingForCollision () {
        RaycastHit hit;
        if (Physics.SphereCast (position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDst, settings.obstacleMask)) {
            return true;
        } else { }
        return false;
    }

    Vector3 ObstacleRays () {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++) {
            Vector3 dir = cachedTransform.TransformDirection (rayDirections[i]);
            Ray ray = new Ray (position, dir);
            if (!Physics.SphereCast (ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask)) {
                return dir;
            }
        }

        return forward;
    }

    Vector3 SteerTowards (Vector3 vector) {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude (v, settings.maxSteerForce);
    }

}