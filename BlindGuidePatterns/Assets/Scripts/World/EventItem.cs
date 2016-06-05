using UnityEngine;
using System.Collections;

public class EventItem : MonoBehaviour {
    public enum objectType { pullable, meltable, freezable };
    public objectType type = objectType.pullable;

    BlindGuyTutorial blindGuyTutorial;
    Transform blindGuyTransform, myTransform;
    public float triggerRadius;
    bool triggered = false;

    string myTag;

    public Vector3 startPos;

    Geyser myGeyser;

	void Start() 
    {
        if (gameObject.GetComponent<Geyser>() != null)
        {
            myGeyser = gameObject.GetComponent<Geyser>();
            myGeyser.enabled = false;
        }

        blindGuyTutorial = GameObject.FindWithTag("Blindguy").GetComponent<BlindGuyTutorial>();
        blindGuyTransform = blindGuyTutorial.transform;
        myTransform = gameObject.transform;
        startPos = myTransform.position;

        if (type == objectType.pullable)
        {
            myTag = gameObject.tag;
            gameObject.tag = "Untagged";
        }
        else
            GetComponent<Collider2D>().enabled = false;
	}
	
	void Update() 
    {
        if (blindGuyTutorial == null || blindGuyTransform == null)
        {
            blindGuyTutorial = GameObject.FindWithTag("Blindguy").GetComponent<BlindGuyTutorial>();
            blindGuyTransform = blindGuyTutorial.transform;
            return;
        }

        if (Vector2.Distance(blindGuyTransform.position, myTransform.position) < triggerRadius && !triggered)
        {
            triggered = true;
            switch (type)
            {
                case objectType.pullable:
                    gameObject.tag = myTag;
                    blindGuyTutorial.StartEvent(gameObject.GetComponent<Rigidbody2D>());
                    break;
                case objectType.meltable:
                    GetComponent<Collider2D>().enabled = true;
                    blindGuyTutorial.StartEvent(gameObject);
                    break;
                case objectType.freezable:
                    GetComponent<Collider2D>().enabled = true;
                    if (myGeyser != null)
                    {
                        myGeyser.enabled = true;
                        blindGuyTutorial.StartEvent(myGeyser);
                    }
                    break;
            }
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 200, 200, 0.3f);
        Gizmos.DrawSphere(transform.position, triggerRadius);
    }
}
