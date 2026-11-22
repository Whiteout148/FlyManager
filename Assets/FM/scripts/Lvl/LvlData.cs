using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "LvlData",menuName = "Lvl/LvlData",order = 1)]
public class LvlData : ScriptableObject
{
    [SerializeField] private List<CellsRow> _grid;
    [SerializeField] private List<PlacementRule> _rules;
    [SerializeField] private List<UnitData> _units;
    [SerializeField] private SeatingPlace _placePrefab;
    [SerializeField] private PlaneData _planeData;
    [SerializeField] private int _placesCount;

    public IReadOnlyList<CellsRow> Grid => _grid;
    public IReadOnlyList<PlacementRule> Rules => _rules;
    public IReadOnlyList<UnitData> Units => _units;
    public SeatingPlace Place => _placePrefab;
    public PlaneData PlaneData => _planeData;
    public int PlacesCount => _placesCount;
}
