using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlane : MonoBehaviour
{
    [SerializeField] private List<PlaneSection> _sections;
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

    public IReadOnlyList<PlaneSection> Sections => _sections;
    public Transform Start => _start;
    public Transform End => _end;

    public void AddSection(PlaneSection section)
    {
        if (section == null)
            throw new System.ArgumentNullException(nameof(section));

        _sections.Add(section);
    }

    public void ShowInside()
    {
        foreach (var section in Sections)
        {
            if (section.TryGetComponent(out Paintable paintable))
            {
                paintable.Hide();
            }
        }
    }
}
