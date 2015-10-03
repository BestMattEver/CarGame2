using UnityEngine;
using System.Collections;

public class ScreenTextStuff : MonoBehaviour {

	public string Isay =" ";
	TextMesh text;

	// Use this for initialization
	void Start () 
	{
		Isay =" ";
		text = this.GetComponent<TextMesh>();
	}//end start
	
	// Update is called once per frame
	void Update () 
	{
	
	}//end update

	public void ToggleText ()
	{
		if(this.enabled == true)
		{
			this.enabled = false;
		}
		else
		{
			this.enabled = true;
		}
	}

	public void ChangeText(string newtext)
	{
		Isay = newtext;
		text.text = Isay;
	}

}//end class
