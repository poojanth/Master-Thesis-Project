using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Rock : MonoBehaviour
{
    MeshRenderer mesh;
    Rigidbody rigid;
    [SerializeField] float Timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();

        mesh.enabled = false;
        rigid.useGravity = false;
        
           
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > Timer)
    {
        mesh.enabled = true;
        rigid.useGravity = true;
    }
    }
}
