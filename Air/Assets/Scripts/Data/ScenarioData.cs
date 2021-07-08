using Mono.Data.Sqlite;

[System.Serializable]
public class ScenarioData : DataBean<ScenarioData> {
   

    // 事件说明
    internal string desc;

    // 事件对应文件名
    internal string file;

    // 事件类型
    internal EventType type;

    // 事件状态(锁定，解锁，已结束)
    internal EventStatus status;

    // 解锁条件1
    internal EventCondition condition1;

    // 解锁条件1的值
    internal int conditionValue1;

    // 解锁条件2
    internal EventCondition condition2;

    // 解锁条件2的值
    internal int conditionValue2;

    // 解锁条件3
    internal EventCondition condition3;

    // 解锁条件3的值
    internal int conditionValue3;

    public ScenarioData() {}
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
    
    
    /**
     * 复写这个玩意得到类
     */
    @override
    DataBean<ScenarioData> parseTableData(SqliteDataReader reader ) {
        
        return new ScenarioData();
    }
}