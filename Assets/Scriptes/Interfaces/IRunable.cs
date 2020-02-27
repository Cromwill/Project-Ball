interface IRunable
{
    void Run();
    void Run<T>(T value);
    void Run<T, V>(T valueT, V valueV);
}

