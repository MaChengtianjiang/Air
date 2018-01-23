using System.Collections;
using System.Collections.Generic;
using System;

public class DataBase <T> {


	protected Dictionary<int, T> Table = new Dictionary<int, T>();

	public T Get(int index) {
		return Table[index];
	}
}
