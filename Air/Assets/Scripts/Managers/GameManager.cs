/** 
 *Copyright(C) 2018 by #COMPANY# 
 *All rights reserved. 
 *FileName:     #SCRIPTFULLNAME# 
 *Author:       #AUTHOR# 
 *Version:      #VERSION# 
 *UnityVersion：#UNITYVERSION# 
 *Date:         #DATE# 
 *Description:    
 *History: 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    private int _currentLeavle = 0;
    private bool _isInitialized = false;

    void Awake() {
        StartCoroutine(this.Initialize());
    }


    //------------------------------------------------------------
    /// <summary>
    /// 初始化
    /// </summary>
    //------------------------------------------------------------
    public IEnumerator Initialize() {
        // 既に初期化済みの場合は何もしない
        if (_isInitialized == true) {
            yield break;
        }

        // FPSの設定
        QualitySettings.vSyncCount = 0 /* Don't Sync */;
        // 垂直同步
        Application.targetFrameRate = 30;

        //		if (Debug.isDebugBuild) {
        //			DebugMenuManager.Instance.transform.SetParent(this.transform);
        //		}
        // SoundManager.Instance.transform.SetParent(this.transform);
        SceneManager.Instance.transform.SetParent(this.transform);
        SceneManager.Instance.Init();

        _isInitialized = true;
        Debug.Log("========== GameManager Initialized ==========");
    }
    
    public void TestCsvLoader() {
        Dictionary<int, DataTable> charaTable = CsvLoader.LoadDataBaseCsv("CharaDatas");
        var x = charaTable[5];
        Debug.Log("id:" + charaTable[5].GetIntValue("ID") + "的Value:" + x.GetSrtingValue("Job"));
    }
}