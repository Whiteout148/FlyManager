using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Lvl/Unit", order = 1)]
public class UnitData : ScriptableObject
{
    [SerializeField] private Unit _prefab;

    public Unit Prefab => _prefab;
}
