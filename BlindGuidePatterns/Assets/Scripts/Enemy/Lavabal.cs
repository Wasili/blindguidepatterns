using UnityEngine;
using System.Collections;

public class Lavabal : MonoBehaviour {
    public float speed, maxHeight = 2, downwardSpeed = 6f, upwardSpeed = 5f, minYVelocity = -2f;
    bool going;
    Transform blindTransform;
    DataMetricObstacle dataMetric = new DataMetricObstacle();

   void Start()
    {
		GetComponent<Rigidbody2D>().velocity = (blindTransform.position - transform.position).normalized * speed;
		GetComponent<Rigidbody2D>().velocity += new Vector2(0, upwardSpeed);
        dataMetric.obstacle = DataMetricObstacle.Obstacle.LavaBall;
        dataMetric.spawnTime = Time.timeSinceLevelLoad.ToString();
    }

    void Awake()
    {
        blindTransform = GameObject.FindWithTag("Blindguy").transform;
    }

    void Update()
    {
        clone(); //here coded
    }

    void OnTriggerEnter2D(Collider2D ice)
    {
        if (ice.gameObject.tag == "IceAttack")
        {
            dataMetric.howItDied = "Ice";
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

    void OnBecameInvisible()
    {
        dataMetric.howItDied = "Telekinesis";
        dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
        dataMetric.saveLocalData();
        Destroy(gameObject);
    }

    void clone() //here coded
    {
        Lavabal lav = new Lavabal();
        if (GetComponent<Rigidbody2D>().velocity.y > minYVelocity)
            GetComponent<Rigidbody2D>().velocity -= new Vector2(0, downwardSpeed * Time.deltaTime);
        lav = this;
        Destroy(gameObject);
    }
}