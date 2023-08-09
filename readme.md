# NodeMCU Based Motion Capture & Remote Physical Training

**Background:**

![The image shows left arm of the trainee as red, as it does not follow trainer.](https://i.ibb.co/XtKR5Tn/image.png)
The image shows left arm of the trainee as red, as it does not follow trainer's pre-recorded animation. Right arm is marked green as it is in sync with the trainer's arm.
![enter image description here](https://i.ibb.co/hC2P8p8/Node.png)

This repository is a spin-off from my university's final year project which was remote gamified workout lesson delivery. The system consisted of 3 parts:

1) Motion capture hardware consisting of arbitrary number of "nodes" (though as initial proof of concept, 8 were developed, 2 for each limb). Each node consisted of a NodeMCU wifi chip, connected to MPU 6050 via jumper cables, and a small battery.

2. Desktop based Unity app that would:
	A. Allow a trainer to record a workout.
	B. Allow the trainee to load a recorded workout and try to follow it
	C. As the trainee follows the trainer, identify the where the trainee is committing mistakes and highlight those areas.
	D. An analytics engine that provides post-workout analysis.
3. A web portal where trainers can share their workout files with trainees.

In this particular repository, web portal and post-workout analytics engine has been removed as it contained some proprietary code. Some other areas have also been stripped down. However core motion capture system remains and a pre-alpha proof of concept for live workout comparison also exist.

**How to run**
1) Open the repository root folder in unity 2018.x (you can try with later versions but I personally havent tested it)
2) Set up the NodeMCU hardware circuit using following configuration:
MPU6050  |  NodeMCU Pin
	VCC - > 3.3v
  GND  -> G        			     
  SCL  ->   D1 (GPIO05)   
  SDA     -> D2 (GPIO04)  
  INT   ->   D8 (GPIO15) 
  3) Open the MPU6050_DMP5_UDP sketch file in arduino IDE
  4) At line 116 and 117, add your computer's IP address and desired port number for the node (what port number you set doesnt matter as long as it is currently not under use on your computer by any other app.)
  5) Close Arduino.
  6) Open the project in unity. Open the "Exercise" scene in the editor. On the character on the right, select the limb that you want to link to the node that you configured in step 2-5. In inspector panel, find the port number field and make sure its reading from the same port number that node is sending the data to. Do this for all nodes and all limbs.
7)  Turn on the node. It will start as a wifi access point. From your phone or laptop, connect to the newly created access point and browse to 192.168.4.1. From there, add your wifi name and password, and restart the node. The newly created wifi access point will disappear and node will start to connect to your network.

8) Play the scene in unity, the character should follow your movements.

Will add a more detailed documentation later if I get time. Till then, feel free to shoot queries at twitter.com/ThisMyHandle
