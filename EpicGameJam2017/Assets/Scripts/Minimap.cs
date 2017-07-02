using System.Collections;

using UnityEngine;


public class Minimap : MonoBehaviour
{
    public float MinTimeAlive = 120.0f;
    public float MaxTimeAlive = 240.0f;
    public float MinTimeDestroyed = 120.0f;
    public float MaxTimeDestroyed = 240.0f;

    private ParticleSystem explosion;
    private MeshRenderer meshRenderer;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        explosion = GetComponentInChildren<ParticleSystem>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        StartCoroutine(RunExplodingMinimap());
    }

    private IEnumerator RunExplodingMinimap()
    {
        explosion.Stop();
        meshRenderer.enabled = true;
        spriteRenderer.enabled = true;

        yield return new WaitForSeconds(Random.Range(MinTimeAlive, MaxTimeAlive));

        explosion.Play();
        meshRenderer.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(Random.Range(MinTimeDestroyed, MaxTimeDestroyed));

        StartCoroutine(RunExplodingMinimap());
    }
}
