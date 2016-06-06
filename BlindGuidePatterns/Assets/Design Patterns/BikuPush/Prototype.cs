using UnityEngine;
using System.Collections;

public abstract class Prototype : MonoBehaviour {

    // the interface to acces to SnowmanToClone, method overrided in SnowmanToClone
    public abstract Prototype Clone();
}
