using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public Dictionary<Collider, Action<Rigidbody>> interactionDict = new Dictionary<Collider, Action<Rigidbody>>();
    public Dictionary<Collider, Action> infoData = new Dictionary<Collider, Action>();
    public Dictionary<Collider, Action<int>> damageDict = new Dictionary<Collider, Action<int>>();
    public Action<int, float> moveBuff;
    public Action<string> printInfo;
    protected override void Init()
    {

    }
    protected override void Reset()
    {

    }
}
