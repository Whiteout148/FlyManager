using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plane", menuName = "AirPlane/Plane", order = 1)]
public class PlaneData : ScriptableObject
{
    [SerializeField] private AirPlane _airPlane;
    [SerializeField] private PlaneSection _sectionPrefab;
    [SerializeField] private int _sectionsCount;

    public AirPlane Plane => _airPlane;
    public PlaneSection Section => _sectionPrefab;
    public int SectionCount => _sectionsCount;
}
