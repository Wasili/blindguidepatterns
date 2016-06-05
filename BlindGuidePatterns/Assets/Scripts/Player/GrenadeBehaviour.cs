using UnityEngine;
using System.Collections;

public class GrenadeBehaviour : MonoBehaviour {
    public float grenadeMaxCD, pause, grenadeCooldown = 0;        
    public AudioClip bark;
    public BarkCooldownVisual cooldownVisualObject;

    string barkButtonName = "Bark";

	void Start () 
    {
        if (cooldownVisualObject == null)
        {
            cooldownVisualObject = GameObject.Find("clock").GetComponent<BarkCooldownVisual>();
        }
	}

    void Bark()
    {
        grenadeCooldown -= Time.deltaTime;
        if (Input.GetButton(barkButtonName) && grenadeCooldown <= 0)
        {
            AudioSource.PlayClipAtPoint(bark, transform.position);
            GameObject.FindWithTag("Blindguy").GetComponent<BlindGuyAI>().stopTimer = pause;
            grenadeCooldown = grenadeMaxCD;
            cooldownVisualObject.SetDisabled(grenadeCooldown);
        }
    }

    //deze wordt gebruikt voor gooiende granaten, die worden nu niet meer gebruikt maar ik laat ze staan als back-up
    /*void ThrowGrenade()
    {
        grenadeCooldown -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && grenadeCooldown <= 0 && grenadeCharges > 0)
        {
            Instantiate(grenade, new Vector3(transform.position.x, transform.position.y), new Quaternion(0, 0, 0, 0));
            grenadeCooldown = grenadeMaxCD;
            grenadeCharges--;
        }
    }*/

	void Update () 
    {
        Bark();
        //ThrowGrenade();
	}
}
