using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRealizeRule 
{
    void Realize(List<Unit> units, List<SeatingPlace> places);
}
