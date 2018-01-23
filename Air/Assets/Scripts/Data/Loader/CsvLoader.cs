/** 
 *Copyright(C) 2018 by #COMPANY# 
 *All rights reserved. 
 *FileName:     CsvLoader
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
using UnityEngine;

public class CsvLoader
{

    private const string CsvPathRoot = "Csv/";

    public static Dictionary<int, Dictionary<string, string>> LoadDataBaseCsv(string path)
    {
        var asset = Resources.Load(CsvPathRoot + path, typeof(TextAsset)) as TextAsset;

        string value = asset.text;

        //分割行
        string[] strLine = value.Split('\n');
        bool isLoadTypeEnd = false;
        Dictionary<int, string> dataTypeTable = new Dictionary<int, string>();
		Dictionary<int, Dictionary <string, string>> dataBase = new Dictionary<int, Dictionary<string, string>>();
		int index = -1;

        for (int i = 0; i < strLine.Length; i++)
        {
            string currentStrLine = strLine[i];
            // 忽略行 忽略符号 #
            if (currentStrLine.Substring(0, 1) == "#")
            {
                continue;
            }
            string[] currentDatas = currentStrLine.Split(',');
            // 读取数据
            Dictionary<string, string> data = new Dictionary<string, string>();


            for (int j = 0; j < currentDatas.Length; j++)
            {
                if (!isLoadTypeEnd)
                {
                    //  读取数据类型
                    dataTypeTable.Add(j, currentDatas[j]);

                }
                else
                {
                    // 登录各行数据
					Debug.Log(dataTypeTable[j] + "," + currentDatas[j]);
                    //data.Add(dataTypeTable[j], currentDatas[j]);
					
                }
				
            }
			

            if (!isLoadTypeEnd)
            {
				for(int ii = 0; ii < dataTypeTable.Count; ii++) {
                    // 登录
                    dataBase.Add(ii, null);
				}
                isLoadTypeEnd = true;
            } else {
				dataBase[index] = data;
            }
        }



        return dataBase;
    }


}
