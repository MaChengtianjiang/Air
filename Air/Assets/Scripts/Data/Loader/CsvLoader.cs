//
//  CsvLoad#FILEEXTENSION#
//  #PROJECTNAME#
//
//  Created by #SMARTDEVELOPERS# on #CREATIONDATE#.
//
//
 
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvLoader<T>  {

	private const string CsvPathRoot = "Csv";

	void LoadCsv(string path, DataBase<T> data) {


		var asset = Resources.Load(CsvPathRoot + path, typeof(TextAsset)) as TextAsset;

		string value = asset.text;

		//分割行
		string[] strLine = value.Split('\n');

		List<string> strText =  new List<string>();

		for (int i = 0; i < strLine.Length; i++) {
			if ()
		}
	}
	
}
