/** 
 *Copyright(C) 2018 by #COMPANY# 
 *All rights reserved. 
 *FileName:     MapLoader
 *Author:       Passion 
 *Version:      1.0 
 *UnityVersion：2017.1.1f1 
 *Date:         2018-01-23 
 *Description:    
 *History: 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapLoader {
    private const string CsvPathRoot = "Csv/Stage/";

    public static List<List<String>> LoadDataBaseCsv(string path) {

        List<List<String>> csvTable = new List<List<String>>();
        var asset = Resources.Load(CsvPathRoot + path, typeof(TextAsset)) as TextAsset;

        string value = asset.text;

        //分割行
        string[] strLine = value.Split('\n');
        for (int i = 0; i < strLine.Length; i++) {
            string currentStrLine = strLine[i];
            // 忽略行 忽略符号 #
            if (currentStrLine.Substring(0, 1) == "#") {
                continue;
            }

            List<String> currentDatas = currentStrLine.Split(',').ToList();
            csvTable.Add(currentDatas);
        }


        return csvTable;
    }
}