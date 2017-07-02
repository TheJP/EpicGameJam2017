using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nimbus3000Decoration : MonoBehaviour
{
    private float startTime;
    private Vector3 startPosition;
    private Vector3 despawnPosition;
    private bool flyAway = false;

    public float animationDuration = 1f;

    public void Start()
    {
        startTime = Time.time;
        startPosition = transform.localPosition;
        despawnPosition = -transform.localPosition;
    }

    public void Update()
    {
        var timeFactor = (Time.time - startTime) / animationDuration;
        // Fly away (at the end)
        if (flyAway) { transform.localPosition = Vector3.Lerp(Vector3.zero, despawnPosition, timeFactor); }
        // Fly in (at the start)
        else { transform.localPosition = Vector3.Lerp(startPosition, Vector3.zero, timeFactor); }
    }

    public IEnumerator FlyAway()
    {
        flyAway = true;
        startTime = Time.time;
        yield return new WaitForSeconds(animationDuration);
        Destroy(gameObject);
    }
}
