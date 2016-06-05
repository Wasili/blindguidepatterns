using UnityEngine;
using System.Collections;

public class IcePool : MonoBehaviour {
    bool frozen = true;
    SpriteRenderer spriteRenderer;
    Color myColor;

	void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myColor = spriteRenderer.color;
	}
	
	void Update () 
    {
        if (!frozen)
            myColor.a -= (Time.deltaTime);

        myColor.a = Mathf.Clamp(myColor.a, 0, 1);
        spriteRenderer.color = myColor;

        if (myColor.a <= 0)
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FireAttack" && frozen)
        {
            gameObject.tag = "Untagged";
            frozen = false;
        }
    }
}
