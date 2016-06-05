using UnityEngine;
using System.Collections;

public class Panther : MonoBehaviour
{
    public float attackDistance, revealDistance, speed, throwSpeed, frameTime, animationTime, fade, visibility, speedCap, offset;
    float frameTimer, rotate = -20;
    public Sprite[] running, sneaking, leaping;
    Sprite[] triggeredAnimation;
    public Sprite burnt, frozen;
    SpriteRenderer spriteRenderer;
    public int curSprite = 0, speedmod = 5;
    public bool goLeft;
    private bool dead = false;
    public bool isEndBoss = false;

    void Start()
    {
        frameTimer = animationTime;
        fade = 0;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        triggeredAnimation = sneaking;

        if (isEndBoss)
            GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        spriteRenderer.color = new Color(255, 255, 255, fade);
        if (!dead)
        {
            AnimatePanther();
            Movement();

            if (Vector2.Distance(transform.position, GameObject.FindWithTag("Blindguy").transform.position) <= attackDistance)
            {
                Attack();
            }
            else if (Vector2.Distance(transform.position, GameObject.FindWithTag("Blindguy").transform.position) <= revealDistance)
            {
                Reveal();
            }
            if (this.transform.position.x <= (GameObject.FindGameObjectWithTag("Blindguy").transform.position.x + offset))
            {
                speed = 0;
                speedCap = 0;
                dead = true;
            }
        }
        else
        {
            if (isEndBoss)
            {
                transform.localScale -= new Vector3(-Time.deltaTime, Time.deltaTime, 0);
                if (transform.localScale.y <= 0)
                    Destroy(gameObject);
            }
            fade = 1;
        }
    }

    private void Attack()
    {
        if (!dead)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotate);
            triggeredAnimation = leaping;
        }
    }


    private void Reveal()
    {
        if (!dead)
        {
            fade += Time.deltaTime/speedmod;
            if (fade >= visibility)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().Play();
                if (isEndBoss)
                    GetComponent<Collider2D>().enabled = true;
                triggeredAnimation = running;
                if (speed <= speedCap)
                {
                    speed += Time.deltaTime*3;
                }
            }
        }
    }

    void Movement()
    {
        if (!dead)
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);                
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (!dead)
        {
            if (target.gameObject.tag == "FireAttack")
            {
                dead = true;
                if (!isEndBoss)
                    spriteRenderer.sprite = burnt;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<AudioSource>().Stop();
            }
            else if (target.gameObject.tag == "IceAttack")
            {
                dead = true;
                if (!isEndBoss)
                    spriteRenderer.sprite = frozen;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<AudioSource>().Stop();
            }
        }
    }

    void AnimatePanther()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            curSprite++;
            if (curSprite >= triggeredAnimation.Length)
            {
                curSprite = 0;
            }
            frameTimer = animationTime;
        }
        this.gameObject.GetComponent<SpriteRenderer>().sprite = triggeredAnimation[curSprite];
    }
}


