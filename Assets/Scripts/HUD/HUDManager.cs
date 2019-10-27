using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

   public AbilitiesManager abilitiesManager;

    public Image[] spellsCD;
    public Image health;

    public Animator animatorSwamp;


    private void Update()
    {
        for (int i = 0; i < spellsCD.Length; ++i)
        {
            spellsCD[i].fillAmount = abilitiesManager.abilities[i].percetajeCD();
        }

        health.fillAmount = ((float)PlayerState.instance.Life / (float)PlayerState.instance.MaxLife);
    }


  

    public void PlaySwampAnimation()
    {
        animatorSwamp.SetTrigger("Pantano");
    }
}
