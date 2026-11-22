using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridArranger _gridArranger;
    [SerializeField] private LvlData _config;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private PlaneGenerator _planeGenerator;

    private void Start()
    {
        Generate(_config);
    }

    private void Generate(LvlData lvlConfig)
    {
        IReadOnlyList<RuntimeCellsRow> grid = _gridArranger.Arrange(lvlConfig.Grid);

        List<Unit> units = new List<Unit>();

        int unitIndex = 0;

        for (int i = 0; i < grid.Count; i++)
        {
            RuntimeCellsRow row = grid[i];

            for (int x = 0; x < row.Count; x++)
            {
                Cell cell = row[x];

                units.Add(Instantiate(lvlConfig.Units[unitIndex].Prefab, cell.transform.position, Quaternion.identity, transform));
                unitIndex++;
            }
        }

        AirPlane plane = _planeGenerator.Generate(lvlConfig);
        plane.ShowInside();
        List<SeatingPlace> places = _planeGenerator.PlaceSeats(plane, lvlConfig);

        for (int i = 0; i < lvlConfig.Rules.Count; i++)
        {
            IRealizeRule realizer = lvlConfig.Rules[i].CreateRealizer();
            realizer.Realize(units, places);
        }
    }
}
