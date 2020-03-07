static class GameDataStorage
{
    private static int _totalScorre;

    public static void AddTotalScorre(int scorre)
    {
        _totalScorre += scorre;
    }
}

public class TimeScorreData
{
    public int Scorre;
    public float Time;

    public TimeScorreData(int scorre, float time)
    {
        Scorre = scorre;
        Time = time;
    }
}

public class ScorrePerTime
{
    private TimeScorreData[] _datas;
    private int _dataCounter = 0;

    public ScorrePerTime(int count, float startTime)
    {
        _datas = new TimeScorreData[count];
        _datas[_dataCounter] = new TimeScorreData(0, startTime);
    }

    public float GetValue(int point, float time)
    {
        int scorreSum = 0;
        float averageTime = 0;
        int firstIndex = 0;

        _dataCounter = GetNextIndex();
        firstIndex = _datas[_dataCounter] == null ? 0 : GetNextIndex();
        _datas[_dataCounter] = new TimeScorreData(point, time);


        for (int i = 0; i < _datas.Length; i++)
        {
            if (_datas[i] != null)
                scorreSum += _datas[i].Scorre;
        }

        averageTime = _datas[_dataCounter].Time - _datas[firstIndex].Time;
        return scorreSum / averageTime;
    }

    private int GetNextIndex()
    {
        int index = _dataCounter + 1;
        return index == _datas.Length ? 0 : index;
    }
}