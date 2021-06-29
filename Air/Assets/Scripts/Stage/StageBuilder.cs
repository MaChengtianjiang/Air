using System;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilder : MonoBehaviour {
    
    
    
    const String path = "Prefabs/Stage/";
    
    Dictionary<String, GameObject> cellPrefabMap;



    void LoadCellPrefab() {

        cellPrefabMap = new Dictionary<string, GameObject>();
        
        foreach (CellDefine cell in  Enum.GetValues(typeof(CellDefine))) {
            var go = (GameObject) Resources.Load(path + cell);
            cellPrefabMap.Add(cell.ToString() ,go);
        }

    }
    
    /**
     * 读取场景
     */
    public void LoaderStage(String stageName) {

        LoadCellPrefab();
        

        List<List<String>> map = MapLoader.LoadDataBaseCsv(stageName);

        // 反转y轴(不然为逆向)
        map.Reverse();

        // map.ForEach(x => Debug.Log(String.Join(",", x)));

        // 高
        StageManager.Instance.stageHeight = map.Count;

        // 遍历获取宽
        foreach (var row in map) {
            StageManager.Instance.stageWidth = StageManager.Instance.stageWidth < row.Count
                ? row.Count
                : StageManager.Instance.stageWidth;
        }

        StageManager.Instance.stageMap =
            new StageCell [StageManager.Instance.stageWidth, StageManager.Instance.stageHeight];


        // 获取宽度
        for (int y = 0; y < map.Count; y++) {
            for (int x = 0; x < map[y].Count; x++) {
                

                
                switch (map[y][x]) {
                    case "":
                    case null:
                    case "0":
                        // 不做处理
                        break;
                    default:
                        
                        // TODO 其他样式没画
                        var go = cellPrefabMap["Work"];
                        go.transform.position = new Vector3(x, y, 0);
                        GameObject temp =
                            StageManager.Instantiate(go, StageManager.Instance.transform, true) as GameObject;
                        temp.name = $"front({x}, {y})";
                        StageManager.Instance.stageMap[x, y] = temp.GetComponent<StageCell>();
                        break;
                }
            }
        }
    }
}