using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public void LoadDesktopVersion()
    {
        SceneManager.LoadScene("Car Scene");
    }

    public void LoadMobileVersion()
    {
        SceneManager.LoadScene("Mobile Car Scene");
    }
}
