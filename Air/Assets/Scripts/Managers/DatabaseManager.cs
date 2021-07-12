using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : Singleton<DatabaseManager> {
    
    // sql加载
    [SerializeField] private SqlReader _sqlReader;
    
    private Dictionary<string, Dictionary<int, DataBean>> _dataBaseMap;

    public void Init() {
        _dataBaseMap = new Dictionary<string, Dictionary<int, DataBean>>();

        _sqlReader.Load<ScenarioData>(_dataBaseMap, new ScenarioData());
       


        _sqlReader.Close();
        
        
        ScenarioData x = (ScenarioData) GatItemById<ScenarioData>(1);
        
        

    }

    public string GetTableName<T>() where T : DataBean {
        object[] attributes = typeof(T).GetCustomAttributes(true);
        DataTableAttribute attribute = attributes[0] as DataTableAttribute;
        return attribute.tableName;
    }

    public DataBean GatItemById<T>(int id) where T : DataBean {
        return _dataBaseMap[GetTableName<T>()][id];
    }
}