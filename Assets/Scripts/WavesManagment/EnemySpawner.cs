using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Activates a zombie object in this spawner position
    public void CreateZombie(GameObject enemy)
    {
        Vector3 desiredPosition  = new Vector3(transform.position.x, BoidManager.Instance.target.position.y, transform.position.z);
        enemy.transform.position = desiredPosition;

        enemy.SetActive(true);
    }
}
