using System;
using System.Reflection;
using Mono.Data.Sqlite;
using UnityEngine;

public class DataBean {
    // id
    public int id;

    protected DataBean() {
    }

    /**
     * 表对象映射实体
     */
    public virtual DataBean parseTableData(SqliteDataReader reader) {
        return new DataBean();
    }


    protected void setValue<T>(ref T temp, SqliteDataReader reader) where T : DataBean {
        PropertyInfo[] pros = typeof(T).GetProperties();
        foreach (PropertyInfo item in pros) {
            var result = reader.GetValue(reader.GetOrdinal(item.Name));

            // Debug.Log(String.Format("{0}:{1}", item.Name, result.ToString()));
            if (!string.IsNullOrEmpty(result.ToString())) {
                // 枚举(这里需要各类自定义)
                if (item.PropertyType.BaseType.ToString() == "System.Enum") {
                    temp.setEnum(ref temp, item, reader);
                }
                else if (item.PropertyType == typeof(int)) {
                    // 数值
                    item.SetValue(temp, int.Parse(result.ToString()));
                }
                else if (item.PropertyType == typeof(string)) {
                    // 字符串
                    item.SetValue(temp, result.ToString());
                }
            }
        }
    }

    /**
     * 根据模版类和表字段名获取枚举值
     */
    protected static T parseEnum<T>(SqliteDataReader reader, String colName) {
        return (T) Enum.Parse(typeof(T), reader.GetString(reader.GetOrdinal(colName)));
    }


    /**
     * 表中存在枚举时候获取值方法
     */
    protected virtual void setEnum<T>(ref T temp, PropertyInfo item, SqliteDataReader reader) {
    }
}