using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Transform weaponCannon;

    public float vulnerabilityTime = 1.2f;

    public MeshRenderer capeRender;
    public MeshRenderer minigunRender;
    public SkinnedMeshRenderer shouldersRender;


    [Space(10)]
    [Header("Movement:")]
    [SerializeField] public Transform trMesh;
    [SerializeField] LayerMask layerPlane;

    [SerializeField] MeshCollider playerGround;


    public SkinnedMeshRenderer MeshRenderer;

    PlayerMovement movement;
    TransformBounds bound;

    private bool vulnerable = true;
    public bool Vulnerable
    {
        get
        {
            return vulnerable;
        }
        set
        {
            vulnerable = true;
        }
    }

    public void PlayerHitted()
    {
        StartCoroutine("Blink");
    }

    private IEnumerator Blink()
    {
        var endTime = Time.time + vulnerabilityTime;
        while (Time.time < endTime)
        {
            minigunRender.enabled = false;
            shouldersRender.enabled = false;
            MeshRenderer.enabled = false;
            capeRender.enabled = false;
            yield return new WaitForSeconds(0.2f);
            MeshRenderer.enabled = true;
            capeRender.enabled = true;
            minigunRender.enabled = true;
            shouldersRender.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        vulnerable = true;
    }

   public Animator anim;
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else

        }

        instance = this;       
        movement = new PlayerMovement(this.transform, trMesh, layerPlane,anim);

        bound = new TransformBounds(playerGround, transform);

    }   

    private void Update()
    {
        movement.update();
        bound.update();
    }

}
