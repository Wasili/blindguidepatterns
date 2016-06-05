using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Vector3 targetPos;
    float acceleration;
    Vector3 targetVelo = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    void Start()
    {
        targetPos = this.transform.position;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += velocity * speed * Time.deltaTime;
    }

    void MoveMouse()
    {
        if (Vector2.Distance(this.transform.position, targetPos) > 0.5f)
        {
            this.transform.position += targetVelo * Time.deltaTime * speed * acceleration;

            if (Vector2.Distance(this.transform.position, targetPos) < 5)
            {
                acceleration -= Time.deltaTime;
            }
        }

        acceleration = Mathf.Clamp(acceleration, 0, 1);
        if (Input.GetMouseButtonDown(0))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = transform.position.z;
            targetVelo = (targetPos - this.transform.position).normalized;
            acceleration = 1;
        }
    }
}
