using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int damage;
    private int life;
    private float velocity;

    public float secondsUntilResurrection;

    // Cached
    private Boid     cachedBoid;
    private Collider cachedCollider;

    private void Awake()
    {
        cachedBoid     = GetComponent<Boid>();
        cachedCollider = GetComponent<Collider>();
    }

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
        }
    }
    public float Velocity
    {
        get
        {
            return velocity;
        }
        set
        {
            velocity = value;
        }
    }

    public virtual void LevelUP() { }

    public virtual void Resurrection()
    {
        cachedCollider.enabled = true;
        cachedBoid.enabled     = true;
        
    }

    public void OnDead()
    {
        cachedCollider.enabled = false;
        cachedBoid.enabled     = false;

        StartCoroutine("WaitingResurrection", secondsUntilResurrection);
    }

    IEnumerator WaitingResurrection(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Resurrection();        
    }
}
