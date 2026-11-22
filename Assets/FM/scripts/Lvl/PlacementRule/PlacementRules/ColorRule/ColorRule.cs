using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorRule", menuName = "PlacementRule/ColorRule", order = 1)]
public class ColorRule : PlacementRule
{
    [SerializeField] private List<Color> _unitsColor;
    [SerializeField] private List<Color> _seatsColor;

    public IReadOnlyList<Color> UnitsColor => _unitsColor;
    public IReadOnlyList<Color> SeatsColor => _seatsColor;

    public override IRealizeRule CreateRealizer()
    {
        return new ColorRuleRealizer(this);
    }
}
