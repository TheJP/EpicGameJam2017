using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

  public Players player;

  public float rotationArc = 90.0f;
  public float rotationSpeed = 60.0f;

  public float maxFireDistance = 6;
  public float fireDistanceSpeed = 2;

  public GameObject shell;
}
