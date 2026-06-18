using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EndlessRoad : MonoBehaviour
{
    public Transform player; // Player (FirstPersonController or capsule) Transform
    public List<GameObject> roadSegments; // Array of road segments
    public float segmentLength = 100.0f; // Length of each road segment on the X-axis

    void Start()
    {
        // Start by spawning a few road segments in front of the player
       if(roadSegments != null && roadSegments.Count > 0)
        {
            roadSegments = roadSegments.OrderBy(r => r.transform.position.x).ToList();
        }
    }

    public void MoveRoad()
    {
        GameObject movedRoad = roadSegments[0];
        roadSegments.Remove(movedRoad);

        Vector3 lastRoadPosition = roadSegments[roadSegments.Count - 1].transform.position;

        // Set the new position for the moved road segment (keep the Z-axis unchanged)
        float newX = lastRoadPosition.x + segmentLength;
        movedRoad.transform.position = new Vector3(newX, lastRoadPosition.y, lastRoadPosition.z);

        roadSegments.Add(movedRoad);
    }


}
