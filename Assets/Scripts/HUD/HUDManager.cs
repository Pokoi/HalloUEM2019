using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

   public AbilitiesManager abilitiesManager;

    public Image[] spellsCD;


    private void Update()
    {
        foreach(var img in spellsCD)
        {

        }
        Debug.Log(abilitiesManager.abilities[0].percetajeCD());

    }
}
