/****************************************************
* Unity版本：2019.4.11f1
* 文件：CameraController.cs
* 作者：tottimctj
* 邮箱：tottimctj@163.com
* 日期：2021/06/28 17:26:06
* 功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    private Player _player;

    
    public void Awake() {
        SceneManager.Instance.SetCameraController(this);
    }


    public void setPos(Vector3 pos) {
        transform.position = pos;
    }

}
