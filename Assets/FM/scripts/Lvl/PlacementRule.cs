using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class PlacementRule : ScriptableObject
{
    [SerializeField] private bool _useRule;

    public abstract IRealizeRule CreateRealizer();
}
