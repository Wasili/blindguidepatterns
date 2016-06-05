using UnityEngine;
using System.Collections;

public class Jet : MonoBehaviour
{
    public float timeAlive;
    public float lifeTime = 2f;
    public bool frozenState = false;
    SpriteRenderer spriteRenderer;
    public Sprite activeSprite, frozenSprite;
    public BoxCollider2D colBox;
    float growTime;
    public Geyser myGeyser;

    void Start()
    {
        colBox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeAlive = 0;
        spriteRenderer.sprite = activeSprite;

        transform.localScale = Vector3.zero;
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "IceAttack")
        {
            frozenState = true;
            spriteRenderer.sprite = frozenSprite;
            colBox.enabled = false;
            Invoke("FreezeGeyser", 0.5f);
        }
    }

    void FreezeGeyser()
    {
        myGeyser.frozenState = true;
        myGeyser.spriteRenderer.sprite = myGeyser.frozenSprite;

    }

    void Update()
    {
        growTime += Time.deltaTime;
        if (growTime < 1 && !frozenState)
        {
            transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
        }
        if (this.gameObject != null && !frozenState)
        {
            timeAlive += Time.deltaTime;

            if (timeAlive >= lifeTime - 0.5f)
            {
                transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
                if (timeAlive >= lifeTime)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        if (frozenState)
            GetComponent<AudioSource>().Stop();
    }

    void OnBecameVisible()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }
}
