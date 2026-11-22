using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRuleHandler 
{
    bool TryCombine(Unit unit, SeatingPlace place);
}
