using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainColor : MonoBehaviour
{
  private MeshRenderer meshRenderer;
  private Color? color;

  void Start()
  {
    meshRenderer = GetComponentInChildren<MeshRenderer>();
    SetColor();
  }

  public void SetColor(Color color)
  {
    this.color = color;
  }

  private void SetColor()
  {
    if(meshRenderer == null || color == null)
    {
      return;
    }

    var material = new Material(meshRenderer.material);
    material.SetColor("_Color", color.Value);
    meshRenderer.material = material;
  }
}
