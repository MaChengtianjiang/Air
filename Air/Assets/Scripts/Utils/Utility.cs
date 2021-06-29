/****************************************************
* Unity版本：2019.4.11f1
* 文件：Utility.cs
* 作者：tottimctj
* 邮箱：tottimctj@163.com
* 日期：2021/06/29 11:16:15
* 功能：Nothing
*****************************************************/

#if !STANDALONE_TOOL
using UnityEngine;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using System;
using StringBuilder = System.Text.StringBuilder;
using CompilerGeneratedAttribute = System.Runtime.CompilerServices.CompilerGeneratedAttribute;

//****************************************************************
/// <summary>
///	常用工具类。<br/>
/// </summary>
//****************************************************************
static class Utility {
	

	//================================================================
	// 一般関連。
	//================================================================
	
	//------------------------------------------------------------
	/// <summary>
	///	Swap处理。<br/>
	/// </summary>
	/// <typeparam name="T">型。</typeparam>
	/// <param name="a">値。</param>
	/// <param name="b">値。</param>
	//------------------------------------------------------------
	public static void Swap<T>(ref T a, ref T b) {
		T tmp = a;
		a = b;
		b = tmp;
	}

	
	//------------------------------------------------------------
	/// <summary>
	///	int型进行比较<br/>
	/// </summary>
	/// <returns>
	/// -1 : left <  right<br/>
	///  1 : left >  right<br/>
	///  0 : left == right<br/>
	/// </returns>
	/// <param name="left">値。</param>
	/// <param name="right">値。</param>
	//------------------------------------------------------------
	public static int IntCompereTo(int left, int right) {
		return (left < right)? -1 : ((left > right)? 1 : 0);
	}
	

}
