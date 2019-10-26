using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //Singleton init
    #region
    private static WaveController instance = null;

    public static WaveController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    //List of spanw points where enemies will be spawned randomly
    public List<EnemySpawner> spawnerList = new List<EnemySpawner>();

    //Time between enemy instantiation 
    [Range(0.1f, 2.0f)]
    public float spawnTick;

    //Number to choose a random between each type 
    public int nEnemiesTypes = 3;

    //List of each enemy for pooling
    public List<EnemyBasic> basicEnemyPool = new List<EnemyBasic>();
    public List<EnemyFast> fastEnemyPool = new List<EnemyFast>();
    public List<EnemyTank> tankEnemyPool = new List<EnemyTank>();


    [Header("Enemies Prefabs")]
    //Enemies prefabs
    public GameObject basicEnemy;
    public GameObject fastEnemy;
    public GameObject tankEnemy;


    private void Start()
    {
        StartCoroutine("ZombieGeneration");

       
    }


    //Generates a zombie after spawnTick time is elapsed
    IEnumerator ZombieGeneration()
    {
        while (true)
        {

           //int level = GameManager.Instance.Level;

            int randSpawner = Random.Range(0, spawnerList.Count);
            int enemyType = 0; //Random.Range(0, nEnemiesTypes); //Random type //TODO: CAMBIARLO A RANDOM Y PONER BIEN LOS PREFABS

            //Obtain enemy from pool of the random type 
            //Update enemy stats based on the game level
            //Enemy is passed to the spawner to move it to its position and activate it

            if (enemyType == 0)
            {
                EnemyBasic enemy = GetPoolBasic(basicEnemyPool);
                enemy.LevelUPIfNeeded();
                spawnerList[randSpawner].CreateZombie(enemy.gameObject);
            }
            else if (enemyType == 1)
            {
                EnemyFast enemy = GetPoolFast(fastEnemyPool);
                enemy.LevelUPIfNeeded();
                spawnerList[randSpawner].CreateZombie(enemy.gameObject);
            }
            else if (enemyType == 2)
            {
                EnemyTank enemy = GetPoolTank(tankEnemyPool);
                enemy.LevelUPIfNeeded();
                spawnerList[randSpawner].CreateZombie(enemy.gameObject);
            }

            yield return new WaitForSeconds(spawnTick);

            yield return null;
        }
    }


    //Looks for a disabled BASIC zombie and returns it , if no one is found it creates it and add the zombie to the list 
    private EnemyBasic GetPoolBasic(List<EnemyBasic> list)
    {
        int i = 0;
        while (i < list.Count)
        {
            if (!list[i].gameObject.activeSelf) return list[i].gameObject.GetComponent<EnemyBasic>();
            else i++;
        }
        GameObject go = Instantiate(basicEnemy);
        EnemyBasic enemy = go.GetComponent<EnemyBasic>();
        basicEnemyPool.Add(enemy);
        return enemy;

    }
    //Looks for a disabled FAST zombie and returns it , if no one is found it creates it and add the zombie to the list 
    private EnemyFast GetPoolFast(List<EnemyFast> list)
    {

        int i = 0;
        while (i < list.Count)
        {
            if (!list[i].gameObject.activeSelf) return list[i].gameObject.GetComponent<EnemyFast>();
            else i++;
        }
        GameObject go = Instantiate(basicEnemy);
        EnemyFast enemy = go.GetComponent<EnemyFast>();
        fastEnemyPool.Add(enemy);
        return enemy;

    }

    //Looks for a disabled TANK zombie and returns it , if no one is found it creates it and add the zombie to the list 
    private EnemyTank GetPoolTank(List<EnemyTank> list)
    {
        
        int i = 0;
        while (i < list.Count)
        {
            if (!list[i].gameObject.activeSelf) return list[i].gameObject.GetComponent<EnemyTank>();
            else i++;
        }
        GameObject go = Instantiate(basicEnemy);
        EnemyTank enemy = go.GetComponent<EnemyTank>();
        tankEnemyPool.Add(enemy);
        return enemy;

    }

}
