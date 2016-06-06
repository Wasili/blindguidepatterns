using UnityEngine;
using System.Collections;

public class Lavaval : MonoBehaviour
{
    public bool frozenState = false;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public Color frozenColor;
    BoxCollider2D colBox;
    public float offsetY = 2.1f, frameTime;
    float timer;
    int currentSprite = 1;
    bool reversing;
    DataMetricObstacle dataMetric = new DataMetricObstacle();

    void Start()
    {
        colBox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dataMetric.obstacle = DataMetricObstacle.Obstacle.Lavafall;
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "IceAttack")
        {
            frozenState = true;
            spriteRenderer.color = frozenColor;
            colBox.enabled = false;
            dataMetric.howItDied = "Ice";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
        }
    }

    void Animate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (currentSprite + 1 < sprites.Length && !reversing)
            {
                currentSprite++;
            }
            else if (currentSprite > 0)
            {
                currentSprite--;
                reversing = true;
                if (currentSprite == 1)
                    reversing = false;
            }

            timer = frameTime;
        }
    }

    void Update()
    {
        if (!frozenState)
        {
            Animate();
            spriteRenderer.sprite = sprites[currentSprite];
        }
    }

    void OnBecameVisible()
    {
        dataMetric.spawnTime = Time.timeSinceLevelLoad.ToString();
    }
}
