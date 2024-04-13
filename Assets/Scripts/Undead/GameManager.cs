using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region References

    public WheelController wheelController;
    public EnemyManager enemyManager;
    public RoadManager roadManager;
    public StreetPool streetPool;
    public EnemyPool enemyPool;
    public CasingPool casingPool;
    public CasingManager casingManager;

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
        
    }
}
