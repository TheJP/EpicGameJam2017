using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [Tooltip("Type of this ingredient")]
    public Indgredients type;

    public void TimeUp()
    {
        // TODO: Give player score and init animation
        Destroy(gameObject);
    }
}
