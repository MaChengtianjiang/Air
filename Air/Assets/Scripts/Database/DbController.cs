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

public class DbController : MonoBehaviour {
    string DatabasePath = Application.dataPath + "/Resources/DataBase/demo.db";
    private SqliteConnection connection;


    void Init() {
        connection = new SqliteConnection("Data Source=" + DatabasePath);
        connection.Open();
    }

    void GetData<T>(string tableName,  T data) where T : DataBean {
        //执行数据库操作
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM node";
        SqliteDataReader reader = command.ExecuteReader();
        int count = 1;
        while (reader.Read()) {
            // //读取ID
            // print("id:" + count);
            // // reader.GetData();
            // Debug.Log(reader.GetInt32(reader.GetOrdinal("id")));
            // //读取Name
            // Debug.Log(reader.GetString(reader.GetOrdinal("name")));
            //

            var temp = new DataBean<ScenarioData>();
            temp.id = reader.GetInt32(reader.GetOrdinal("id"));
            data.GetType();
            
            
            count++;
        }

        reader.Close();
    }
}