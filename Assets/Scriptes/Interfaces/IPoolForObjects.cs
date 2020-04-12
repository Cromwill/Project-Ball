public interface IPoolForObjects
{
    void GeneratePool(int objectCount, bool isFirstGame, string levelName);
    IObjectPool GetObject();
    IObjectPool GetObject(int index);
    void ReturnObjectToPool(IObjectPool obj);
}
