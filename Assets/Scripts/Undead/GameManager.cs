using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables


    #endregion

    #region References

    public WheelController wheelController;
    public EnemyManager enemyManager;
    public RoadManager roadManager;
    public StreetPool streetPool;
    public EnemyPool enemyPool;
    public ShellCasingPool ShellCasingPool;
    public BulletCasingPool BulletCasingPool;
    public CasingManager casingManager;
    public UIManager uiManager;
    public ConsumablesPool ConsumablesPool;
    public ConsumablesManager consumablesManager;
    public BulletTracePool bulletTracePool;
    public TracerManager tracerManager;
    public Turret Turret;
    public Minigun Minigun;
    public EnergyGun EnergyGun;
    public LoseMenu LoseMenu;

    #endregion
    #region Singleton

    private static GameManager instance;

    private GameManager(){}

    public static GameManager Instance
    {
        get
        {
            if(instance is null)
                Debug.LogError("Game Manager is Null");
            return instance;
        }

    }
    #endregion

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quit();
        Retry();
    }

    void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Retry()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
