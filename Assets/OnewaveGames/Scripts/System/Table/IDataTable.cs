namespace OnewaveGames.Scripts.System.Table
{
    public interface IDataTableBase {}
    
    public interface IDataTable : IDataTableBase
    {
        void RegisterData();
        void Clear();
    }

    public interface IDataBase { }

    public class Data : IDataBase
    {
        int Key { get; set; }
    }
}