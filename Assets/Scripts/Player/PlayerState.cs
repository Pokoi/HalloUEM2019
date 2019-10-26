using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public static PlayerState instance;

    private int damageBullet = 1;
    public int DamageBullet { get { return damageBullet; } set { damageBullet = value; } }

    private int maxLife = 100;

    public int MaxLife
    {
        get
        {
            return maxLife;
        }
    }
    private int life;
    public int Life { get { return life; } set { life = value; } }


    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }
        instance = this;

        life = MaxLife;
    }




}
