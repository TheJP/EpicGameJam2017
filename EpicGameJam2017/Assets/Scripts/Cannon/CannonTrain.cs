using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CannonTrain : MonoBehaviour
{
    public Vector3 rotationCenter = Vector3.zero;
    public float rotationSpeed = 60.0f;

    private TrainColor trainColor;

    // Use this for initialization
    void Start()
    {
        LookAt2D(rotationCenter);

        trainColor = GetComponentInChildren<TrainColor>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotationCenter, transform.forward, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var unicorn = other.GetComponent<Unicorn>();
        if(unicorn != null && !unicorn.IsFlying)
        {
            unicorn.Stun();
        }
    }

    public void SetColor(Color color)
    {
        trainColor.SetColor(color);
    }

    private void LookAt2D(Vector3 point)
    {
        var dx = point.x - transform.position.x;
        var dy = point.y - transform.position.y;
        var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
