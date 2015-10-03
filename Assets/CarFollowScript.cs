using UnityEngine;
using System.Collections;

public class CarFollowScript : MonoBehaviour {

	//public float x;
	//public float y;
	//public float z;

	Vector3 camoffset;

	//Transform cameraoffset;
	public GameObject Playercar;


	// Use this for initialization
	void Start () {

		Playercar = GameObject.Find("PlayerCar");

		//camoffset = new Vector3(x,y,z);


		/*
		cameraoffset = new Transform();
		cameraoffset.position.x = x;
		cameraoffset.position.y = y;
		cameraoffset.position.z = z;
		*/
	}
	
	// Update is called once per frame
	void Update () {


		//this line moves the camera exactly how the car moves
		//this.transform.position = new Vector3((Playercar.transform.position.x + camoffset.x), (Playercar.transform.position.y + camoffset.y),(Playercar.transform.position.z + camoffset.z));
		//this.transform.rotation = new Quaternion(0.0f,(Playercar.transform.rotation.y) ,0.0f, 0.0f);

		/*
		this.transform.position.x = (Playercar.transform.position.x + cameraoffset.position.x);
		this.transform.position.y = (Playercar.transform.position.y + cameraoffset.position.y);
		this.transform.position.z = (Playercar.transform.position.z + cameraoffset.position.z);
		*/
	}
}
