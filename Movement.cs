using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ANT_Managed_Library;
using System.Collections.Generic;
using System;
using System.IO;
using Dynastream.Fit;



public class Movement : MonoBehaviour {



    private byte[] pageTosend;
    public float m_speed;
	Animator anim;
	public Rigidbody rocketPrefab;
	public Rigidbody rocketPrefab2;
	public Rigidbody rocketPrefab3;
	public Transform barrelEnd;
	private float chrono = 0f;
	public Text ChronoText;
	public Image Banane1;
	public Image Banane2;
	public Image Banane3;
	public Text BananeText;
    private int previousresistance;




	//variable use for speed display
	int prevRev;
	int prevMeasTime = 0;

	int stoppedCounter = 0;



	public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
	                 // How fast the tank moves forward and back.
	public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.




	private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
	private string m_TurnAxisName;              // The name of the input axis for turning.
	private Rigidbody m_Rigidbody;              // Reference used to move the tank.
	private float m_MovementInputValue;         // The current value of the movement input.
	private float m_TurnInputValue;             // The current value of the turn input.
	private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.


	private int countbanana; // the current number of bananas available

	private void Awake ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		chrono= 0f;
		SetcountText ();


		

	}


	private void OnEnable ()
	{
		// When the tank is turned on, make sure it's not kinematic.
		m_Rigidbody.isKinematic = false;

		// Also reset the input values.
		m_MovementInputValue = 0f;
		m_TurnInputValue = 0f;
	}


	private void OnDisable ()
	{
		// When the tank is turned off, set it to kinematic so it stops moving.
		m_Rigidbody.isKinematic = true;
	}


	private void Start ()
	{
		// The axes names are based on player number.
		m_MovementAxisName = "Vertical" ;
		m_TurnAxisName = "Horizontal" ;
		countbanana = 0;

		// Store the original pitch of the audio source.

	}


	private void Update ()
	{
		// Store the value of both input axes.
		m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
		//print (m_MovementInputValue);
		m_TurnInputValue = Input.GetAxis (m_TurnAxisName);
		// Store the value of both input axes.
		m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
//		print (m_MovementInputValue);



		

			if (Input.GetKeyDown(KeyCode.S))
			{
			print("init device and start background scan");
				AntManager.Instance.Init();
				backgroundScanChannel = AntManager.Instance.OpenBackgroundScanChannel(0);
				backgroundScanChannel.onReceiveData += ReceivedBackgroundScanData;
			}

			if (Input.GetKeyDown(KeyCode.D)) 
			{
				Debug.Log("setting resistance to 0 %");
				byte[] pageToSend = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 0 };
				FECChannel.sendAcknowledgedData(pageToSend);
			}
			if (Input.GetKeyDown(KeyCode.F)) 
			{
				Debug.Log("setting resistance to 100 %");
				byte[] pageToSend = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 200 };//unit is 0.50%
				FECChannel.sendAcknowledgedData(pageToSend);
			}
			if (Input.GetKeyDown(KeyCode.E)) 
			{
				Debug.Log("setting resistance to 75 %");
				byte[] pageToSend = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 150 };//unit is 0.50%
				FECChannel.sendAcknowledgedData(pageToSend);
			}



	}



	private void FixedUpdate ()
	{

        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Slope();
        Move();
		Turn ();
		Animating ();
		Banana ();
		chrono = Time.fixedTime;
		//print (chrono);
		SetcountText ();






	}

    void Slope()
    {

        float Angular = Mathf.Acos(Vector3.Dot(transform.forward, Vector3.up));
        Int32 angle = (Int32)(Angular * 180f / Mathf.PI/1f-90);
        float tiltAroundX = Input.GetAxis("Vertical");
        Debug.Log("Angular direction relative to the ground :" + angle + ".");

        if (angle !=previousresistance) {
            switch (angle)
            {

                case 1:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 10 };
                    FECChannel.sendAcknowledgedData(pageToSend);
                    previousresistance = angle;
                    break;
                case 2:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend2 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 20 };
                    FECChannel.sendAcknowledgedData(pageToSend2);
                    previousresistance = angle;
                    break;
                case 3:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend3 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 30 };
                    FECChannel.sendAcknowledgedData(pageToSend3);
                    previousresistance = angle;
                    break;
                case 4:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend4 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 40 };
                    FECChannel.sendAcknowledgedData(pageToSend4);
                    previousresistance = angle;
                    break;
                case 5:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend5 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 50 };
                    previousresistance = angle;
                    FECChannel.sendAcknowledgedData(pageToSend5);
                    break;
                case 6:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend6 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 60 };
                    FECChannel.sendAcknowledgedData(pageToSend6);
                    previousresistance = angle;
                    break;
                case 7:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend7 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 70 };
                    FECChannel.sendAcknowledgedData(pageToSend7);
                    previousresistance = angle;
                    break;
                case 8:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend8 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 80 };
                    FECChannel.sendAcknowledgedData(pageToSend8);
                    previousresistance = angle;
                    break;
                case 9:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend9 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 90 };
                    FECChannel.sendAcknowledgedData(pageToSend9);
                    previousresistance = angle;
                    break;
                case 10:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend10 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 100 };
                    FECChannel.sendAcknowledgedData(pageToSend10);
                    previousresistance = angle;
                    break;
                case 11:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend11 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 110 };
                    FECChannel.sendAcknowledgedData(pageToSend11);
                    previousresistance = angle;
                    break;
                case 12:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend12 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 120 };
                    FECChannel.sendAcknowledgedData(pageToSend12);
                    previousresistance = angle;
                    break;
                case 20:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend20 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 200 };
                    FECChannel.sendAcknowledgedData(pageToSend20);
                    previousresistance = angle;
                    break;
                case 13:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend13 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 130 };
                    FECChannel.sendAcknowledgedData(pageToSend13);
                    previousresistance = angle;
                    break;
                case 14:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend14 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 140 };
                    FECChannel.sendAcknowledgedData(pageToSend14);
                    previousresistance = angle;
                    break;
                case 15:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend15 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 150 };
                    FECChannel.sendAcknowledgedData(pageToSend15);
                    previousresistance = angle;
                    break;
                case 16:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend16 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 160 };
                    FECChannel.sendAcknowledgedData(pageToSend16);
                    previousresistance = angle;
                    break;
                case 17:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend17 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 170 };
                    FECChannel.sendAcknowledgedData(pageToSend17);
                    previousresistance = angle;
                    break;
                case 18:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend18 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 180 };
                    FECChannel.sendAcknowledgedData(pageToSend18);
                    previousresistance = angle;
                    break;
                case 19:
                    Debug.Log("setting resistance to " + 10 * angle);
                    byte[] pageToSend19 = new byte[8] { 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 4, 55, 190 };
                    FECChannel.sendAcknowledgedData(pageToSend19);
                    previousresistance = angle;
                    break;  
            }





        }
    }

    private void Move ()
	{

	
		Vector3 movement = transform.forward *(-1)* m_speed * Time.deltaTime;

		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}


	private void Turn ()
	{
		// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
	}

	//void OnTriggerEnter(Collider other) 
	//{
	//	if (other.gameObject.CompareTag ("PickUp"))
	//	{
	//		other.gameObject.SetActive (false);//là ajoute simplement en plus de désactiver(essaie sans compteur en premier lieu ) ta variable vitesse(par exemple speed=speed*1.5 pour ton objet, essaie de travailler avec tes variables à toi; quel que soit le nom du prefab que je te donne, juste change le nom comme de celui ci de "Pick Up" à ce dont t'as besoin
	//	}
	//}
	AntChannel backgroundScanChannel;
	AntChannel FECChannel;

	ushort deviceNumber;
	byte transType;


	void ReceivedBackgroundScanData(Byte[] data)
	{

		byte deviceType = (data[12]); // extended info Device Type byte
		//use the Extended Message Formats to identify nodes

		switch (deviceType)
		{

		case AntplusDeviceType.FitnessEquipment:
			{
				backgroundScanChannel.Close();
				deviceNumber = (ushort)((data[10]) | data[11] << 8);

				transType = data[13];
				Debug.Log("found FEC trainer, opening channel, device number is "+ deviceNumber);

				FECChannel = AntManager.Instance.OpenChannel(ANT_ReferenceLibrary.ChannelType.BASE_Slave_Receive_0x00, 1, deviceNumber, 17, transType, 57, 8192, false);
				FECChannel.onReceiveData += FECData;
				FECChannel.hideRXFAIL = true;
				break;
			}

		default:
			{
				
				break;
			}
		}

	}

	// Update is called once per frame
	public int deviceNumber2;


	//variable use for speed display
	int prevRev2;
	int prevMeasTime2 = 0;

	int stoppedCounter2 = 0;
	public float GetSpeed(Byte[] data)
	{
		float speed=0;
		//speed formula as described in the ant+ device profile doc
		int currentRevCount = (data[6]) | data[7] << 8;
		int currentMeasTime = (data[4]) | data[5] << 8;
		//print (prevRev2);
		//print (currentRevCount);
		//print (currentMeasTime);
		if (prevRev2 > 0)
		{

			if (currentMeasTime != prevMeasTime2 || currentRevCount != prevRev2)
			{
				float s = (2.070f * (currentRevCount - prevRev2) * 1024) / (currentMeasTime - prevMeasTime2);
				s *= 3.6f;
				//print (s);

				speed = currentMeasTime*1.0f/1024*3.6f*2.070f;
				stoppedCounter2 = 0;
			}
			else {

				stoppedCounter2++;
			}

		}

		if (stoppedCounter2 > 5 || speed < 1)

			speed = 0;


		prevRev2 = currentRevCount;
		prevMeasTime2 = currentMeasTime;


		return speed;
	}

	public void FECData(Byte[] data)
	{

		if (data[0] == 16) //General FE Data Page
		{

			int distanceTraveled = data[3];
			//Debug.Log("distanceTraveled:" + distanceTraveled+"m");
			m_speed=GetSpeed (data);

			//print(m_speed);
		}
	}

	void Animating()
	{
		bool riding = false;
		if (m_speed!=0)
		{
			riding = true;
		}

		bool fall = false;
	
		anim.SetBool ("IsRiding", riding);
	}
	void OnTriggerEnterBis(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Cube")) {
			other.gameObject.SetActive (false);
			countbanana = 3;
		
		}

		if (other.gameObject.CompareTag ("Nail")) {
			anim.SetBool ("IsFalling", true);
			m_Rigidbody.MovePosition (m_Rigidbody.position + new Vector3 (5, 0, 0));
			anim.SetBool ("Sol", true);

		}


	}

	void Banana ()
	{
		
		if (Input.GetKeyDown (KeyCode.B) & countbanana > 0) {
			Rigidbody rocketInstance;
			rocketInstance = Instantiate (rocketPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
			rocketInstance.AddForce (barrelEnd.forward * -2000);
			countbanana -= 1;
		
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			Rigidbody rocketInstance;
			rocketInstance = Instantiate (rocketPrefab2, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
			rocketInstance.AddForce (barrelEnd.forward * -2000);

		}
		if (Input.GetKeyDown (KeyCode.L)) {
			Rigidbody rocketInstance;
			rocketInstance = Instantiate (rocketPrefab3, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
			rocketInstance.AddForce (barrelEnd.forward * -2000);

		}
	}
	void SetcountText()
	{
		

		ChronoText.text = "Temps:" + chrono.ToString ();
		BananeText.text = countbanana.ToString ();

	}
}




