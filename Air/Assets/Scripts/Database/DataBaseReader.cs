/****************************************************
* Unity版本：2019.4.11f1
* 文件：DbController.cs
* 作者：tottimctj
* 邮箱：tottimctj@163.com
* 日期：2021/07/08 16:34:09
* 功能：Nothing
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

public class DataBaseReader {
    Dictionary<string, DataBean> dataTableMap = new Dictionary<string, DataBean> {
        {"demo", new ScenarioData()}
    };


    Dictionary<string, Dictionary<int, DataBean>> dataBase = new Dictionary<string, Dictionary<int, DataBean>>();


    string DatabasePath = "/Resources/dataBase/demo.db";
    private SqliteConnection connection;

    void Init() {
        connection = new SqliteConnection("Data Source=" + String.Format("{0}{1}", Application.dataPath, DatabasePath));
        connection.Open();

        foreach (var keyValuePair in dataTableMap) {
            dataBase.Add(keyValuePair.Key, new Dictionary<int, DataBean>());
            LoaderData(keyValuePair.Key, dataTableMap[keyValuePair.Key]);
        }

        Debug.Log("SqlLite ReadFinish");
    }

    void LoaderData(String tableName, DataBean data) {
//执行数据库操作
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM " + tableName;
        SqliteDataReader reader = command.ExecuteReader();

        int count = 1;
        while (reader.Read()) {
            var temp = data.parseTableData(reader);
            dataBase[tableName].Add(temp.id, temp);
        }

        reader.Close();
    }
}