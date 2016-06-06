using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;


public class DataCollector {
    private static DataCollector _instance;

    private string _constr = "URI=file:" + Application.dataPath + "/AutomatedGameDesign/Scripts/agd_db.db";
    private IDbConnection dbConnection;
    
    private DataCollector()
    {
        dbConnection = (IDbConnection)new SqliteConnection(_constr);
        dbConnection.Open();
    }

    public static DataCollector getInstance()
    {
        if (_instance == null) _instance = new DataCollector();
        return _instance;
    }

    public void saveMetric(DataMetric metric)
    {
        Debug.Log(metric.queryForSave);
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = metric.queryForSave;
        IDataReader dataReader = dbCommand.ExecuteReader();

        dataReader.Close();
        dbCommand.Dispose();
    }

    public DataMetric getMetricData(string name)
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM Attack WHERE name=" + name;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            string attackTime = dataReader.GetString(0);
            string type = dataReader.GetString(1);
        }

        dataReader.Close();
        dbCommand.Dispose();

        return null;
    }

    /*public static int GetLocalMetricValue(string _name) {
        string metric = PlayerPrefs.GetString(_name);
        return metricValues[(int)metricPoint];
    }*/

    /*public static void SaveAllMetricsLocally()
    {
        //iterate through all metric points, except the last one
        for (int i = 0; i < metricValues.Length; i++)
        {
            //read the metric point at the current iteration
            Metric metric = (Metric)i;
            //save the metric value of the current iteration locally
            PlayerPrefs.SetInt(metric.ToString(), metricValues[i]);
        }
    }*/

    /*public static void LoadLocalMetricData() {
        for (int i = 0; i < metricValues.Length; i++)
        {
            //read the metric point at the current iteration
            Metric metric = (Metric)i;
            //load the metric value of the current iteration from local save data
            metricValues[i] = PlayerPrefs.GetInt(metric.ToString());
        }
    }*/

    public static void ClearLocalMetricData() {
        //remove all entires from database
    }

    public static void LoadOnineMetricData() {
        //get all metric data using an http request and save it in local variables
    }

    public static void SaveAllMetricsOnline() {
        //send an http request containing all metric data to the server
    }
}
