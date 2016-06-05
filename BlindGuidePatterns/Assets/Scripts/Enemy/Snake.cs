using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    //public PolygonCollider2D start, threat, attack;
    public Sprite initial, threatening, attacking, frozen, burnt;
    public float cooldown, maxCooldown, warningRange, attackRange;
    private bool dead = false, firstWarning = true;
    public AudioClip hiss;
    Transform blindGuyTransform;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = initial;
    }

    void Awake()
    {
        blindGuyTransform = GameObject.FindWithTag("Blindguy").transform;
    }

    void Warning()
    {
        if (!dead)
        {
            spriteRenderer.sprite = threatening;
            if (firstWarning == true)
            {
                AudioSource.PlayClipAtPoint(hiss, transform.position);
                firstWarning = false;
            }
        }
    }

    void Attacking()
    {
        spriteRenderer.sprite = attacking;
        cooldown = maxCooldown;
        Invoke("Warning", maxCooldown);
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (!dead)
        {
            
            if (target.gameObject.tag == "IceAttack")
            {
                dead = true;
                spriteRenderer.sprite = frozen;
                GetComponent<Collider2D>().enabled = false;
            }
            else if (target.gameObject.tag == "FireAttack")
            {
                dead = true;
                spriteRenderer.sprite = burnt;
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void Update()
    {
        if (blindGuyTransform == null)
            blindGuyTransform = GameObject.FindWithTag("Blindguy").transform;

        if (!dead)
        {
            if (cooldown >=0)
            cooldown -= Time.deltaTime;

            if (Vector2.Distance(transform.position, blindGuyTransform.position) <= warningRange && Vector2.Distance(transform.position, GameObject.FindWithTag("Blindguy").transform.position) > attackRange)
            {
                Warning();
            }

            if (cooldown <= 0)
            {
                if (Vector2.Distance(transform.position, blindGuyTransform.position) <= attackRange)
                {
                    Attacking();
                }
            }
        }
    }
}
