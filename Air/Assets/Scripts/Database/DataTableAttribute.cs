using System;

/**
 * 获取表名注解
 */
public class DataTableAttribute : Attribute
{
    public DataTableAttribute(string tableName)
    {
        this.tableName = tableName;
    }

    public string tableName { get; set; }
}
