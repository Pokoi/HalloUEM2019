// +----------------------------------------------------------------------------+
// | Created by Alejandro Benitez Lopez, benitezdev@gmail.com (benitezdev.com)  |
// | On 26th of October 2019                                                    |
// | Copyright © 2019 Benitezdev. Creative Commons License:                     |
// | Attribution 4.0 International (CC BY 4.0)                                  |
// +----------------------------------------------------------------------------+


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : Ability
{

    private float startTime = 0;
    [SerializeField] float decrementCDperLv = 1f;


    void Update()
    {
        Available = Time.time > startTime + cooldown;
    }

    public override void LevelUp()
    {
        cooldown -= decrementCDperLv;
    }

    public override void ActivateAbility()
    {
        startTime = Time.time;

        AudioSourceManager.Instance.source.clip = AudioSourceManager.Instance.wc;
        AudioSourceManager.Instance.source.Play();


        KillEnemiesBasic(WaveController.Instance.basicEnemyPool);
        KillEnemiesFast(WaveController.Instance.fastEnemyPool);
        KillEnemiesTank(WaveController.Instance.tankEnemyPool);
    }


    
    //Looks for all active EnemyBasic zombie and returns it.
    private void KillEnemiesBasic(List<EnemyBasic> list)
    {
        int i = 0;
        while (i < list.Count)
        {
            if (list[i].gameObject.activeSelf)
            {
                list[i].KillBySwamp();
            }
            else i++;
        }
    }

    //Looks for all active EnemyFast zombie and returns it.
    private void KillEnemiesFast(List<EnemyFast> list)
    { 
        int i = 0;
        while (i < list.Count)
        {
            if (list[i].gameObject.activeSelf)
            {
                list[i].KillBySwamp();
            }
            else i++;
        }
    }

    //Looks for all active EnemyTank zombie and returns it.
    private void KillEnemiesTank(List<EnemyTank> list)
    {
        int i = 0;
        while (i < list.Count)
        {
            if (list[i].gameObject.activeSelf)
            {
                list[i].KillBySwamp();
            }
            else i++;
        }
    }

    public override float percetajeCD()
    {

        if (Available) return 0.6f;
        return Mathf.Clamp((Time.time - startTime) / cooldown, 0, 0.6f);
    }
}