using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSection : MonoBehaviour
{
    [SerializeField] private List<Transform> _places;
    [SerializeField] private Transform _end;

    public IReadOnlyList<Transform> Places => _places;
    public Transform End => _end;
}
