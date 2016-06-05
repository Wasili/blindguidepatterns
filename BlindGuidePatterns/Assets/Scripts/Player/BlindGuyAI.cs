using UnityEngine;
using System.Collections;

public class BlindGuyAI : MonoBehaviour {
    public float stopTimer = 0;
    public float speed = 2;

    float regularSpeed;

    Vector3 positionOffset, blindGuySize;

    public Sprite[] burned;
    public Sprite[] dizzy;
    public Sprite[] frozen;
    public Sprite[] stunned;
    public Sprite[] walking;
    int curSprite;
    public float animationTime = 1;
    float frameTimer;
    public AudioClip freezeDeath, flameDeath, dazedDeath;

    Sprite[] triggeredAnimation;

    bool dying = false;

	void Start ()
    {
        frameTimer = animationTime;
        regularSpeed = speed;
    }
	
	void Update () 
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        if (dying)
        {
            AnimateBlindGuy();
        }

        else if (stopTimer > 0)
        {
            stopTimer -= Time.deltaTime;
            speed = 0;
            triggeredAnimation = stunned;
            AnimateBlindGuy();
        }
        else
        {
            speed = regularSpeed;
            triggeredAnimation = walking;
            AnimateBlindGuy();
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Finish")
        {
            if (Application.loadedLevelName == "Level10")
            {
                Application.LoadLevel("PostGameScene");
                return;
            }
            MenuBehaviour menuScript = GameObject.FindWithTag("UIBehaviour").GetComponent<MenuBehaviour>();
            menuScript.SaveProgress();
            Application.LoadLevel("WinState");
        }

        if (col.tag == "FallingTrigger")
        {
            col.GetComponentInParent<Rigidbody2D>().isKinematic = false;
        }

    }

    void LoadLevel()
    {
        Application.LoadLevel("LossState");
    }

   
    public void SetDizzyDeath() 
    {
        Invoke("LoadLevel", 3f);
        regularSpeed = 0;
        speed = 0;
        triggeredAnimation = dizzy;
        dying = true;
        AudioSource.PlayClipAtPoint(dazedDeath, transform.position);
    }

    public void SetFlameDeath()
    {
        Invoke("LoadLevel", 3f);
        regularSpeed = 0;
        speed = 0;
        triggeredAnimation = burned;
        dying = true;
        AudioSource.PlayClipAtPoint(flameDeath, transform.position);
    }

    public void SetFrozenDeath()
    {
        Invoke("LoadLevel", 3f);
        regularSpeed = 0;
        speed = 0;
        triggeredAnimation = frozen;
        dying = true;
        AudioSource.PlayClipAtPoint(freezeDeath, transform.position);
    }

    void AnimateBlindGuy()
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
