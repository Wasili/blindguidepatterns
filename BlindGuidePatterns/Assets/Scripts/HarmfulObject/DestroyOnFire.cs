using UnityEngine;
using System.Collections;

public class DestroyOnFire : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D fire)
    {
        if (fire.gameObject.tag == "FireAttack")
        {
            Destroy(gameObject);
        }
    }
}
