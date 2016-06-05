using UnityEngine;
using System.Collections;

public class ObjectDestroyer : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Blindguy") // object doet damage in "Blind" script, delete zichzelf in deze in aanraking met blindguy
        {
            Destroy(gameObject, 0.2f);  // de float is de delay, geeft duidelijkere feedback door de speler te laten zien wat de blindguy raakte
        }                               // daarnaast zorgt het ervoor dat het zichzelf niet sloopt voordat de damage gedaan kan worden
    }
}
