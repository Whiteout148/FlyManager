using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRuleRealizer : IRealizeRule
{
    private ColorRule _rule;

    public ColorRuleRealizer(ColorRule rule)
    {
        _rule = rule;
    }

    public void Realize(List<Unit> units, List<SeatingPlace> places)
    {
        if(units.Count != _rule.UnitsColor.Count) 
            throw new IndexOutOfRangeException();
        if (places.Count != _rule.SeatsColor.Count)
            throw new IndexOutOfRangeException();

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].TryGetComponent(out Paintable paintable))
            {
                paintable.SetColor(_rule.UnitsColor[i]);
            }
        }

        for (int i = 0; i < places.Count; i++)
        {
            if (places[i].TryGetComponent(out Paintable paintable))
            {
                paintable.SetColor(_rule.SeatsColor[i]);
            }
        }
    }
}
