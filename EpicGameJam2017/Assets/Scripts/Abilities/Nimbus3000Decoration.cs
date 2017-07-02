using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nimbus3000Decoration : MonoBehaviour
{
    private float startTime;
    private Vector3 startPosition;
    public float animationDuration = 1f;

    public void Start()
    {
        startTime = Time.time;
        startPosition = transform.localPosition;
    }

    public void Update()
    {
        transform.localPosition = Vector3.Lerp(startPosition, Vector3.zero, (Time.time - startTime) / animationDuration);
    }
}
