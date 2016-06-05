using UnityEngine;
using System.Collections;

public class DataCollector : MonoBehaviour {
    //create a list of existing metric points, with the final value being used to determine the amount of metric points
    public enum Metric { FireUsage, IceUsage, TelekinesisUsage, DestructionUsage, BarkUsage, DeathPlayerCount, DeathPlayerReason, METRIC_COUNT };
    //an array containing the values of each metric point
    static int[] metricValues = new int[(int)Metric.METRIC_COUNT];

    public static void AdjustDataMetric(Metric metricPoint, int metricAdditiveValue) {
        //increment the specified metric value by the specified amount
        metricValues[(int)metricPoint] += metricAdditiveValue;
    }

    public static int GetMetricValue(Metric metricPoint) {
        return metricValues[(int)metricPoint];
    }

    public static void SaveAllMetricsLocally()
    {
        //iterate through all metric points, except the last one
        for (int i = 0; i < metricValues.Length; i++)
        {
            //read the metric point at the current iteration
            Metric metric = (Metric)i;
            //save the metric value of the current iteration locally
            PlayerPrefs.SetInt(metric.ToString(), metricValues[i]);
        }
    }

    public static void LoadLocalMetricData() {
        for (int i = 0; i < metricValues.Length; i++)
        {
            //read the metric point at the current iteration
            Metric metric = (Metric)i;
            //load the metric value of the current iteration from local save data
            metricValues[i] = PlayerPrefs.GetInt(metric.ToString());
        }
    }

    public static void ClearLocalMetricData() {
        for (int i = 0; i < metricValues.Length; i++)
        {
            //read the metric point at the current iteration
            Metric metric = (Metric)i;
            //save the metric value of the current iteration locally
            PlayerPrefs.SetInt(metric.ToString(), 0);
        }
    }

    public static void LoadOnineMetricData() {
        //get all metric data using an http request and save it in local variables
    }

    public static void SaveAllMetricsOnline() {
        //send an http request containing all metric data to the server
    }
}
