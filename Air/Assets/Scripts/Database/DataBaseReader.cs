/****************************************************
* Unity版本：2019.4.11f1
* 文件：DbController.cs
* 作者：tottimctj
* 邮箱：tottimctj@163.com
* 日期：2021/07/08 16:34:09
* 功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class DataBaseReader : MonoBehaviour  {
    private readonly string DatabasePath = "/Resources/dataBase/demo.db";

    Dictionary<string, DataBean> dataTableMap = new Dictionary<string, DataBean> {
        {"scenario", new ScenarioData()}
    };

    Dictionary<string, Dictionary<int, DataBean>> dataBase = new Dictionary<string, Dictionary<int, DataBean>>();


    private SqliteConnection connection;
    private DataSet set;
    

    public void Load() {
        connection = new SqliteConnection("Data Source=" + String.Format("{0}{1}", Application.dataPath, DatabasePath));
        connection.Open();

        foreach (var keyValuePair in dataTableMap) {
            dataBase.Add(keyValuePair.Key, new Dictionary<int, DataBean>());
            LoaderTableData(keyValuePair.Key, dataTableMap[keyValuePair.Key]);
        }

        Debug.Log("DataLoad is ReadFinish");
        connection.Dispose();
        connection.Close();
    }

    /**
     * 获取表数据
     */
    void LoaderTableData<T>(String tableName, T data) where T : DataBean {
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM " + tableName;
        SqliteDataReader reader = command.ExecuteReader();

        while (reader.Read()) {
            var temp = data.parseTableData(reader);
            temp.id = reader.GetInt32(reader.GetOrdinal("id"));
            dataBase[tableName].Add(temp.id, temp);
        }

        reader.Close();
    }
}