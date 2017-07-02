using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainColor : MonoBehaviour
{
  private MeshRenderer meshRenderer;

  void Start()
  {
    meshRenderer = GetComponentInChildren<MeshRenderer>();
  }

  public void SetColor(Color color)
  {
    var material = new Material(meshRenderer.material);
    material.SetColor("_Color", color);
    meshRenderer.material = material;
  }
}
