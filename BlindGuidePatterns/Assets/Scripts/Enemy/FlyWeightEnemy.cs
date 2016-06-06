using UnityEngine;
using System.Collections;


public abstract class FlyWeightEnemy : MonoBehaviour
{
    public abstract void Start();
    public abstract void Awake();
    public abstract void Update();
    public abstract void Attack();
    public abstract void OnBecameVisible();

}
