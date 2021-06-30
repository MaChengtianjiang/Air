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

public class StageManager : Singleton<StageManager> {
    [SerializeField] private DiceController diceController;
    [SerializeField] private StageBuilder _stageBuilder;


    private SceneStatus _sceneStatus = SceneStatus.Loading;

    internal int stageWidth { get; set; } = 0;
    internal int stageHeight { get; set; } = 0;

    internal StageCell[,] stageMap { get; set; }


    private Player player;
    private StageUIController stageUIController;
    private CameraController cameraController;

    public void SetPlayer(Player player) {
        this.player = player;
    }

    public void SetUIController(StageUIController controller) {
        stageUIController = controller;
    }

    public void SetCameraController(CameraController controller) {
        cameraController = controller;
    }

    public void SetCameraPos(Vector3 pos) {
        cameraController.setPos(pos);
    }

    public IEnumerator Init(String stageName) {
        // TODO 优化成异步处理
        _stageBuilder.LoaderStage(stageName);

        // 等待各独立对象完成初始化
        while (player == null) {
            yield return null;
        }

        while (stageUIController == null) {
            yield return null;
        }

        while (cameraController == null) {
            yield return null;
        }

        // 初始化骰子
        diceController.Init();
        stageUIController.setPlayerData();

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


    public void RollDice(StageUIController stageUIController) {
        diceController.Roll(
            () => {
                _sceneStatus = SceneStatus.RollDice;
                stageUIController.HideUI();
            });
    }


    private StageCell _currentCell;

    public void setCurrentCell(StageCell cell) {
        _currentCell = cell;
        Debug.Log("_currentCell:" + _currentCell.getType());
    }
}