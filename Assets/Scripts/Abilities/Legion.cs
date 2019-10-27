//
// File: Legion.cs
// Project: HalloUEM2019
// Description : GameJam Project
// Author: Jesús Fermín Villar Ramírez (pokoidev)


// MIT License
// © Copyright (C) 2019  pokoidev

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legion : Ability
{

    public float    duration        = 2.5f;
    public int      initialShots  = 3;
    public int      incrByLevel     = 2;
    public float    damage          = 20;
    private float   startTime       = 0;
    private int     shots;
    public BoxCollider spawnArea;

    public GameObject bomb_prefab;

    private List<Bomb> bombsPool = new List<Bomb>();


    private void Awake()
    {
        shots = initialShots;
    }
    // Update is called once per frame
    void Update()
    {
        Available = Time.time > startTime + cooldown;
    }

    public override void ActivateAbility()
    {
        StartCoroutine("CondorLegion");
    }

    IEnumerator CondorLegion()
    {
        float tick      = duration / (shots - 1);
        float timeStamp = Time.time;

        startTime = Time.time;

        while (true)
        {
            if (Time.time > timeStamp + duration) break;
            Shot();
            yield return new WaitForSeconds(tick);
        }

        yield return null;
    }

    private void Shot()
    {
        Bomb b = GetBomb();

        if(b != null)
        {
            b.transform.position = GetRandomPosition();
            b.gameObject.SetActive(true);
            b.StartCoroutine("Falling");
        }
       
    }

    Bomb GetBomb()
    {
        int i = 0;

        while (i < bombsPool.Count)
        {
            if (!bombsPool[i].gameObject.activeSelf) return bombsPool[i];
            else i++;
        }

        GameObject go = Instantiate(bomb_prefab);
        bombsPool.Add(go.GetComponent<Bomb>());
        return go.GetComponent<Bomb>();
    }

    Vector3 GetRandomPosition() 
    {
        return spawnArea.bounds.center + new Vector3(
           (Random.value - 0.5f) * spawnArea.bounds.size.x,
           20,
           (Random.value - 0.5f) * spawnArea.bounds.size.z
        );
    }

    public override void LevelUp()
    {
        shots += incrByLevel;
        duration += (incrByLevel * 0.5f);
    }

    public override float percetajeCD()
    {

        if (Available) return 0.6f;
        return Mathf.Clamp((Time.time - startTime) / cooldown, 0, 0.6f);
    }
}
