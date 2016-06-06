using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour
{
    public float speed, maxHeight = 2, downwardSpeed = 6f, upwardSpeed = 5f, minYVelocity = -2f;
    bool going;
    Transform blindTransform;
    public AudioClip snowballHit;
    DataMetricObstacle dataMetric = new DataMetricObstacle();

    void Start()
    {
        blindTransform = GameObject.FindWithTag("Blindguy").transform;
        GetComponent<Rigidbody2D>().velocity = (blindTransform.position - transform.position).normalized * speed;
        GetComponent<Rigidbody2D>().velocity += new Vector2(0, upwardSpeed);
        dataMetric.obstacle = DataMetricObstacle.Obstacle.SnowBall;
        dataMetric.spawnTime = Time.timeSinceLevelLoad.ToString();
    }

    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.y > minYVelocity)
            GetComponent<Rigidbody2D>().velocity -= new Vector2(0, downwardSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FireAttack")
        {
            dataMetric.howItDied = "Fire";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Blindguy")
        {
            AudioSource.PlayClipAtPoint(snowballHit, transform.position);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Ground")
        {
            dataMetric.howItDied = "Telekinesis";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            Destroy(gameObject);
        }
    }

    public void playSound(AudioClip sound)
    {
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }
}
