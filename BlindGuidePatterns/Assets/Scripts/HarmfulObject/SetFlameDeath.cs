using UnityEngine;
using System.Collections;

public class SetFlameDeath : MonoBehaviour {

    GameObject blindMan;

    void Awake()
    {
        blindMan = GameObject.FindWithTag("Blindguy");
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Blindguy")
        {
            blindMan.GetComponent<BlindGuyAI>().SetFlameDeath();
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Blindguy")
        {
            blindMan.GetComponent<BlindGuyAI>().SetFlameDeath();
        }
    }
}
