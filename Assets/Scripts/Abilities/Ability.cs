using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private int level;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    public float cooldown;

    public virtual void LevelUp() { }

    public virtual void ActivateAbility() { }

    public virtual float percetajeCD() { return 0; }

    private bool available = true;

    public bool Available
    {
        get
        {
            return available;
        }
        set
        {
            available = value;
        }
    }
}
