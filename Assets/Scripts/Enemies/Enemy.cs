using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    private int damage;
    private int life;
    private float velocity;

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

    public abstract void LevelUP();

    public abstract void Resurrection();

    public abstract void ReceiveDamage(int dmg);

    


}
