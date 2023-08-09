using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityOSC;
public class QuaternionRotation : MonoBehaviour
{

    public Quaternion currentRotation;
    private OSCReciever reciever;
    public Quaternion offsetRotation;
    public int port = 8338;



    // Start is called before the first frame update
    void Start()
    {
        reciever = new OSCReciever();
        reciever.Open(port);
        //currentRotation = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (reciever.hasWaitingMessages())
        {
            OSCMessage msg = reciever.getNextMessage();


            //Write some text to the test.txt file


            Debug.Log(string.Format("message received: {0} {1}", msg.Address, DataToString(msg.Data)));

            Quaternion q = new Quaternion((float)msg.Data[0], (float)msg.Data[1], (float)msg.Data[2], (float)msg.Data[3]);

            transform.rotation = q;
            //transform.rotation = Quaternion.Inverse(Quaternion.identity) * q;

        }
    }

    private string DataToString(List<object> data)
    {
        string buffer = "";

        for (int i = 0; i < data.Count; i++)
        {
            buffer += data[i].ToString() + " ";
        }

        buffer += "\n";

        return buffer;
    }
}
