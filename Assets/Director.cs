using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*this is the main game controlling script. 
This will generate each map
it will then set up missions for the player to take based on the map
it detects when these missions are complete
then awards whatever goods the player should get from the missions.


MAP GENERATION ALGORITHM
-------------------------
0.select the correct tileset based on player progression
1.Place a starting tile at (0,0,0)
2.find all unpaired 'roadend's 
3.put a road tile's 'roadstart' on each 'roadend'
4.repeat 2 and 3 5ish times
5.find any remaining 'roadend's 
6.put a cap tile on each one
7.fill out map with lots of extras (enemies, pickups, npcs, obstacles, etc...)

*/
public class Director : MonoBehaviour {

	public int totalroadcount=0;
	public int NumberOfRoads = 3;
	public TextMesh cameratext;
	public GameObject[] Protoroadtiles; // this is an array of road tiles I've instantiated in the editor, in the end we'd have a bunch of different tile sets for different biomes
	public GameObject[] ProtoStartTiles;
	public GameObject[] ProtoCapTiles;
	public GameObject SlimCapTile;
	public bool[] wevetried = new bool[4];
	int wevetriedeverything =0;

	public bool OMGoverlap; // = false;

	GameObject[] roadends;
	GameObject[] roadstarts;

	// Use this for initialization

	void Start ()
	{
		//1. set down a start tile at 0,0,0 [DONE]
		//2. generate roads off of that start tile [DONE]
		//3. find remaining roadends, and cap them.
		//4. populate all roads with extras

		//roadstarts = (GameObject.FindGameObjectsWithTag("RoadStart"));

		//while(roadstarts.Length < 10)
		//{
			//for(int kl =0; kl <= roadstarts.Length-1; kl++)
			//{
				//Destroy(roadstarts[kl].transform.parent);
			//}


			//initializing this array to flase for later
			for(int x = 0; x <= wevetried.Length-1; x++)
			{
				wevetried[x] = false;			
			}
	

			//this line picks a random start tile and plops it down at 0,0,0
			Instantiate(ProtoStartTiles[Random.Range (0, ProtoStartTiles.Length)], (new Vector3(0,0,0)), Quaternion.Euler(new Vector3(0,0,0)));

			//this for loop builds roads off of the start tile 
			for(int r=0; r <= NumberOfRoads; r++)//this iterates through numberofroads times and builds new road sections on whats already there.
			{
				Debug.Log("BUILDING LEVEL " + r + " ROADS");

				roadends = (GameObject.FindGameObjectsWithTag("RoadEnd"));//add any found roadends to the roadends array.
				for(int i=0; i<=roadends.Length-1; i++)//iterate through each roadend in the array
				{

					this.GenerateRoad(roadends, i);//build ONE properly rotated road
				}
			}

			//roadstarts = (GameObject.FindGameObjectsWithTag("RoadStart"));

		//}//end while
		/*
		roadstarts = (GameObject.FindGameObjectsWithTag("RoadStart"));
		Debug.Log ("length: " + roadstarts.Length);
		for(int c=0; c<= roadstarts.Length-1; c++)
		{
			Debug.Log("ARE WE EVEN IN HERE?");
			//turns back on roadgeo so players can drive on roads
			roadstarts[c].collider.isTrigger = true;
		}//end roadgeo turn back on for
		*/

	}//end start
	
	// Update is called once per frame
	void Update () {
	
	}

	void AddBufferRoadSections()
	{
		//this method will be called after the start tile is laid down, and it'll just add straight roads to every road end as a buffer,
		//so the random roads have a bit more space to snake around
	}

	void addroadcap()
	{
		//this method will be called after all roads have been generated and it will take the remaining road ends
		//and randomly add cap sections to them, checking for overlaps.
	}


	void GenerateRoad(GameObject[] roadends, int i)
	{	

		Transform tempcollider;
		RaycastHit collisioninfo;
		int whichtile;
		int rand;
		Vector3 spawnpos;
		GameObject temproad;
		int temprot =0;
		List<GameObject> REsInTemproad = new List<GameObject>();

			
				if(roadends[i].GetComponent<RoadEndStuff>().paired == false)//if this one isnt paired yet...
				{
					
					spawnpos = roadends[i].transform.position;//gets the location of the roadend in question
					rand = Random.Range (0, 100);//gets a random number between 0 and 100 --- (Protoroadtiles.Length)

					//these if statments use that random number to decide which type of tile to lay (essentially allowing me to weight selection of certain road sections)
					if(rand <= 33) 
					{whichtile = 0;}
					else if((rand > 33) && (rand <= 62)) 
					{whichtile = 1;}
					else if((rand > 62) && (rand <= 90))
					{whichtile = 2;}
					else if(rand > 90)
				    {whichtile = 3;}
					else
					{whichtile =0;}
				
					//this line instantiates a road section at position 0,0,0 corrosponding to the random number [NOTE: IT IS NOT ROTATED PROPERLY YET]
					temproad = (GameObject)Instantiate(Protoroadtiles[whichtile], new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));

					//sets the number on each section of road so I can keep track of em.
					temproad.GetComponentInChildren<RoadStartstuff>().iamroad = totalroadcount;
			
			//this line of code moves the road section to where it ought to be, hopefuly activating the overlap triggers
					temproad.transform.position = temproad.transform.position + spawnpos + new Vector3 (0,-20,0);
					//temproad.transform.position = temproad.transform.position + Vector3.zero;
					
					//this line sets the beforeme variable in the new roadEnd to the previous roadEnd, so we can iterate through a chain later.
					temproad.GetComponentInChildren<RoadStartstuff>().beforeme = roadends[i];
					
					//this line gets all the roadends in the temproad (most of the time it'll just be one, but in the case of y intersections and 4 ways it might be more
					foreach(Transform child in temproad.transform)
					{//i will be honest with you... i dont know how a foreach works... i was just told to use one...	
						if(child.gameObject.tag == "RoadEnd")
						{
							REsInTemproad.Add(child.gameObject);
						}
					}//end foreach

					//Debug.Log (temproad.name + "has " + REsInTemproad.Count + " RoadEnds");
					
					for(int k = 0; k<= REsInTemproad.Count-1; k++)
					{//this for loop iterates through each of the roadends in temproad and sets their degreessofar indivigually (this is importaint for getting the correct rotation on the NEXT road section.
					
						//this line adds the previous roadEnd's (beforeme's) final rotation and This roadEnds rotation to get how this road ought to be rotated
						temprot =  (temproad.GetComponentInChildren<RoadStartstuff>().beforeme.GetComponent<RoadEndStuff>().degreessofar) + (REsInTemproad[k].GetComponent<RoadEndStuff>().degreeturn);
					
						//(temproad.GetComponentInChildren<RoadStartstuff>().degreeturn)  
					
						//sets this road section's rotation variable (degreessofar) to the rotation obtained in the previous line
						REsInTemproad[k].GetComponent<RoadEndStuff>().degreessofar = temprot;
					}//end for
					
					
					//this line does the rotation again, using the roadStartStuff. for rotation of this roadsection only, not setting future rotations on roadends like we did inside the for a few lines up.
					temprot = (temproad.GetComponentInChildren<RoadStartstuff>().beforeme.GetComponent<RoadEndStuff>().degreessofar) + (temproad.GetComponentInChildren<RoadStartstuff>().degreeturn);
					
					//this sets the entire road section's actual physical rotation. (as opposed to the rotation for the NEXT set of roads which is set in the roadends in the for loop above.)
					temproad.transform.rotation = Quaternion.Euler(new Vector3 (0,temprot,0));
					
					//turn off the road geo, so overlap check wont hit itself.
					tempcollider = temproad.transform.Find ("RoadGeo");
					tempcollider.gameObject.SetActive(false);

					//gets the roadstart object of this roadsection for use later
					GameObject temproadstart = temproad.GetComponentInChildren<RoadStartstuff>().gameObject;

					//check for overlaps using the roadstart colliders
					Debug.Log ("overlap: " + (temproad.GetComponentInChildren<RoadStartstuff>().gameObject.rigidbody.SweepTest(transform.up, out collisioninfo, 20.0f)) +" on " + totalroadcount);// if yes, send a debug message
							
					//this line checks for an overlap and takes appropriate action
					if(temproad.GetComponentInChildren<RoadStartstuff>().gameObject.rigidbody.SweepTest(transform.up, out collisioninfo, 20.0f))		
			   		{
						//these if statments mark which type of roadsectoin had the overlap
						if(whichtile == 0)
						{
							wevetried[0] = true;
						}
						else if(whichtile == 1)
						{
							wevetried[1] = true;
						}
						else if(whichtile == 2)
						{
							wevetried[2] = true;
						}
						else if(whichtile == 3)
						{
							wevetried[3] = true;
						}
								
						//this for loop checks to see if all the roadsection types have been tried. if yes, then we'll put a cap section on the road instead.
						for(int a=0; a <= wevetried.Length-1; a++)
						{
							if(wevetried[a])
							{
								wevetriedeverything++;
							}
						}
								
						
				
						//if wevetriedeverything is = to the number of possible roadsections, then we've tried them all and all we can do is cap the road
						if(wevetriedeverything >= 4)
						{	
							//
							Debug.Log("-----------------OK. WE'RE GOING TO CAP ROAD SECTION: " + (totalroadcount-1) + "! CAP CAP CAP!---------------");
					//temproad.GetComponentInChildren<RoadStartstuff>().beforeme.SetActive(false);//this line turns off the prior road's road end so it wont try to be used in the next round of road generation. when we cap it, we wont need this line.		
					//temproad.GetComponentInChildren<RoadStartstuff>().beforeme.gameObject.SetActive(false);//this line turns off the prior road's road end so it wont try to be used in the next round of road generation. when we cap it, we wont need this line.
							temproad.SetActive(false);
							//now add a cap to the previous road section (ie: beforeme)

							//Destroy (temproad); //this destroys the current road because its overlapping and no good.

						}//end if
					    else //there might still be other roads to try...
					    {
							Debug.Log ("----------------------call generate road again because: overlaps---------------------------");
							
							
							temproad.SetActive(false);
							//Destroy (temproad); //this destroys the current road because its overlapping and no good.
							

							this.GenerateRoad(roadends, i); //this runs

							//set all the variables required for overlap checks back to false and 0
							wevetriedeverything =0;
							for(int z = 0; z <= wevetried.Length-1; z++)
							{
								wevetried[z] = false;			
							}
					
							//
									
							
					    }//end else
								
					}//end the overlap check


					//after the sweep test up, put the tile where it actually belongs.
					temproad.transform.position = spawnpos;
						
				
					//turns back on roadgeo so players can drive on roads
					tempcollider = temproad.transform.Find ("RoadGeo");
					tempcollider.gameObject.SetActive(true);
					

					//turns the roadstart colliders into triggers so the player doesnt run into them
					foreach(Collider c in temproadstart.GetComponents<BoxCollider>())
					{//i will be honest with you... i dont know how a foreach works... i was just told to use one...	
						c.isTrigger = true;
					}//end foreach				
					

					//tempcollider = temproad.transform.Find ("RoadStart");
					//tempcollider.collider.isTrigger = true;
			

					//tell the roadend we're working on that it is paired with a road... finally.
					roadends[i].GetComponent<RoadEndStuff>().paired = true;
					
					REsInTemproad.Clear();
					totalroadcount++;
					
				}//end if
				
				

	}//end generateroads


}//end class









