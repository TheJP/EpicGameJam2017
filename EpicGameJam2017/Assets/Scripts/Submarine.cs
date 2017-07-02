using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Submarine : MonoBehaviour
{
    public float speed = 100f;
    public float turnSpeed = 10f;

    private Vector3 currentTarget;

    private void Start()
    {
        SetNewTarget();
    }

    public void Update()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        // Fly to target
        body.AddForce(transform.up * speed * Time.deltaTime);
        var direction = -Vector3.Cross(currentTarget - transform.position, transform.up).z;
        transform.Rotate(Vector3.forward, Mathf.Sign(direction) * turnSpeed * Time.deltaTime, Space.World);

        // Reached target.. Flying to next one
        if (Vector3.Distance(transform.position, currentTarget) < 2f * body.velocity.magnitude)
        {
            SetNewTarget();
        }
    }

    private void SetNewTarget()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(Random.value, Random.value, 0f));
        // Cool visualization: Debug.DrawRay(ray.origin, ray.direction, Color.green, 99999f);
        // Only works with orthographic camera! With perspectice camera the formula has to be setup and solved
        currentTarget = new Vector3(ray.origin.x, ray.origin.y, transform.position.z);
    }
}
