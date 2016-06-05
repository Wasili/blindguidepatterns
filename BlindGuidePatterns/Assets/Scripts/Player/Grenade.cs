using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{
    public float timeAlive, lifeTime, stunDelay, cleanDelay, killDelay;
    public SpriteRenderer spriteRenderer;
    public Sprite grenade, broken;
    public ParticleSystem particles;
    public bool exploded;
    public AudioClip bark;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particles = GetComponent<ParticleSystem>();
        spriteRenderer.sprite = grenade;
        GetComponent<ParticleSystem>().enableEmission = false;
        this.GetComponent<Rigidbody2D>().velocity = GlobalGuide.AimTowardsMouse(this.transform.position) * 10;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag != "Player")
        {
            Explode();
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            this.GetComponent<Collider2D>().enabled = false;
        }

    }
    
    void Explode()
    {
        spriteRenderer.sprite = broken;
        this.GetComponent<ParticleSystem>().enableEmission = true;
        AudioSource.PlayClipAtPoint(bark, transform.position);
        Invoke("CleanUp", cleanDelay);
        Invoke("StunBlind", stunDelay);
    }

    void CleanUp()
    {
        this.GetComponent<ParticleSystem>().enableEmission = false;
        Destroy(this.gameObject, killDelay);
    }

    void StunBlind()
    {
        GameObject.FindWithTag("Blindguy").GetComponent<BlindGuyAI>().stopTimer = cleanDelay;
    }

    void Update()
    {
        if (this.gameObject != null)
        {
            timeAlive += Time.deltaTime;
            if (timeAlive >= lifeTime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
