using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int damage;
    public int max_life;
    public int pointsForKill = 5;
    int life;
    int level;
    public float levelModifier = 1f;


    public float secondsUntilResurrection = 3f;

    public float detonationTime = .5f;

    private bool defeat;

    public bool Defeat
    {
        get
        {
            return defeat;
        }
        set
        {
            defeat = value;
        }
    }

    // Cached
    private Boid     cachedBoid;
    private Collider cachedCollider;

    private void Awake()
    {
        cachedBoid      = GetComponent<Boid>();
        cachedCollider  = GetComponent<Collider>();
        life            = max_life;
    }     

    public void LevelUPIfNeeded()
    {
        int currentLevel = GameManager.Instance.Level;
        
        if (level != GameManager.Instance.Level)
        {
            LevelUP(currentLevel);
        }
    }

    protected virtual void LevelUP(int level) 
    {
        this.level  = level;
        damage      *= (int) levelModifier;
        max_life    *= (int) levelModifier;
        
        cachedBoid.velocityModifier  *= (levelModifier * 0.5f);
    }

    public void Resurrection()
    {
        defeat = true;
        cachedCollider.enabled = true;
        cachedBoid.enabled     = true;
        life                   = max_life;        
    }

    public void ReceiveDamage(int dmg) 
    {
        this.life -= dmg;
        if (this.life <= 0) OnDead();
    }

    private void OnEnable()
    {
        cachedCollider.enabled = true;
        cachedBoid.enabled     = true;
        life                   = max_life;
        defeat                 = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null) StartCoroutine("Explode", detonationTime);

        PlayerBullet bullet = other.gameObject.GetComponent<PlayerBullet>();
        if (bullet != null)
        {
            ReceiveDamage(PlayerState.instance.DamageBullet);
            bullet.getTrailRenderer().Clear();
            bullet.gameObject.SetActive(false);
        }
    }
    private IEnumerator Explode(float timeToDetonate)
    {
        
        yield return new WaitForSeconds(timeToDetonate);

        //TODO: comprobar si estamos en un radio de X pixeles para hacer daño o no 

        if (Player.instance.Vulnerable)
        {
            PlayerState.instance.Life -= damage;
            Player.instance.PlayerHitted();
            Player.instance.Vulnerable = false;
        }
            
        OnDead();

        yield return null;
    }
    public void OnDead()
    {
        GameManager.Instance.UpdateScore(pointsForKill);

        if (defeat)
        {
            this.gameObject.SetActive(false);
        }
        cachedCollider.enabled = false;
        cachedBoid.enabled     = false;

        BoidManager.Instance.RemoveBoid(cachedBoid);
        Invoke("Resurrection", secondsUntilResurrection);
    }


    public void KillBySwamp()
    {
        if (defeat) GameManager.Instance.UpdateScore(pointsForKill);
        else GameManager.Instance.UpdateScore(pointsForKill * 2);

        this.gameObject.SetActive(false);
    }
}
