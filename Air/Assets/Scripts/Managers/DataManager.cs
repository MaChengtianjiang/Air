using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager> {
    // 玩家属性相关
    public PlayerStatus playerData { private set; get; }

    // 游戏属性相关
    public GameData gameData { private set; get; }

    public void Init() {
        // 读档
        if (true) {
            playerData = new PlayerStatus();
            gameData = new GameData();
            return;
        }

        playerData = new PlayerStatus();
        gameData = new GameData();
    }

    public void AddRound() {
        gameData.Rounds++;

        // 根据年龄(回合数)改变场景
    }
}