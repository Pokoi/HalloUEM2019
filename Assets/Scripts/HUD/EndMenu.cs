using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void PlayAgain() { SceneManager.LoadScene(0); }
    public void Exit() { Application.Quit(); }

}
