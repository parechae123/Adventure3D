using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public Dictionary<Collider, Action<Rigidbody>> interactionDict = new Dictionary<Collider, Action<Rigidbody>>();
    protected override void Init()
    {

    }
    protected override void Reset()
    {

    }
}
