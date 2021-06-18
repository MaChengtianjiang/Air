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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SceneManager : Singleton<SceneManager> {
    [SerializeField] private GameObject frontObj;

    [SerializeField] private DiceController diceController;


    private SceneStatus _sceneStatus = SceneStatus.Loading;

    internal int stageWidth { get; private set; } = 0;
    internal int stageHeight { get; private set; } = 0;

    internal StageCell[,] stageMap { get; private set; }


    private Player player;
    private StageUIController stageUIController;

    public void SetPlayer(Player player) {
        this.player = player;
    }
    
    public void SetUIController(StageUIController controller) {
        this.stageUIController = controller;
    }
    
    
    public IEnumerator Init(String stageName) {
        

        
        // TODO 优化成异步处理
        LoaderStage(stageName);
        
        // 等待各独立对象完成初始化
        while (player != null) {
            yield return null;
        }
        
        while (stageUIController != null) {
            yield return null;
        }
        
        // 初始化骰子
        diceController.Init();

        _sceneStatus = SceneStatus.Stand;
    }

    public bool isReady() {
        return _sceneStatus != SceneStatus.Loading;
    }


    void Update() {
        if (_sceneStatus == SceneStatus.Loading) {
            return;
        }

        _JudgeStatus();
    }



    /**
     * 状态判断
     */
    void _JudgeStatus() {
        switch (_sceneStatus) {
            case SceneStatus.Stand:
                stageUIController.ShowUI();
                break;
            case SceneStatus.RollDice:
                if (!Dice.rolling) {
                    _sceneStatus = SceneStatus.Run;
                    Debug.Log(String.Format("Dice.Count:{0}", Dice.Value("d6")));
                    StartCoroutine(player.Run(Dice.Value("d6")));
                }

                break;
            case SceneStatus.Run:
                if (player.isStop()) {
                    _sceneStatus = SceneStatus.InCell;
                }

                break;
            case SceneStatus.InCell:
                // 先进入到下一轮
                _sceneStatus = SceneStatus.Stand;
                break;
        }
    }


    /**
     * 读取场景
     */
    public void LoaderStage(String stageName) {
        List<List<String>> map = MapLoader.LoadDataBaseCsv(stageName);

        // 反转y轴(不然为逆向)
        map.Reverse();

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
                    case "":
                    case null:
                    case "0":
                        continue;
                        break;
                    case "S":
                        // 起点
                        break;
                    default:
                        var go = frontObj;
                        go.transform.position = new Vector3(x, y, 0);
                        GameObject temp = Instantiate(go, this.transform, true) as GameObject;
                        temp.name = $"front({x}, {y})";
                        stageMap[x, y] = temp.GetComponent<StageCell>();
                        break;
                }
            }
        }
        
    }


    public void RollDice(StageUIController stageUIController) {
        diceController.Roll(
            () => {
                _sceneStatus = SceneStatus.RollDice;
                stageUIController.HideUI();
            });
    }

}