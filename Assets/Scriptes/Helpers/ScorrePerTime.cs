public class ScorrePerTime
{
    private TimeScorreData[] _datas;
    private int _dataCounter = 0;
    private float _currentValue;

    public ScorrePerTime(int count, float startTime, float scorrePerSecond)
    {
        _datas = new TimeScorreData[count];
        _datas[_dataCounter] = new TimeScorreData(scorrePerSecond, startTime);
        _currentValue = scorrePerSecond;
    }

    public float GetValue(float point, float time)
    {
        int firstIndex;

        _dataCounter = GetNextIndex();
        firstIndex = _datas[_dataCounter] == null ? 0 : GetNextIndex();
        _datas[_dataCounter] = new TimeScorreData(point, time);

        if (_dataCounter == 0)
        {
            _currentValue = GetAverageValue(firstIndex);
        }

        return _currentValue;
    }

    private int GetNextIndex()
    {
        int index = _dataCounter + 1;
        return index == _datas.Length ? 0 : index;
    }

    private float GetAverageValue(int firstIndex)
    {
        float scorreSum = 0;
        float averageTime;

        for (int i = 0; i < _datas.Length; i++)
        {
            if (_datas[i] != null)
                scorreSum += _datas[i].Scorre;
        }

        averageTime = _datas[_dataCounter].Time - _datas[firstIndex].Time;
        var value = scorreSum / averageTime;
        if (value > _currentValue)
            return value;
        else
            return _currentValue;
    }
}

public class TimeScorreData
{
    public float Scorre;
    public float Time;

    public TimeScorreData(float scorre, float time)
    {
        Scorre = scorre;
        Time = time;
    }
}