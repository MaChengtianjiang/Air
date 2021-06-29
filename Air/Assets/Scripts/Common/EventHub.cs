/****************************************************
* Unity版本：2019.4.11f1
* 文件：EventHub.cs
* 作者：tottimctj
* 邮箱：tottimctj@163.com
* 日期：2021/06/29 10:37:29
* 功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHub {
    public static void ParseCell(StageCell cell) {
        switch (cell.getType()) {
            case CellDefine.Game:
                NavMiniGame();
                break;
            case CellDefine.Battle:
                break;
            case CellDefine.Mission:
                break;
            case CellDefine.Work:
                break;
            case CellDefine.Shop:
                break;
            case CellDefine.Event:
                break;
            case CellDefine.Financial:
                break;
            case CellDefine.Travel:
                break;
            case CellDefine.Social:
                break;
        }
    }

    static void NavMiniGame() {
        
        // TODO * 体能 打地鼠
        // TODO * 智力 神经衰弱
        // TODO * 道德 找茬
        // TODO * 魅力 简单跑酷
    }
}