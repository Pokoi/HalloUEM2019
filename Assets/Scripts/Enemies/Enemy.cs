using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int damage;
    public int life;

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
        cachedBoid     = GetComponent<Boid>();
        cachedCollider = GetComponent<Collider>();
    }

    public virtual void LevelUP() { }

    public virtual void Resurrection()
    {
        cachedCollider.enabled = true;
        cachedBoid.enabled     = true;
        defeat = false;
        
    }

    public void ReceiveDamage(int dmg) 
    {
        this.life -= dmg;
        if (this.life <= 0) OnDead();
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
        
        PlayerState.instance.Life -= damage;
        OnDead();

        yield return null;
    }
    public void OnDead()
    {
        cachedCollider.enabled = false;
        cachedBoid.enabled     = false;

        BoidManager.Instance.RemoveBoid(cachedBoid);

        StartCoroutine("WaitingResurrection", secondsUntilResurrection);
    }

    IEnumerator WaitingResurrection(float seconds)
    {
        defeat = true;
        yield return new WaitForSeconds(seconds);
        Resurrection();        
    }
}
