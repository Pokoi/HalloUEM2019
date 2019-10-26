using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    #region
    private static AbilitiesManager instance = null;

    public static AbilitiesManager Instance
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
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public List<Ability> abilities = new List<Ability>();


    private void Update()
    {
        if (abilities[0].Available && Input.GetKeyDown(KeyCode.Alpha1)) abilities[0].ActivateAbility();
        if (abilities[1].Available && Input.GetKeyDown(KeyCode.Alpha2)) abilities[1].ActivateAbility();
        if (abilities[2].Available && Input.GetKeyDown(KeyCode.Alpha3)) abilities[2].ActivateAbility();



    }

}
