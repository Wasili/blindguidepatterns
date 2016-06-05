using UnityEngine;
using System.Collections;

public class BlindDamage : MonoBehaviour {

    public static int health = 100;

	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Lethal") // blind guy wordt gedeletet, game over
        {
            Destroy(gameObject);
            Application.LoadLevel(1);
        }

        else if (target.gameObject.tag == "Major damage") // object doet 40 damage en wordt gedeletet
        {
            //Destroy(target); werkt niet
            health -= 40;
        }

        else if (target.gameObject.tag == "Medium damage") // object doet 25 damage en wordt gedeletet
        {
            //Destroy(target); werkt niet
            health -= 25;
        }

        else if (target.gameObject.tag == "Low damage") // object doet 15 damage en wordt gedeletet
        {
            //Destroy(target); werkt niet
            health -= 15;
        }
    }
	
	// Update is called once per frame
	void Update () {

	    if (health >= 100) //health kan niet boven 100 uit komen
            health = 100;

        if (health <= 0){ // blind guy heeft geen health meer, game over
            //Destroy(gameObject);
            Application.LoadLevel("GameOver");
        }

	}
}
