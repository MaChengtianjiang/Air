using Mono.Data.Sqlite;

public class DataBean<T> where T : new() {
    // id
    internal int id;


    /**
     * 复写这个玩意得到类
     */
    T parseTableData(SqliteDataReader reader ) {
        
        return new T();
    }
}