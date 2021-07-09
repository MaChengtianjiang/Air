using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using Mono.Data.Sqlite;
using UnityEngine;


public class ScenarioData : DataBean {
    // 事件对应文件名
    public string file { get; set; }

    // 事件说明
    public string desc { get; set; }

    // 事件类型
    public EventType type { get; set; }

    // 事件状态(锁定，解锁，已结束)
    public EventStatus status { get; set; }

    // 解锁条件1
    public EventCondition? condition1 { get; set; }

    // 解锁条件1的值
    public int? conditionValue1 { get; set; }

    // 解锁条件2
    public EventCondition? condition2 { get; set; }

    // 解锁条件2的值
    public int? conditionValue2 { get; set; }

    // 解锁条件3
    public EventCondition? condition3 { get; set; }

    // 解锁条件3的值
    public int? conditionValue3 { get; set; }

    public ScenarioData() {

    }

    public static ScenarioData Test(int type) {
        switch (type) {
            case 1:
                return TestBefore();
            default:
                return new ScenarioData();
        }
    }

    public static ScenarioData TestBefore() {
        var temp = new ScenarioData();
        temp.id = 1;
        temp.desc = "Demo/测试剧本1";
        temp.file = "Test1";
        temp.type = EventType.Before;
        temp.status = EventStatus.Unlock;
        temp.condition1 = EventCondition.Str;
        temp.conditionValue1 = 10;
        return temp;
    }


    private static readonly List<Type> enumList = new List<Type>() {
        typeof(EventType),
        typeof(EventStatus),
        typeof(EventCondition),
    };

    /**
     * 复写这个玩意得到类
     */
    public override DataBean parseTableData(SqliteDataReader reader) {
        var temp = new ScenarioData();
        setValue(ref temp, reader);
        return temp;
    }

    protected override void setEnum<ScenarioData>(ref ScenarioData temp, PropertyInfo item, SqliteDataReader reader) {
        if (item.PropertyType == typeof(EventType)) {
            item.SetValue(temp, parseEnum<EventType>(reader, item.Name));
        }
        else if (item.PropertyType == typeof(EventStatus)) {
            item.SetValue(temp, parseEnum<EventStatus>(reader, item.Name));
        }
        else if (item.PropertyType == typeof(EventCondition)) {
            item.SetValue(temp, parseEnum<EventCondition>(reader, item.Name));
        }
    }
}