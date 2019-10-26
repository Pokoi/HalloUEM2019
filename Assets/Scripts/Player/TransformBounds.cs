using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBounds
{

    Vector3 size;
    Transform player;

    public TransformBounds(MeshCollider meshCollider, Transform _player)
    {

        size = (meshCollider.bounds.extents) + meshCollider.transform.position;

        player = _player;
        Debug.Log(size);
       
    }


    public void update()
    {

        var pos = player.position;

        pos.x = Mathf.Clamp(player.position.x, -size[0], size[0]);
        pos.z = Mathf.Clamp(player.position.z, -size[2], size[2]);

        player.position = pos;

    }
}
