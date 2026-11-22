using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellsRow",menuName = "Rows/CellsRow",order = 1)]
public class CellsRow : Row<Cell>
{
    [SerializeField] private int _offset;

    public int Offset => _offset;
}
