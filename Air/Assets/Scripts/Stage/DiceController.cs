using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour {


    // 生成点的坐标
    [SerializeField] private Vector3 _spawnPos = new Vector3(-100, 11, -10);
    [SerializeField] private Vector3 _rollTargetPos = Vector3.zero;


    // 骰子的皮肤
    private string _galleryDie = "d6-white";
    

    public void CreateSpawn() {

        _rollTargetPos = _rollTargetPos + new Vector3(0, -9.5f, 0);
    }

    /**
      * 骰子的方向
      */
    private Vector3 DiceForce() {
        return Vector3.Lerp(_spawnPos, _rollTargetPos, 1).normalized;
    }


    public void Roll(Action callback) {

        if (Dice.rolling) {
            return;
        }

        if (Input.GetMouseButtonDown(Dice.MOUSE_RIGHT_BUTTON)) {
            Dice.Clear();
            string[] a = _galleryDie.Split('-');
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            Dice.Roll("1" + a[0], _galleryDie, _spawnPos, DiceForce());

            callback();
        }
    }
}