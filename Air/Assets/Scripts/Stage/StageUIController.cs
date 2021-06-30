/****************************************************
* Unity版本：2019.4.11f1
* 文件：StageUIController.cs
* 作者：tottimctj
* 邮箱：tottimctj@163.com
* 日期：2021/06/17 15:02:01
* 功能：Nothing
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIController : MonoBehaviour {
    private delegate void RollDiceFunc(StageUIController stageUIController);

    private RollDiceFunc _rollDiceFunc;


    [SerializeField] private GameObject _diceRender;
    [SerializeField] private GameObject _rollButton;

    [SerializeField] private Text playerDataUI;


    public void Awake() {
        StageManager.Instance.SetUIController(this);
        _rollDiceFunc = StageManager.Instance.RollDice;
    }


    public void OnRollDice() {
        _rollDiceFunc(this);
    }

    public void ShowUI() {
        _diceRender.SetActive(false);
        _rollButton.SetActive(true);
    }

    public void HideUI() {
        _diceRender.SetActive(true);
        _rollButton.SetActive(false);
    }

    public void setPlayerData() {
        var data = DataManager.Instance.playerData;
        playerDataUI.text = String.Format("体能:{0} 智力:{1} 道德:{2} 魅力:{3}", data.Str, data.Int, data.Moral, data.Charm);
    }
}