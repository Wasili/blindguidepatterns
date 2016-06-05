using UnityEngine;
using System.Collections;

public class Rollingstones : MonoBehaviour
{
    public float speed, reactionDistance = 20f;
    public bool thrown = false, activateOnDistance = false;
    bool pickedUp = false, soundPlayed = false, shrinked = false, started = false;

    public float maxThrows = 5;

    Transform myTransform, blindGuyTransform;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        myTransform = transform;
        blindGuyTransform = GameObject.FindWithTag("Blindguy").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (activateOnDistance)
        {
            if (!started)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0;
                if (Mathf.Abs(myTransform.position.x - blindGuyTransform.position.x) < reactionDistance)
                {
                    started = true;
                    GetComponent<Rigidbody2D>().gravityScale = 1;
                }
                return;
            }
        }
        transform.Rotate(Vector3.forward * Time.deltaTime * -speed * 50);
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        if (GetComponent<Rigidbody2D>().isKinematic)
        {
            pickedUp = true;
            thrown = false;
            shrinked = false;
            speed = 0;
        }
        if (pickedUp && !GetComponent<Rigidbody2D>().isKinematic)
        {
            thrown = true;
            pickedUp = false;
        }

        if (myTransform.localScale.x <= maxThrows * 0.1f)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - Time.deltaTime);
            gameObject.tag = "Untagged";
            if (myTransform.localScale.x <= 0 || spriteRenderer.color.a <= 0)
                Destroy(gameObject);
        }

        if (spriteRenderer.color.a <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (thrown && !shrinked)
        {
            myTransform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            shrinked = true;
        }
    }

    void OnBecameInvisible()
    {
        GetComponent<AudioSource>().Stop();
    }

    void OnBecameVisible()
    {
        if (!soundPlayed) 
        {
            GetComponent<AudioSource>().Play();
            soundPlayed = true;
        }

        if (activateOnDistance)
        {
            started = true;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
