using System.Collections;
using System.Collections.Generic;

/**
 * General Data Container to hold 3 T values
 */
[System.Serializable]
public class Triple<T1, T2, T3>
{
    public T1 first;
    public T2 second;
    public T3 third;

    public Triple(T1 _first, T2 _second, T3 _third)
    {
        first = _first;
        second = _second;
        third = _third;
    }
}

/**
 * General Data Container to hold 2 T values
 */
[System.Serializable]
public class Pair<T1, T2>
{
    public T1 first;
    public T2 second;

    public Pair(T1 _first, T2 _second)
    {
        first = _first;
        second = _second;
    }
}