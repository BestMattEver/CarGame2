using UnityEngine;
using System.Collections;

public class RoadStartstuff : MonoBehaviour
{
	public GameObject Director;// = GameObject.Find ("Director");
	public GameObject parent; //= this.transform.parent.gameObject;
	public int degreeturn; 
	public int degreessofar =0;
	public GameObject beforeme;
	public int iamroad = 0;
	// Use this for initialization

	
	void Start () 
	{
		//Director = GameObject.Find ("Director");
		//parent = this.transform.parent.gameObject;   //this gets the parent of this road start so we can filter out self collisions later...

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	/*void OnTriggerEnter(Collider other)
	{
		Director = GameObject.Find ("Director");
		parent = this.transform.parent.gameObject;

		if((other.gameObject.transform.parent.gameObject == parent))
		{
			//do nothing, you're colliding with yourself idiot.
			Debug.Log ("self collision. ignore.");
		}//end if
		else
		{
			Debug.Log(Director.GetComponent<Director>().OMGoverlap);
			Director.GetComponent<Director>().OMGoverlap = true;
			Debug.Log ("OMG WE HAVE AN OVERLAP BRUH! " + other.gameObject.transform.parent.gameObject.name);
			//here we also want to remember which type of road section was used when we collided so we can check later if all road sections have been tried.
			//we'll use a new array in the director that matches the protoroadsection array to keep track
			//once all possible road sections are tried, just cap it with a really slim special cap secion.
		}//end else
	}*/

}

