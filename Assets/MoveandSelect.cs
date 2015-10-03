using UnityEngine;
using System.Collections;

public class MoveandSelect : MonoBehaviour {

	public GameObject selector;
	float loc;

	int framecount =0;

	int selection=1;
	//1 = new game
	//2 = continue
	//3 = high scores
	//4 = credits


	// Use this for initialization
	void Start () 
	{

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		framecount++;
		if(framecount > 6)
		{
			framecount=0;

			if(Input.GetAxis("Vertical") < 0)//is the player pressing down?
			{
				if(selection >=4)
				{}//do nothing
				else
				{
					selection++;
					loc = selector.transform.position.z-2;
					selector.transform.position = new Vector3(transform.position.x, transform.position.y,loc);
				}
			}

			if(Input.GetAxis("Vertical") > 0)//is the player pressing up?
			{
				if(selection <=1)
				{}//do nothing
				else
				{
					selection--;
					loc = selector.transform.position.z+2;
					selector.transform.position = new Vector3(transform.position.x, transform.position.y,loc);
				}
			}
		}//end frame limiter
		if(Input.GetAxis("Fire1") > 0)//did the player hit enter to select something?
		{
			select ();
		}
	}

	public void select()
	{//this is where we launch a new scene based on the number of the selection
		if(selection ==1)
		{
			Debug.Log ("you selected NEW GAME");
			Debug.Log ("selection: " + selection);
			Application.LoadLevel("City");
		}

		else if(selection ==2)
		{
			Debug.Log ("you selected CONTINUE");
			Debug.Log ("selection: " + selection);
		}
		else if(selection ==3)
		{
			Debug.Log ("you selected HIGH SCORE");
			Debug.Log ("selection: " + selection);
		}
		else if(selection ==4)
		{
			Debug.Log ("you selected CREDITS");
			Debug.Log ("selection: " + selection);
		}
		else
		{
			Debug.Log ("wtf was that...?");
			Debug.Log ("selection: " + selection);
		}
	}

}
