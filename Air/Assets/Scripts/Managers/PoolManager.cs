using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager> {
    [SerializeField] private int Capaticy = 99;
    
    // 休眠列表(可以搞若干个做分类)
    private List<GameObject> dormantObjects = new List<GameObject>();
    
    // private Dictionary<String, List<GameObject>> dormantObjectDict = new Dictionary<String, List<GameObject>>();


    public GameObject Spawn(GameObject go) {
        GameObject temp = null;

        // 暂时停止生成
        if (dormantObjects.Count > 0) {
            foreach (GameObject dob in dormantObjects) {
                if (dob.name == go.name) {
                    // Find an availble GameObject.
                    temp = dob;
                    dormantObjects.Remove(temp);
                    return temp;
                }
            }
        }

        temp = GameObject.Instantiate(go) as GameObject;
        temp.name = go.name;
        return temp;
    }


    public void Despawn(GameObject go) {
        go.transform.parent = PoolManager.Instance.transform;
        go.SetActive(false);
        dormantObjects.Add(go);
        Trim();
    }

    // 先进先出
    private void Trim() {
        while (dormantObjects.Count > Capaticy) {
            GameObject dob = dormantObjects[0];
            dormantObjects.RemoveAt(0);
            Destroy(dob);
        }
    }
}