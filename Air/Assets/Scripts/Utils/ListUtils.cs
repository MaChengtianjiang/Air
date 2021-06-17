using System;
using System.Collections.Generic;

public class ListUtils {
    
    /**
     * 数组随机排序
     */
    public static List<T> RandomSortList<T>(List<T> ListT) {
        Random random = new Random();
        List<T> newList = new List<T>();
        foreach (T item in ListT) {
            newList.Insert(random.Next(newList.Count + 1), item);
        }

        return newList;
    }
}