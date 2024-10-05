using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public TextMeshProUGUI EndMessageText;
    public TextMeshProUGUI LevelValue;
    public TextMeshProUGUI TotalExpValue;
    public TextMeshProUGUI ZombiesKValue;
    public TextMeshProUGUI BrutesKValue;
    public TextMeshProUGUI TimeSurvivedValue;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerWin(int level, int exp, int zomb, int brute, float time)
    {
        this.gameObject.SetActive(true);
        EndMessageText.text = "VICTORY! ALL LEVELS CLEARED!";
        SetLevelValueText(level);
        SetExpValueText(exp);
        SetEnemiesKText(zomb, brute);
        SetTimeSurvivedText(time);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void GameOver(int level, int exp, int zomb, int brute, float time)
    {
        this.gameObject.SetActive(true);
        EndMessageText.text = "YOU DIED!";
        SetLevelValueText(level);
        SetExpValueText(exp);
        SetEnemiesKText(zomb, brute);
        SetTimeSurvivedText(time);
        Time.timeScale = 0;
        AudioListener.pause = true;
        

    }

    public void Retry()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("Car Scene");
    }

    public void SetLevelValueText(int level)
    {
    
        LevelValue.text = level.ToString();
    }

    public void SetExpValueText(int exp)
    {
        TotalExpValue.text =  exp.ToString();
    }

    public void SetEnemiesKText(int zomb, int brute)
    {
        ZombiesKValue.text = zomb.ToString();
        BrutesKValue.text = brute.ToString();
    }

    public void SetTimeSurvivedText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);  // Get the number of minutes
        int seconds = Mathf.FloorToInt(time % 60);  // Get the remaining seconds
       

        
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        
        Debug.Log(formattedTime); // Example output: "02:05"
       

        TimeSurvivedValue.text = formattedTime;
    }
}
