using UnityEngine;
using System.Collections;

public class FlyingEnemy : MonoBehaviour {
    enum State { idle, attacking, dead };
    State curState;

    public float reactionDistance = 20f, speed = 5f, animationSpeed = 0.5f;
    float frameTimer;

    Transform blindGuyTransform, myTransform;

    public Sprite idleSprite;
    public Sprite[] flyAnimationSprites;
    public AudioClip flyAttack;
    int curSprite;
    SpriteRenderer spriteRenderer;

	void Start () 
    {
        curState = State.idle;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    void Awake()
    {
        blindGuyTransform = GameObject.FindWithTag("Blindguy").transform;
        myTransform = transform;
    }

	void Update () 
    {
        switch (curState)
        {
            case State.idle:
                WaitForBlindGuy();
                break;
            case State.attacking:
                Attack();
                break;
            case State.dead:
                ScaleDeath();
                break;
        }
	}

    void ScaleDeath()
    {
        myTransform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, 0);
		GetComponent<Collider2D>().enabled = false;

        if (myTransform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Die()
    {
        myTransform.position = new Vector3(myTransform.position.x, blindGuyTransform.position.y + 2f, myTransform.position.z);
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void WaitForBlindGuy()
    {
        spriteRenderer.sprite = idleSprite;
        if ((myTransform.position.x - blindGuyTransform.position.x) <= reactionDistance)
        {
            curState = State.attacking;
            AudioSource.PlayClipAtPoint(flyAttack, transform.position);
        }
    }

    void Attack()
    {
        Animate();
        myTransform.position += (blindGuyTransform.position - myTransform.position).normalized * speed * Time.deltaTime;
    }

    void Animate()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            curSprite++;
            if (curSprite >= flyAnimationSprites.Length)
                curSprite = 0;
            frameTimer = animationSpeed;
        }
        spriteRenderer.sprite = flyAnimationSprites[curSprite];
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FireAttack")
        {
            curState = State.dead;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 200, 200, 0.3f);
        Gizmos.DrawSphere(transform.position, reactionDistance);
    }
}
