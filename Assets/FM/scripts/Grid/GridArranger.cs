using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridArranger : MonoBehaviour
{
    [SerializeField] private Cell _prefab;

    public IReadOnlyList<RuntimeCellsRow> Arrange(IReadOnlyList<CellsRow> rows)
    {
        List<RuntimeCellsRow> result = new();

        Vector3 cellScale = _prefab.transform.localScale;

        int maxWidth = 0;

        for (int i = 0; i < rows.Count; i++)
        {
            maxWidth = Mathf.Max(maxWidth, rows[i].Count + rows[i].Offset);
        }

        float width = maxWidth * cellScale.x;
        float length = rows.Count * cellScale.z;

        Vector3 centerOffset = new Vector3(-width / 2f + cellScale.x / 2f, 0f, length / 2f - cellScale.z / 2f);

        for (int y = 0; y < rows.Count; y++)
        {
            CellsRow row = rows[y];

            GameObject rowObject = new GameObject($"Row_{y}");
            rowObject.transform.SetParent(transform, false);

            RuntimeCellsRow runtimeRow = rowObject.AddComponent<RuntimeCellsRow>();

            rowObject.transform.localPosition = new Vector3(row.Offset * cellScale.x, 0, -y * cellScale.z) + centerOffset;

            for (int x = 0; x < row.Count; x++)
            {
                Cell cell = Instantiate(_prefab, rowObject.transform);

                cell.transform.localPosition = new Vector3(x * cellScale.x, 0, 0);

                runtimeRow.Add(cell);
            }

            result.Add(runtimeRow);
        }

        return result;
    }
}
