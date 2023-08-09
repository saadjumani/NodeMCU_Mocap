using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comparison : MonoBehaviour
{
    public Material rightAngle;
    public Material wrongAngle;

    public GameObject comparableObj;

    public GameObject child;
    public GameObject child2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Quaternion.Angle(transform.rotation,comparableObj.transform.rotation) > 20)
        {
            child.GetComponent<Renderer>().material = wrongAngle;
            child2.GetComponent<Renderer>().material = wrongAngle;
        }
        else
        {
            child.GetComponent<Renderer>().material = rightAngle;
            child2.GetComponent<Renderer>().material = rightAngle;

        }
    }
}
