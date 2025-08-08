using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]public PlatformType type;
    IInteraction IPlatform;
    void Start()
    {
        Collider col = GetComponent<Collider>();
        IPlatform = IInteraction.Factory(type,transform);
        GameManager.GetInstance.interactionDict.Add(col, IPlatform.CollisionInteract);
        GameManager.GetInstance.infoData.Add(col, IPlatform.ViewInfo);
    }
}
[Serializable]
public enum PlatformType {jump,dash }