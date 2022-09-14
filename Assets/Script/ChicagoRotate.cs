using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicagoRotate : MonoBehaviour
{
    public GameObject cube;

    [SerializeField]
    private float zRotate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        zRotate = cube.transform.localRotation.eulerAngles.z;        
    }
}
