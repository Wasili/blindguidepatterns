using UnityEngine;
using System.Collections;

public class DataMetricGame : DataMetric
{
    public enum Level { Tutorial, Fire1, Fire2, Fire3, Ice1, Ice2, Ice3, Jungle1, Jungle2, Jungle3 }

    public int session;
    public string starttime;
    public string endTime;
    public Level level;
    public int playerDied;
    public string howPlayerDied;

    public override void saveLocalData()
    {
        queryForSave = "INSERT INTO game(Session, StartTime, EndTime, Level, PlayerDied, HowPlayerDied) VALUES("
            + "'" + session + "'" + ","
            + "'" + starttime + "'" + ","
            + "'" + endTime + "'" + ","
            + "'" + level.ToString() + "'" + ","
            + "'" + playerDied + "'" + ","
            + "'" + howPlayerDied + "'"
            + ")";
        DataCollector.getInstance().saveMetric(this);
    }

    public override string[] loadLocalData()
    {
        queryforLoad = "";
        return null;
    }
}
