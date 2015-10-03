using UnityEngine;
using System.Collections;

public class Triggerhit : MonoBehaviour 
{
	public GameObject TextObjectToChange;
	public string newtext;


	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		TextObjectToChange.GetComponent<ScreenTextStuff>().ChangeText(newtext);
		TextObjectToChange.GetComponent<ScreenTextStuff>().ToggleText();
	
	}//end trigger enter
}
