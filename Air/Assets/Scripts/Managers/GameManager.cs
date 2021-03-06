﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    private int _currentLeavle = 0;
    private bool _isInitialized = false;


    [SerializeField] private String stageName = "Stage1";




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
        SoundManager.Instance.transform.SetParent(this.transform);
        StageManager.Instance.transform.SetParent(this.transform);
        DatabaseManager.Instance.transform.SetParent(this.transform);
        DataManager.Instance.transform.SetParent(this.transform);
        ScenarioManager.Instance.transform.SetParent(this.transform);
        
        
        DatabaseManager.Instance.Init();
        DataManager.Instance.Init();

        StartCoroutine(StageManager.Instance.Init(stageName));

        while (!StageManager.Instance.isReady()) {
            yield return null;
        }




        _isInitialized = true;
        Debug.Log("========== GameManager Initialized ==========");
    }




    // public void TestCsvLoader() {
    //     Dictionary<int, DataTable> charaTable = CsvLoader.LoadDataBaseCsv("CharaDatas");
    //     var x = charaTable[5];
    //     Debug.Log("id:" + charaTable[5].GetIntValue("ID") + "的Value:" + x.GetSrtingValue("Job"));
    // }
}