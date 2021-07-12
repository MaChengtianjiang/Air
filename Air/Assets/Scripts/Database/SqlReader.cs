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

public class SqlReader : MonoBehaviour {
    private readonly string DatabasePath = "/Resources/dataBase/demo.db";



    private SqliteConnection connection;
    private DataSet set;


    public void Load<T>(Dictionary<string, Dictionary<int, DataBean>> dataBase, T item) where T : DataBean {
        connection = new SqliteConnection("Data Source=" + String.Format("{0}{1}", Application.dataPath, DatabasePath));
        connection.Open();


        var tableName = DatabaseManager.Instance.GetTableName<T>();
        
        
        dataBase.Add(tableName, new Dictionary<int, DataBean>());
        LoaderTableData(dataBase, tableName, item);

        Debug.Log("DataLoad is ReadFinish");

    }

    public void Close() {
        connection.Dispose();
        connection.Close();
    }

    /**
     * 获取表数据
     */
    void LoaderTableData(Dictionary<string, Dictionary<int, DataBean>> dataBase, String tableName, DataBean data) {
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