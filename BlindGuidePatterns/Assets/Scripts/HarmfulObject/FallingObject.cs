using UnityEngine;
using System.Collections;

public class FallingObject : MonoBehaviour {
    GameObject blindGuy;
    bool falling;
    public float reactionDistance, rotationSpeed, maxAngle;
    public GameObject baseObject;
    public Sprite frozenBase, frozenRock;
    float rotation;
    float timer;
    bool frozen = false;
    DataMetricObstacle dataMetric = new DataMetricObstacle();

    void Start () 
    {
        blindGuy = GameObject.FindWithTag("Blindguy");
        rotation = -rotationSpeed;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        dataMetric.obstacle = DataMetricObstacle.Obstacle.FallingRock;
    }
	
	void Update () 
    {
        if (frozen)
            return;

        if (blindGuy == null)
            blindGuy = GameObject.FindWithTag("Blindguy");

        if (Mathf.Abs(transform.position.x - blindGuy.transform.position.x) < reactionDistance || GetComponent<Rigidbody2D>().isKinematic)
        {
            baseObject.SetActive(false);
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        timer += rotation * Time.deltaTime;

        if (timer > maxAngle)
        {
            rotation = -rotationSpeed;
            timer = maxAngle;
        }

        if (timer < -maxAngle)
        {
            rotation = rotationSpeed;
            timer = -maxAngle;
        }

        if (GetComponent<Rigidbody2D>().gravityScale <= 0)
        {
            transform.Rotate(new Vector3(0, 0, rotation * Time.deltaTime));
        }

        if (blindGuy.transform.position.x > transform.position.x)
        {
            dataMetric.howItDied = "Telekinesis";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "IceAttack" && GetComponent<Rigidbody2D>().gravityScale <= 0)
        {
            dataMetric.howItDied = "Ice";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            frozen = true;
            baseObject.GetComponent<SpriteRenderer>().sprite = frozenBase;
            GetComponent<SpriteRenderer>().sprite = frozenRock;
            gameObject.tag = "Untagged";
        }
    }

    void OnBecameVisible()
    {
        dataMetric.spawnTime = Time.timeSinceLevelLoad.ToString();
    }
}
