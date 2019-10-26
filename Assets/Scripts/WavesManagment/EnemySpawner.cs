using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Activates a zombie object in this spawner position
    public void CreateZombie(GameObject enemy)
    {
        enemy.transform.position = transform.position;
        enemy.SetActive(true);
    }
}
