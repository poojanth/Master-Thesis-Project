using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessStreet : MonoBehaviour
{
    public Transform player;
    public GameObject groundPrefab;
    private Vector3 spawnPosition = Vector3.zero;
    private float segmentLength = 50f; // Adjust to your segment length
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (player.position.z > spawnPosition.z - (2 * segmentLength))
        {
            Instantiate(groundPrefab, spawnPosition, Quaternion.identity);
            spawnPosition.z += segmentLength;
        } 
    }
}
