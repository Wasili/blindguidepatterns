using UnityEngine;
using System.Collections;

public class GlobalGuide : MonoBehaviour 
{
    //roep deze methode aan om een velocity te krijgen die richting de muis cursor beweegt
    public static Vector3 AimTowardsMouse(Vector3 origin)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = origin.z;
        return (mousePos - origin).normalized;
    }
}