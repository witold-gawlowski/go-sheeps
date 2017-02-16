﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurManager : MonoBehaviour
{

  [SerializeField]
  private float shavedBellySize = 0.7f;

  [SerializeField]
  private float grownFurWidth;

  [SerializeField]
  private Material shavedMaterial;

  [SerializeField]
  private Material fullHairMaterial;

  [SerializeField]
  private float furGrowthDuration = 4.0f;

  private int furState = 1;
  private MeshRenderer meshRenderer;
  private Transform bodyTransform;
  private GroupTag groupTag;
  [HideInInspector]
  public GroupTag.Group initialAffiliation;
  void Awake()
  {
    meshRenderer = GetComponent<MeshRenderer>();
    groupTag = GetComponentInParent<GroupTag>();
    initialAffiliation = groupTag.Affiliation;
    if (groupTag.Affiliation == GroupTag.Group.Shaved)
    {
      print(gameObject.transform.parent.name);
      throw new Exception("Sheep can't be shaved from the start.");
    }
  }


  public void Shave()
  {
    if (furState == 0)
    {
      return;
    }
    meshRenderer.material = shavedMaterial;
    transform.localScale = new Vector3(shavedBellySize, shavedBellySize, 1);
    furState = 0;
    groupTag.Affiliation = GroupTag.Group.Shaved;
    Invoke("Grow", furGrowthDuration);
  }

  public void Grow()
  {
    if (furState == 1)
    {
      return;
    }
    meshRenderer.material = fullHairMaterial;
    transform.localScale = new Vector3(grownFurWidth, grownFurWidth, 1);
    furState = 1;
    groupTag.Affiliation = initialAffiliation;
  }

}
