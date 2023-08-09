using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class recorder : MonoBehaviour
{
    //records file in SureFit (.sf) file format
    //Writes in L1,L2,R1,R2 format

    public float interval;
    public float timeSinceLastRecorded;
    public GameObject[] trackableLimbs;

    public string path = "./animation.csv";
    StreamWriter writer;
    public bool recordReadings;

    string currString;
    // Start is called before the first frame update
    void Start()
    {
        if (recordReadings == true)
        {
            writer = new StreamWriter(path,true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastRecorded += Time.deltaTime;
        if (timeSinceLastRecorded >= interval)
        {
            timeSinceLastRecorded = 0;
            
            if (recordReadings == true)
            {
                //writer.WriteLine(rotationX.ToString() + " , " + rotationY.ToString() + " , " + rotationZ.ToString());
                updateCurrString();
                //Debug.Log(currString);
                writer.WriteLine(currString);
            }

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            writer.Flush();
            writer.Close();
        }
    }

    public void updateCurrString()
    {
        currString = "";
        for (int i=0; i<trackableLimbs.Length; i++)
        {
            if (currString == "")
            {
                currString = currString + trackableLimbs[i].transform.rotation.eulerAngles.x.ToString() + " , " + trackableLimbs[i].transform.rotation.eulerAngles.y.ToString() + " , " + trackableLimbs[i].transform.rotation.eulerAngles.z.ToString();
            }
            else
            {
                currString = currString + " , " + trackableLimbs[i].transform.rotation.eulerAngles.x.ToString() + " , " + trackableLimbs[i].transform.rotation.eulerAngles.y.ToString() + " , " + trackableLimbs[i].transform.rotation.eulerAngles.z.ToString();
            }


        }
    }

}
