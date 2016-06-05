using UnityEngine;
using System.Collections;

public class MonkeyBusiness : MonoBehaviour {

    public Sprite[] sprites;

    public float frameTime, deathFrameTime;
    SpriteRenderer spriteRenderer;
    float timer, deathAnimTimer;
    public GameObject coconut;
    float throwTimer;
    public float throwCooldown, reactionDistance;
    public AudioClip chimpanzee;
    public Sprite[] deathAnimation;
    int curDeathFrame = 0;
    Transform blindGuyTransform;

    bool dead = false;

    public float speed = 5f, throwHeight = 5f;
    public Vector3 throwOffset;

	void Start () {        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        throwTimer = throwCooldown;
        deathAnimTimer = deathFrameTime;
	}

    void Awake()
    {
        blindGuyTransform = GameObject.FindWithTag("Blindguy").transform;
    }
	
	void Throw()
    {
        GameObject myCoconut = (GameObject)Instantiate(coconut, transform.position, Quaternion.Euler(0, 0, 0));
        myCoconut.GetComponent<Coconut>().SetVelocity(blindGuyTransform.position, throwOffset, speed, throwHeight);
        timer = frameTime;
        spriteRenderer.sprite = sprites[1];
        throwTimer = throwCooldown;
    }

    void Update()
    {
        if (dead)
        {
            deathAnimTimer -= Time.deltaTime;
            if (deathAnimTimer <= 0 && curDeathFrame + 1 < deathAnimation.Length)
            {
                curDeathFrame += 1;
                deathAnimTimer = deathFrameTime;
            }
            spriteRenderer.sprite = deathAnimation[curDeathFrame];
            return;
        }

        throwTimer -= Time.deltaTime;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            spriteRenderer.sprite = sprites[0];
        }

        if (throwTimer <= 0)
        {
            if (Vector2.Distance(transform.position, blindGuyTransform.position) <= reactionDistance)
            {
                Throw();
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FireAttack" && !dead)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 200, 200, 0.3f);
        Gizmos.DrawSphere(transform.position, reactionDistance);
    }
}
