using UnityEngine;
using System.Collections;

public abstract class DataMetric
{
    public struct MetricValue
    {
        string name;
        float value;
    };

    public string name { get; protected set; }
    public string details { get; protected set; }
    public MetricValue[] values { get; protected set; }

    public string queryForSave { get; protected set; }
    public string queryforLoad { get; protected set; }

    public abstract void saveLocalData();
    public abstract string[] loadLocalData();
}
