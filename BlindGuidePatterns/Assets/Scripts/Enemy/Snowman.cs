using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour
{
    public GameObject snowball;
    public GameObject rollingStoneIce;
    public float attackCooldown, attackDistance, retreatDistance, speed, throwSpeed, frameTime;
    float timer, animationTimer, attackTimer;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    int currentSprite = 0;
    public bool goLeft, attack2;
    private bool dead = false;
    public AudioClip throwball;

    void Start()
    {
        attackTimer = attackCooldown;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!dead)
        {
            Movement();
            attackTimer -= Time.deltaTime;
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                spriteRenderer.sprite = sprites[0];
            }

            if (attackTimer <= 0 && attack2 == false)
            {
                if (Vector2.Distance(transform.position, GameObject.FindWithTag("Blindguy").transform.position) <= attackDistance)
                {
                    Attack1();
                }
            }

            if (attackTimer <= 0 && attack2 == true)
            {
                if (Vector2.Distance(transform.position, GameObject.FindWithTag("Blindguy").transform.position) <= attackDistance)
                {
                    Attack2();
                }
            }
        }
        else AnimateDeath();
    }

    void Attack1()
    {
        if (!dead)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject myBall = (GameObject)Instantiate(snowball, new Vector3(transform.position.x, transform.position.y + 0.2f, 0),
                Quaternion.Euler(0, 0, 0));
            myBall.GetComponent<Snowball>().speed = throwSpeed;
            timer = frameTime;
            spriteRenderer.sprite = sprites[1];
            attackTimer = attackCooldown;
            playSound(throwball);
        }
    }

    void Attack2()
    {
        if (!dead)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject myGiantBall = (GameObject)Instantiate(rollingStoneIce, new Vector3(transform.position.x, transform.position.y + 0.2f, 0),
                Quaternion.Euler(0, 0, 0));
            myGiantBall.GetComponent<Rollingstones>().speed = throwSpeed;
            timer = frameTime;
            spriteRenderer.sprite = sprites[1];
            attackTimer = attackCooldown;
        }
    }

    void Movement()
    {
        if (!dead)
        {
            if (goLeft == true)
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Vector2.Distance(transform.position, GameObject.FindWithTag("Blindguy").transform.position) <= retreatDistance)
            {
                goLeft = false;
            }

            if (goLeft == false)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D fire)
    {
        if (!dead)
        {
            if (fire.gameObject.tag == "FireAttack")
            {
                dead = true;
            }
        }
    }
    void AnimateDeath()
    {
        animationTimer -= Time.deltaTime * 2;
        if (animationTimer <= 0)
        {
            if (currentSprite + 1 < sprites.Length)
                currentSprite++;
            else
                currentSprite = 3;
            animationTimer = frameTime;
        }
        spriteRenderer.sprite = sprites[currentSprite];
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    public void playSound(AudioClip sound)
    {
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }
}


