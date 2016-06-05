using UnityEngine;
using System.Collections;

public class SuperPowerFireIce : MonoBehaviour {
    Vector3 velocity;
    public Sprite[] sprites;
    public float frameTime;
    int currentSprite = 0;
   
    public int damage = 1;
    public int speed = 1;
    float timer;
    SpriteRenderer spriteRenderer;
    Vector3 targetVector = Vector3.zero;
    Transform spawnSource;
    public Transform playerTransform;
    public float timeToLive = 1;
    float lifeTime;

	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        timer = frameTime;
        lifeTime = timeToLive;
	}

    public void SetSpawnSource(Transform source)
    {
        spawnSource = source;
    }
	
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);

        Animate();
        if (spawnSource != null)
            SetPosition();
        spriteRenderer.sprite = sprites[currentSprite];
    }

    void SetPosition()
    {
        transform.rotation = playerTransform.rotation;
        transform.position = spawnSource.position;
    }

    void SetVelocity()
    {
        GetComponent<Rigidbody2D>().velocity = targetVector * speed;
    }

    void Animate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (currentSprite + 1 < sprites.Length)
                currentSprite++;
            else
                currentSprite = 0;
                timer = frameTime;
        }
    }
}
