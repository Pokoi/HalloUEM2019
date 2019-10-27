using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Ability
{
    public float duration = 2.5f;
    public int percentage = 5; //% of missing hp
    public float ticks = .5f;
    public int incrByLevel = 5;

    public GameObject fx;
    public GameObject prior;

    public Transform spawn;


    private float startTime = 0;

    private void Update()
    {
        Available = Time.time > startTime + cooldown;
        
    }
    public override void ActivateAbility()
    {
        StartCoroutine("PriestEnumerator");
    }

    private IEnumerator PriestEnumerator()
    {
        GameObject go = Instantiate(prior,spawn);
        GameObject particles = Instantiate(fx);

        float currTime = Time.time;

        startTime = Time.time;

        while (true)
        {
            if (Time.time > currTime + duration) break;

            HealPlayer();

            yield return new WaitForSeconds(ticks);
        }


        Destroy(go, 1.5f);
        Destroy(particles, 2f);


        yield return null;

    }

    private void HealPlayer()
    {
        int hpLeft = 100 - PlayerState.instance.Life;

        float healCalc = hpLeft * (percentage / 100f);

       int healQuantity = (int)healCalc;

        if (healQuantity + PlayerState.instance.Life > PlayerState.instance.MaxLife)
            PlayerState.instance.Life = PlayerState.instance.MaxLife;

        else PlayerState.instance.Life += healQuantity;


    }
    public override void LevelUp()
    {
        percentage += incrByLevel;
    }
}
