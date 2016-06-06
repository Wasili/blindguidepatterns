using UnityEngine;
using System.Collections;

public class FireFinish : MonoBehaviour {

    public GameObject fireHouse, finish;
    SetFlameDeath myFlameScript;
    bool burning = false;
    public AudioClip blazing;
    SpriteRenderer sRenderer;
    public Sprite normalSprite, fireSprite;
    DataMetricObstacle dataMetric = new DataMetricObstacle();

    void Start () 
    {
        sRenderer = GetComponent<SpriteRenderer>();
        myFlameScript = GetComponent<SetFlameDeath>();
        dataMetric.obstacle = DataMetricObstacle.Obstacle.FireFinish;
    }
	
	void Update () 
    {

        if (burning)
        {
            sRenderer.sprite = fireSprite;
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            if (myFlameScript == null)
                myFlameScript = gameObject.AddComponent<SetFlameDeath>();
            gameObject.tag = "Untagged";
        }
        else
        {
            sRenderer.sprite = normalSprite;

            if (myFlameScript != null)
                Destroy(myFlameScript);

            gameObject.tag = "Finish";
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "IceAttack")
        {
            if (burning)
            {
                dataMetric.howItDied = "Ice";
                dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
                dataMetric.saveLocalData();
                burning = false;
                GetComponent<AudioSource>().Stop();
            }
        }
        if (col.gameObject.tag == "FireAttack")
        {
            burning = true;
        }
    }

    void OnBecameVisible()
    {
        dataMetric.spawnTime = Time.timeSinceLevelLoad.ToString();
    }
}
