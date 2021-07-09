using Mono.Data.Sqlite;

public class DataBean{
    
    // id
    internal int id;

    public DataBean()
    {
        
    }

    /**
     * 复写这个玩意得到类
     */
    public virtual DataBean parseTableData(SqliteDataReader reader) {

        return new DataBean();
    }
}