//
//  GameManager#FILEEXTENSION#
//  #PROJECTNAME#
//
//  Created by #SMARTDEVELOPERS# on #CREATIONDATE#.
//
//
 
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	void Awake() {
		SceneManager.Instance.transform.SetParent(this.transform);
	}



}
