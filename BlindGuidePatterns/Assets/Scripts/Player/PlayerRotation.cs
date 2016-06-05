using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {
    public enum RotationStyle { fullRotation, flipRotation, arcRotation };
    public RotationStyle rotationStyle;
    public float maxDownwardsAngle = 30f, maxUpwardsAngle = 30f;
    Transform myTransform;

	void Start () 
    {

        myTransform = transform;
	}
	
	void Update () 
    {
        switch (rotationStyle)
        {
            case RotationStyle.fullRotation:
                FullRotation();
                break;
            case RotationStyle.flipRotation:
                FlipRotation();
                break;
            case RotationStyle.arcRotation:
                ArcRotation();
                break;
        }
    }

    void FlipRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
            myTransform.rotation = new Quaternion(0, 0, 0, myTransform.rotation.w);
        else
            myTransform.rotation = new Quaternion(0, 180, 0, myTransform.rotation.w);
    }

    void ArcRotation()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetVector = transform.position - mousePos;
        float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg + 180;

        if (mousePos.x < transform.position.x)
        {
            angle = Mathf.Clamp(angle, 180 - maxUpwardsAngle, 180 + maxDownwardsAngle);
            myTransform.localScale = new Vector3(myTransform.localScale.x, -1, myTransform.localScale.z);
        }
        else
        {
            if (angle < 180)
                angle = Mathf.Clamp(angle, 0, maxUpwardsAngle);
            else
                angle = Mathf.Clamp(angle, 360 - maxDownwardsAngle, 359);

            myTransform.localScale = new Vector3(myTransform.localScale.x, 1, myTransform.localScale.z);
        }
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        myTransform.rotation = newRotation;
    }

    void FullRotation()
    {
        transform.localScale = new Vector3(1, 1, 1);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetVector = transform.position - mousePos;
        float angle = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
        Quaternion newRotation = new Quaternion();
        newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = newRotation;
    }
}
