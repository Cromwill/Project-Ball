public interface IRunable<in T>
{
    void Run(T value);
}

public interface IRunable
{
    void Run();
}

public interface IRunable<in T, in V>
{
    void Run(T valueT, V valueV);
}

