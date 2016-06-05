using UnityEngine;
using System.Collections;

public class FireFinish : MonoBehaviour {

    public GameObject fireHouse, finish;
    SetFlameDeath myFlameScript;
    bool burning = false;
    public AudioClip blazing;
    SpriteRenderer sRenderer;
    public Sprite normalSprite, fireSprite;

	void Start () 
    {
        sRenderer = GetComponent<SpriteRenderer>();
        myFlameScript = GetComponent<SetFlameDeath>();
	}
	
	void Update () 
    {

        if (burning)
        {
            sRenderer.sprite = fireSprite;
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            if (myFlameScript == null)
                myFlameScript = gameObject.AddComponent<SetFlameDeath>();
            gameObject.tag = "Untagged";
        }
        else
        {
            sRenderer.sprite = normalSprite;

            if (myFlameScript != null)
                Destroy(myFlameScript);

            gameObject.tag = "Finish";
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "IceAttack")
        {
            if (burning)
            {
                burning = false;
                GetComponent<AudioSource>().Stop();
            }
        }
        if (col.gameObject.tag == "FireAttack")
        {
            burning = true;
        }
    }
}
