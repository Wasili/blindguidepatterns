using UnityEngine;
using System.Collections;

public class Coconut : MonoBehaviour {
    public float timeToLive = 10f;
    DataMetricObstacle dataMetric = new DataMetricObstacle();

    public void SetVelocity(Vector3 target, Vector3 throwOffset, float speed, float throwHeight) 
    {
        Vector3 targetPos = GameObject.FindWithTag("Blindguy").transform.position + throwOffset;
		GetComponent<Rigidbody2D>().velocity = new Vector2(((targetPos - transform.position).normalized.x * speed), throwHeight);
        dataMetric.obstacle = DataMetricObstacle.Obstacle.Coconut;
        dataMetric.spawnTime = Time.timeSinceLevelLoad.ToString();
    }

    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            dataMetric.howItDied = "Timer";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "FireAttack")
        {
            dataMetric.howItDied = "Fire";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            dataMetric.howItDied = "Telekinesis";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            Destroy(gameObject);
        }
    }
}
