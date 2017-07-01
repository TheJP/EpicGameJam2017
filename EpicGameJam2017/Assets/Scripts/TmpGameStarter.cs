using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpGameStarter : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<Controller>().StartGame(new[] { Players.A, Players.B });
    }
}
