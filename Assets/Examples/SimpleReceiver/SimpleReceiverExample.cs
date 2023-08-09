using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityOSC;

public class SimpleReceiverExample : MonoBehaviour {
    public Quaternion currentRotation;
	private OSCReciever reciever;
    public Quaternion offsetRotation;
	public int port = 8338;

    public float scaleFactor;

    public int rotationX;
    public int rotationY;
    public int rotationZ;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    public float GforceX;
    public float GforceY;
    public float GforceZ;

    public float XlOffsetX;
    public float XlOffsetY;
    public float XlOffsetZ;
    public float XlOffsetTolerance;
    public int targetAccelX;

    public string path = "./values.csv";

    public bool recordSensorReadings;
    StreamWriter writer;

    public Vector3[] extremeAngles;
    public Vector3 targetAngles;
    // Use this for initialization
    void Start () {
		reciever = new OSCReciever();
		reciever.Open(port);
        currentRotation = transform.localRotation;

        if (recordSensorReadings == true)
        {
            writer = new StreamWriter(path, true);
        }

    }

    // Update is called once per frame
    void Update () {
		if(reciever.hasWaitingMessages()){
			OSCMessage msg = reciever.getNextMessage();


            //Write some text to the test.txt file

            GforceX = ((int)msg.Data[3] / 16384f);
            GforceY = ((int)msg.Data[4] / 16384f);
            GforceZ = ((int)msg.Data[5] / 16384f);

            GforceX = GforceX + XlOffsetX;
            GforceY = GforceY + XlOffsetY;


            Debug.Log(string.Format("message received: {0} {1}", msg.Address, (DataToString(msg.Data))+" " + GforceX.ToString() + " "+ GforceY.ToString()+" "+GforceZ.ToString()));
            
            //if( Mathf.Abs( (int) msg.Data[0] + (int)msg.Data[2] + (int)msg.Data[1] )> 4)
            //{
            //    transform.Rotate((int)msg.Data[0] * Time.deltaTime * scaleFactor, (int)msg.Data[2] * Time.deltaTime * scaleFactor, (int)msg.Data[1] * Time.deltaTime * scaleFactor);
            //}

            rotationX = (int) msg.Data[0];
            rotationY = (int) msg.Data[2];
            rotationZ = (int)msg.Data[1];




            /*
            if(Mathf.Abs(rotationX) > 2)
            {
                transform.Rotate(rotationX * Time.deltaTime * scaleFactor, 0, 0);
            }

            if (Mathf.Abs(rotationY) > 2)
            {
                transform.Rotate(0, rotationY * Time.deltaTime * scaleFactor, 0);
            }


            if (Mathf.Abs(rotationZ) > 2)
            {
                transform.Rotate(0, 0, rotationZ * Time.deltaTime * scaleFactor/2);
            }*/

            if (Mathf.Abs(rotationX) > 2)
            {
                if (rotationX > 0)
                {
                    rotationX = rotationX + (int)(rotationX * 0.1f);
                }

                if (Mathf.Abs(rotationZ) > Mathf.Abs(rotationX * 2))
                {
                    transform.Rotate(((rotationX + offsetX) * Time.deltaTime * scaleFactor) / 10, 0, 0);
                }
                else if (Mathf.Abs(rotationY) > Mathf.Abs(rotationX * 2))
                {
                    transform.Rotate(((rotationX + offsetX) * Time.deltaTime * scaleFactor) / 10, 0, 0);
                }
                else
                {
                    transform.Rotate((rotationX + offsetX) * Time.deltaTime * scaleFactor, 0, 0);
                }

            }

            if (Mathf.Abs(rotationY) > 2)
            {


                if (Mathf.Abs(rotationX) > Mathf.Abs(rotationY * 2))
                {
                    transform.Rotate(0, (-rotationY + offsetY) * Time.deltaTime * scaleFactor / 10, 0);
                }
                else if (Mathf.Abs(rotationZ) > Mathf.Abs(rotationY * 2))
                {
                    transform.Rotate(0, ((-rotationY + offsetY) * Time.deltaTime * scaleFactor) / 10, 0);
                }
                else
                {
                    transform.Rotate(0, (-rotationY + offsetY) * Time.deltaTime * scaleFactor, 0);
                }
            }

            /*
            if (Mathf.Abs(rotationZ) > 2)
            {

                if (Mathf.Abs(rotationX) > Mathf.Abs(rotationZ * 2))
                {
                    transform.Rotate(0, 0, ((rotationZ + offsetZ) * Time.deltaTime * scaleFactor) / 10);
                }
                else if (Mathf.Abs(rotationY) > Mathf.Abs(rotationZ * 2))
                {
                    transform.Rotate(0, 0, ((rotationZ + offsetZ) * Time.deltaTime * scaleFactor) / 10);
                }
                else
                {
                    transform.Rotate(0, 0, (rotationZ + offsetZ) * Time.deltaTime * scaleFactor);
                }
            }
            */
            if (recordSensorReadings == true)
            {
                writer.WriteLine(rotationX.ToString() + " , " + rotationY.ToString() + " , " + rotationZ.ToString());

                if (Input.GetKeyDown(KeyCode.D))
                {
                    writer.Close();
                }
            }


            if (checkDriftCorrectionX() == true && Mathf.Abs(Mathf.Abs(rotationX) + Mathf.Abs(rotationY)) < 10)
            {
                if (targetAccelX == -1)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(-90, transform.rotation.y, 0)), 0.1f);
                }

                if (targetAccelX == 0)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)), 0.1f);
                }

                if (targetAccelX == 1)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(90, transform.rotation.y, transform.rotation.z)), 0.1f);
                }
            }

            /*
            if (checkExtremeAngle()==true && (rotationX + rotationY + rotationZ) <20 )
            {
                //Debug.Log("lerping");
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetAngles), 0.1f);
            }*/

            // Quaternion q = new Quaternion((float)msg.Data[1], (float)msg.Data[3], (float)msg.Data[2], (float)msg.Data[0] );
            //Vector3 rotation = q.eulerAngles;

            //transform.localRotation = q;

            //transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y , transform.rotation.eulerAngles.z*-1);

            //Debug.Log(rotation);
            /*
            float MPU_X=(float)msg.Data[0];
            float MPU_Y=(float)msg.Data[1];
            float MPU_Z=(float)msg.Data[2];

            transform.rotation = Quaternion.Euler(new Vector3(MPU_X, MPU_Y, MPU_Z));
            */



        }

        if (Input.GetKeyDown(KeyCode.X))
       
             reciever.Close();

        }
	
	
	private string DataToString(List<object> data)
	{
		string buffer = "";
		
		for(int i = 0; i < data.Count; i++)
		{
			buffer += data[i].ToString() + " ";
		}
		
		buffer += "\n";
		
		return buffer;
	}

    private bool checkExtremeAngle()
    {

        for (int i=0; i < extremeAngles.Length; i++)
        {
            if(Quaternion.Angle(transform.rotation, Quaternion.Euler(extremeAngles[i]) ) < 30){
                targetAngles = extremeAngles[i];
                return true;
            }
        }

        return false;
    }

    private bool checkDriftCorrectionX()
    {
        if (GforceY < -0.8f && GforceY > -1.2f)
        {
            targetAccelX = -1;
            return true;
        }
        if (GforceY > -0.2 && GforceY < 0.2)
        {
            targetAccelX = 0;
            return true;

        }
        if (GforceY > 0.8 && GforceY < 1.2)
        {
            targetAccelX = 1;
            return true;

        }

        return false;

    }
}