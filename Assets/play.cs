using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class play : MonoBehaviour
{
    public float interval;
    public float timeSinceLastRecorded;
    public GameObject[] trackableLimbs;

    public string path = "./animation_read.csv";

    StreamReader reader;

    public bool recordReadings;

    string currString;

    string[] splitStrings;
    float[] angleValues;

    // Start is called before the first frame update
    void Start()
    {
        reader = new StreamReader(path, true);
        angleValues = new float[trackableLimbs.Length * 3];
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastRecorded += Time.deltaTime;

        if (timeSinceLastRecorded > interval) { 
            timeSinceLastRecorded = 0;
        {
            if (!reader.EndOfStream)
            {
                splitStrings = reader.ReadLine().Trim().Split(',');

                for (int i = 0; i < splitStrings.Length; i++)
                {
                    angleValues[i] = float.Parse(splitStrings[i]);
                }
                for (int i = 0; i < trackableLimbs.Length; i++)
                {
                    trackableLimbs[i].transform.rotation = Quaternion.Euler(angleValues[(i * 3) + 0], angleValues[(i * 3) + 1], angleValues[(i * 3) + 2]);
                }

            }
        }
        }

    }
}
