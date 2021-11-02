using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public class Marker
    {
        public Vector3 position;
        public Quaternion rotation;

        public Marker(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }

    public List<Marker> markerList = new List<Marker>();

    private void FixedUpdate()
    {
        UpdateMarkerList();
    }

    public void UpdateMarkerList()
    {
        if(Vector2.Distance(transform.position, markerList[markerList.Count-1].position) > 1)
        {
            markerList.Add(new Marker(transform.position, transform.rotation));
        }
        else
        {
            return;
        }

    }

    public void ClearMarkerList()
    {
        markerList.Clear();
        markerList.Add(new Marker(transform.position, transform.rotation));
    }
}
