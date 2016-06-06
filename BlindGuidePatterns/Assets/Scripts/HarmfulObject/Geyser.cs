using UnityEngine;
using System.Collections;

public class Geyser : MonoBehaviour {

    public bool frozenState = false, spraying = false;
    public GameObject waterJet;
    public Transform spawnPos;
    public SpriteRenderer spriteRenderer;
    public Sprite activeSprite, frozenSprite, brokenSprite;
    public BoxCollider2D colBox;
    public float offsetY = 2.1f;
    GameObject spawnedJet;
    Vector3 myScale;
    DataMetricObstacle dataMetric = new DataMetricObstacle();

    void Start () {
        colBox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myScale = transform.localScale;
        StopSpray();
        dataMetric.obstacle = DataMetricObstacle.Obstacle.Geyser;
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "IceAttack")
        {
            dataMetric.howItDied = "Ice";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            frozenState = true;
            spriteRenderer.sprite = frozenSprite;
            colBox.enabled = false;
            if (spawnedJet != null)
                spawnedJet.GetComponent<Jet>().frozenState = true;
        }
        if (target.gameObject.tag == "PullableObject") {
            if (target.gameObject.GetComponent<Rollingstones>() != null)
            {
                if (target.gameObject.GetComponent<Rollingstones>().thrown)
                {
                    dataMetric.howItDied = "Destruction";
                    dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
                    dataMetric.saveLocalData();
                    frozenState = true;
                    spriteRenderer.sprite = brokenSprite;
                    colBox.enabled = false;
                    if (spawnedJet != null)
                        Destroy(spawnedJet);
                }
            }
            else if (target.gameObject.name == "FallObject")
            {
                dataMetric.howItDied = "Destruction";
                dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
                dataMetric.saveLocalData();
                frozenState = true;
                spriteRenderer.sprite = brokenSprite;
                colBox.enabled = false;
                if (spawnedJet != null)
                    Destroy(spawnedJet);
            }
        }
    }

    public void SprayWater()
    {
        if (!frozenState && !spraying)
        {
            Destroy(spawnedJet);
            spawnedJet = (GameObject)Instantiate(waterJet, new Vector3(transform.position.x, transform.position.y + offsetY, 0), transform.rotation);
            spraying = true;
            spawnedJet.GetComponent<Jet>().myGeyser = this;
            Invoke("StopSpray", spawnedJet.GetComponent<Jet>().lifeTime);
        }
    }

    public void StopSpray()
    {
        spraying = false;
        Invoke("SprayWater", 2f);
    }

    void Update()
    {
        if (frozenState)
        {
            spraying = false;
            return;
        }
        if (spraying)
            myScale.y += Time.deltaTime;
        else
            myScale.y -= Time.deltaTime;

        myScale.y = Mathf.Clamp(myScale.y, 0.7f, 1f);

        transform.localScale = myScale;
    }

    void OnBecameVisible()
    {
        dataMetric.spawnTime = Time.timeSinceLevelLoad.ToString();
    }
}
