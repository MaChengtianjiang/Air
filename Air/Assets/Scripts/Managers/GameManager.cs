/** 
 *Copyright(C) 2018 by #COMPANY# 
 *All rights reserved. 
 *FileName:     #SCRIPTFULLNAME# 
 *Author:       #AUTHOR# 
 *Version:      #VERSION# 
 *UnityVersion：#UNITYVERSION# 
 *Date:         #DATE# 
 *Description:    
 *History: 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	void Awake() {
		Dictionary<int, Dictionary<string, string>> charaTable = CsvLoader.LoadDataBaseCsv("CharaDatas");
		SceneManager.Instance.transform.SetParent(this.transform);
	}



}
