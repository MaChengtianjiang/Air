using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    private int _currentLeavle = 0;
    private bool _isInitialized = false;


    [SerializeField] private String stageName = "Stage1";


    // 玩家属性相关
    [SerializeField] private PlayerData playerData;
    
    // 游戏属性相关
    [SerializeField] private GameData gameData;


    void Awake() {
        StartCoroutine(this.Initialize());
    }


    //------------------------------------------------------------
    /// <summary>
    /// 初始化
    /// </summary>
    //------------------------------------------------------------
    public IEnumerator Initialize() {
        // 防止重复
        if (_isInitialized == true) {
            yield break;
        }

        // FPS
        QualitySettings.vSyncCount = 0 /* Don't Sync */;
        // 垂直同步
        Application.targetFrameRate = 30;

        //		if (Debug.isDebugBuild) {
        //			DebugMenuManager.Instance.transform.SetParent(this.transform);
        //		}
        // SoundManager.Instance.transform.SetParent(this.transform);
        StageManager.Instance.transform.SetParent(this.transform);
        StartCoroutine(StageManager.Instance.Init(stageName));

        while (!StageManager.Instance.isReady()) {
            yield return null;
        }


        LoadData();


        _isInitialized = true;
        Debug.Log("========== GameManager Initialized ==========");
    }


    void LoadData() {
        // 读档
        if (true) {
            playerData = new PlayerData();
            gameData = new GameData();
        } else {
            
        }
        
        // 根据年龄(回合数)改变场景
    }

    public void AddRound() {
        gameData.Rounds++;
    }

    // public void TestCsvLoader() {
    //     Dictionary<int, DataTable> charaTable = CsvLoader.LoadDataBaseCsv("CharaDatas");
    //     var x = charaTable[5];
    //     Debug.Log("id:" + charaTable[5].GetIntValue("ID") + "的Value:" + x.GetSrtingValue("Job"));
    // }
}