using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotive_Script : MonoBehaviour {

    //public Player player;
    public float speed = 0.01f;
    public float radius = 10.0f;
    public float angle = 0.0f;

    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {

        this.angle = 0;
        this.rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 newPosition = calcCoordinates();
        rigidBody.MovePosition(newPosition);
	}

    private Vector2 calcCoordinates()
    {
        this.angle += speed;

        float xPosition = Mathf.Cos(angle) * radius;
        float yPosition = Mathf.Sin(angle) * radius; ;

        return new Vector2(xPosition, yPosition);
    }
}
