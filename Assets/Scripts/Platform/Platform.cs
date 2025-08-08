using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]public PlatformType type;
    IPlatformParts IPlatform;
    void Start()
    {
        IPlatform = IPlatformParts.Factory(type,transform);
        GameManager.GetInstance.interactionDict.Add(GetComponent<Collider>(), IPlatform.CollisionInteract);
    }
}
[Serializable]
public enum PlatformType {jump,dash }