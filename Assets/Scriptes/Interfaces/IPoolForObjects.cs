public interface IPoolForObjects
{
    void GeneratePool(int objectCount);
    IObjectPool GetObject();
    IObjectPool GetObject(int index);
    void ReturnObjectToPool(IObjectPool obj);
}
