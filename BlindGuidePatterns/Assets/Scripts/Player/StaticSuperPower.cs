using UnityEngine;
using System.Collections;

public class StaticSuperPower : MonoBehaviour {
    Vector3 velocity;
    public Sprite[] sprites;
    public float frameTime;
    int currentSprite = 0;

    public int damage = 1;
    public int speed = 1;
    float timer;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        MovementSpeed();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        timer = frameTime;
    }

    void Update()
    {
        Animate();
        spriteRenderer.sprite = sprites[currentSprite];
    }

    void MovementSpeed()
    {
        GetComponent<Rigidbody2D>().velocity = GlobalGuide.AimTowardsMouse(transform.position) * speed;
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

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
