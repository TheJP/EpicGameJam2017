using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMarker : MonoBehaviour
{
    [Tooltip("MeshRenderer that is used to draw the marker and allows you to change the marker color")]
    public MeshRenderer meshRenderer;

    [Tooltip("Speed of this marker")]
    public float speed = 1f;

    [Tooltip("Objects, that belong to the player and potentionally contain markable components")]
    public GameObject[] playerObjects;

    /// <summary>Player for which this marker is used.</summary>
    public Players Player { get; private set; }

    /// <summary>Contains all objects, which will be marked by this instance of a player marker.</summary>
    private readonly List<PlayerMarkable> markables = new List<PlayerMarkable>();

    /// <summary>Sets the player of and changes the color this marker.</summary>
    public void SetPlayer(Players player)
    {
        Player = player;
        meshRenderer.material = new Material(meshRenderer.material) { color = Constants.PlayerColors[player] };
    }

    public void Start()
    {
        markables.AddRange(playerObjects
            .SelectMany(gameObject => gameObject.GetComponentsInChildren<PlayerMarkable>())
            .Where(markable => markable.Player == Player));
    }

    public void Update()
    {
        // Move marker to active markable object
        var target = markables.FirstOrDefault(markable => markable.ShouldBeMarked);
        if (target == null) { return; }
        var direction = target.Position - transform.position;
        var distance = direction.magnitude;
        if (distance <= speed * Time.deltaTime)
        {
            transform.position = target.Position;
        }
        else
        {
            transform.position += (direction / distance) * speed * Time.deltaTime;
        }
    }
}
