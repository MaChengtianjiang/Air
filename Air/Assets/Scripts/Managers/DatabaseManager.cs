using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : Singleton<DatabaseManager> {


    [SerializeField]
    private DataBaseReader _dataBaseReader;
    
    public void Init() {
        
        _dataBaseReader.Load();
    }
    
}
