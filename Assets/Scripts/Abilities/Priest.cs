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

    private MeshRenderer meshRenderer;

    public GameObject light;

    public Transform spawn;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

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
        AudioSourceManager.Instance.source.clip = AudioSourceManager.Instance.priest;
        AudioSourceManager.Instance.source.Play();
        GameObject go = Instantiate(prior,spawn);
        GameObject particles = Instantiate(fx);
        light.SetActive(true);

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
        light.SetActive(false);
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

    public override float percetajeCD()
    {
        
        if(Available) return 0.6f;
        return Mathf.Clamp((Time.time - startTime)/cooldown,0,0.6f);


    }
}
