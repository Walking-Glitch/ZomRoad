using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public TextMeshProUGUI LevelValue;
    public TextMeshProUGUI TotalExpValue;
    public TextMeshProUGUI ZombiesKValue;
    public TextMeshProUGUI BrutesKValue;
    public TextMeshProUGUI TimeSurvivedValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("Car Scene");
    }
}
