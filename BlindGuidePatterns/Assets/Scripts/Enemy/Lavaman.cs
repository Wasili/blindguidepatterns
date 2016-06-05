using UnityEngine;
using System.Collections;

public class Lavaman : MonoBehaviour {
    public GameObject lavaball;
    public float attackCooldown, attackDistance, retreatDistance, speed, throwSpeed, frameTime, reactionDistance = 25f;
    float timer, animationTimer, attackTimer;
    public Sprite[] sprites;
    public Sprite frozen;
    SpriteRenderer spriteRenderer;
    public bool goLeft, reactOnDistance = false;
    private bool dead = false;
    Transform blindGuyTransform;
    bool blindGuyInRange = false;
    public AudioClip throwball;

    void Start()
    {
        attackTimer = attackCooldown;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        blindGuyTransform = GameObject.FindWithTag("Blindguy").transform;
    }

    void Update()
    {
        if (reactOnDistance)
        {
            if (!blindGuyInRange) 
            {
                if (Vector3.Distance(transform.position, blindGuyTransform.position) < reactionDistance)
                {
                    blindGuyInRange = true;
                }
                return;
            }
        }
        if (!dead)
        {
            Movement();
            attackTimer -= Time.deltaTime;
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                spriteRenderer.sprite = sprites[0];
            }

            if (attackTimer <= 0)
            {
                if (Vector2.Distance(transform.position, blindGuyTransform.position) <= attackDistance)
                {
                    Attack();
                }
            }
        }
        else AnimateDeath();
    }

    void AnimateDeath()
    {
        transform.localScale += new Vector3(-Time.deltaTime, -Time.deltaTime, 0);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - Time.deltaTime);
        if (transform.localScale.x <= 0 || spriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        if (!dead)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject myBall = (GameObject) Instantiate(lavaball, new Vector3(transform.position.x, transform.position.y + 0.2f, 0), 
                Quaternion.Euler(0, 0, 0));
            if(myBall.GetComponent<Lavabal>())
                myBall.GetComponent<Lavabal>().speed = throwSpeed;
            timer = frameTime;
            spriteRenderer.sprite = sprites[1];
            attackTimer = attackCooldown;
            playSound(throwball);
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

            if (Vector2.Distance(transform.position, blindGuyTransform.position) <= retreatDistance)
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

    void OnTriggerEnter2D(Collider2D ice)
    {
        if (!dead)
        {
            if (ice.gameObject.tag == "IceAttack")
            {
                dead = true;
            }
        }
    }
    public void playSound(AudioClip sound)
    {
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }
 }




