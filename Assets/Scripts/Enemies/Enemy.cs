using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int damage;
    public int life;

    public float secondsUntilResurrection = 3f;

    public float detonationTime = .5f;

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
        
    }

    public virtual void ReceiveDamage(int dmg) { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null) StartCoroutine("Explode", detonationTime);
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
        yield return new WaitForSeconds(seconds);
        Resurrection();        
    }
}
