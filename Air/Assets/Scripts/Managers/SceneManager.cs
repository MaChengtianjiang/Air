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

using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Singleton<SceneManager> {
    [SerializeField] private GameObject frontObj;

    [SerializeField] private DiceController _diceController;


    private bool _isInit = false;


    internal int stageWidth { get; private set; } = 0;
    internal int stageHeight { get; private set; } = 0;

    internal StageCell[,] stageMap {
        get;
        private set; 
    }
    

    private void Start() {
        Debug.Log("SceneManager:Initialize");
    }

    public void Init() {
        _diceController.CreateSpawn();
        LoaderStage("Stage1");

        _isInit = true;
    }


    void Update() {
        if (!_isInit) {
            return;
        }

        if (true) {
            _diceController.Roll();
        }
    }


    /**
     * 读取场景
     */
    public void LoaderStage(String stageName) {
        List<List<String>> map = MapLoader.LoadDataBaseCsv(stageName);

        // map.ForEach(x => Debug.Log(String.Join(",", x)));

        // 高
        stageHeight = map.Count;

        // 遍历获取宽
        foreach (var row in map) {
            stageWidth = stageWidth < row.Count ? row.Count : stageWidth;
        }
        
        stageMap = new StageCell [stageWidth, stageHeight];


        // 获取宽度
        for (int y = 0; y < map.Count; y++) {
            for (int x = 0; x < map[y].Count; x++) {
                switch (map[y][x]) {
                    case "0":
                        continue;
                        break;
                    default:
                        var go = frontObj;
                        go.transform.position = new Vector3(x, -y, 0);
                        GameObject temp = Instantiate(go, this.transform, true) as GameObject;
                        temp.name = $"front({x}, {y})";
                        stageMap[x, y] = temp.GetComponent<StageCell>();
                        break;
                }
            }
        }
        
        Debug.Log("");
    }
}
