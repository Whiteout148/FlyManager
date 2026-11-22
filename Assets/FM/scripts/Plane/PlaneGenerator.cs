using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
    private const float SectionLenght = 2f;

    public AirPlane Generate(LvlData data)
    {
        AirPlane plane = Instantiate(data.PlaneData.Plane, transform);
        plane.transform.localPosition = Vector3.zero;

        Vector3 position = plane.Start.position;

        for (int i = 0; i < data.PlaneData.SectionCount; i++)
        {
            PlaneSection section = Instantiate(data.PlaneData.Section, plane.transform);

            section.transform.position = position;
            section.transform.rotation = plane.transform.rotation;

            plane.AddSection(section);

            position = section.End.position;
        }

        plane.End.position = position;

        return plane;
    }

    public List<SeatingPlace> PlaceSeats(AirPlane plane, LvlData data)
    {
        List<SeatingPlace> places = new List<SeatingPlace>();
        int count = 0;

        foreach (PlaneSection section in plane.Sections)
        {
            foreach (Transform place in section.Places)
            {
                if (count >= data.PlacesCount)
                    return places;

                SeatingPlace newPlace = Instantiate(data.Place, place);
                newPlace.transform.localScale = Vector3.one;
                newPlace.transform.localPosition = Vector3.zero;

                places.Add(newPlace);
                count++;
            }
        }

        return places;
    }
}