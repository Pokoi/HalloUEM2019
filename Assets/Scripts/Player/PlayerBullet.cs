using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] float speed = 20f;
    

    // Bullet damage in PlayerState.DamageBullet




    public IEnumerator MoveTo(Vector3 pos)
    {
        var dir = pos - transform.position;
        var dirNorm = dir.normalized;
        
        while(dir.sqrMagnitude > 1f)
        {
            dir = pos - transform.position;
            Debug.Log(dir.sqrMagnitude);
            transform.Translate(dirNorm * speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("LLEGO!");
    }


    private void OnDisable()
    {

    }

    //IEnumerator CheckBulletOnScreen()
    //{
        //Vector3 viewPos = cam.WorldToViewportPoint(checkedObject.position);
        //if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        //{
        //    // Your object is in the range of the camera, you can apply your behaviour
        //    isMoving = false;
        //}
        //else
        //    isMoving = true;

    //}


    private void OnEnable()
    {
        transform.position = Player.instance.weaponCannon.position;
        transform.rotation = Player.instance.weaponCannon.rotation;
    }



    private void DisableBullet()
    {
        transform.gameObject.SetActive(false);
    }
}
