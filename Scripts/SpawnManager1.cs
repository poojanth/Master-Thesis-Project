using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager1 : MonoBehaviour
{
    EndlessRoad1 roadSpawner;

    // Start is called before the first frame update
    void Start()
    {
       roadSpawner = GetComponent<EndlessRoad1>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTriggerEntered1()
    {
        roadSpawner.MoveRoad1();
    }
}
