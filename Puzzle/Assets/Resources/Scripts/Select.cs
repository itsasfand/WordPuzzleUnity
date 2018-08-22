using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class Select : MonoBehaviour {
	public static string word ;
	public AudioSource clk;
	public Text text;
	public bool isDown;
	public static int flag=0;
	public static int Times=0;
	public static int dif= 9999999;
	public  static int start_point;
	public static int next_point;
	public GameObject obj;
    RectTransform rect;
	public Vector2 oSize;
	Color oBColor;
	Color oTColor;
	List<int> Movement = new List<int>{-1,Level_Info.GridX,-Level_Info.GridX,1,Level_Info.GridX+1,-1-Level_Info.GridX,1-Level_Info.GridX,-1+Level_Info.GridX};
	void Start () {
		//clk.clip = lvlInfo.tap_sound;
		oTColor = text.color;
		//oBColor = gameObject.GetComponentInParent<Image> ().color;
		oSize = new Vector2 (transform.parent.GetComponent<RectTransform> ().rect.width, transform.parent.GetComponent<RectTransform> ().rect.height);
		isDown = false;
		text = gameObject.GetComponent<Text> ();
		word = "";

		rect = obj.GetComponent<RectTransform> ();

	}

	// Update is called once per frame
	public static string Reverse (string s) //Reverse the Given String
	{
		char[] charArray = s.ToCharArray ();
		Array.Reverse (charArray);
		return new string (charArray);
	}
	void Update () {
		
		if (Input.GetMouseButtonUp (0)) {
			flag = 0;
			dif = 9999999;
			Times = 0;
			start_point = 12;
			Debug.Log ("WORD THROUGH BUTTON : " + word);


			ResizeButtons ();


			if (GM.ToFind.Contains(word) || GM.ToFind.Contains(Reverse (word))) {
				if (GM.ToFind.Contains (Reverse (word))) {
					GM.ToFind.Remove (Reverse (word));
				} else {
					GM.ToFind.Remove (word);
				}
				GameObject [] texts = GameObject.FindGameObjectsWithTag ("Selected");
				foreach (var item in texts) {
					Text	text = item.GetComponent<Text> ();
					item.tag = "Completed";
				}
				word = "";

			} else {
				GameObject [] texts = GameObject.FindGameObjectsWithTag ("Selected");
				foreach (var item in texts) {
				  Text	text = item.GetComponent<Text> ();
					if (item.tag != ("Completed")) {
						text.color = oTColor;
						//gameObject.GetComponentInParent<Image> ().color = oBColor;
						item.tag = "notSelected";
					}
				}
				word = "";
			}
			isDown = false;
		} else if (Input.GetMouseButtonDown (0) || Input.GetMouseButton (0)) {
			isDown = true;
			Debug.Log ("IS DOWN");
		}


	}

	public void OnMouseEnt()
	{

		bool err;
		err = false;
//		if (Mathf.Abs(next_point.y - start_point.y ) == gameObject.GetComponent<RectTransform>().rect.height || flag==0) {
//			if (flag >0) {
//				start_point = next_point;
//			}
//			flag++;



		if (int.Parse (transform.parent.GetChild (0).name) - start_point == dif || flag ==0 && Input.GetMouseButton (0) && Times!= 0) {


			if (flag == 0) {
				Debug.Log (int.Parse (transform.parent.GetChild (0).name) - start_point);
				if (Movement.Contains (int.Parse (transform.parent.GetChild (0).name) - start_point)) {
					next_point = int.Parse (transform.parent.GetChild (0).name);
					Debug.Log ("HERE");
				} else {

					err = true;
				}

			} else {
				next_point =  int.Parse (transform.parent.GetChild (0).name);
			}

		}
		//		
		if (Input.GetMouseButton (0 ) && !err) {

	
			if  (next_point - start_point == dif || flag==0) {
				if (clk.isActiveAndEnabled) {
					clk.Play ();
				}
				ChangeButton ();
				flag++;
				if (flag ==1 && next_point - start_point == 1) {
					dif = 1;
					flag++;

				}
				else if (flag == 1 && next_point - start_point == -1)
				{
					dif = -1;
					flag++;
				}
				else if (flag == 1 && next_point - start_point == Level_Info.GridX)
				{
					dif = Level_Info.GridX;
					flag++;
				}
				else if (flag == 1 && next_point - start_point == -Level_Info.GridX)
				{
					dif = -Level_Info.GridX;
					flag++;
				}
				else if (flag == 1 && next_point - start_point == -Level_Info.GridX-1)
				{
					dif = -Level_Info.GridX-1;
					flag++;
				}
				else if (flag == 1 && next_point - start_point == Level_Info.GridX+1)
				{
					dif = Level_Info.GridX+1;
					flag++;
				}
				else if (flag == 1 && next_point - start_point == Level_Info.GridX-1)
				{
					dif = Level_Info.GridX-1;
					flag++;
				}
				else if (flag == 1 && next_point - start_point == -Level_Info.GridX+1)
				{
					dif = -Level_Info.GridX+1;
					flag++;
				}
				start_point = next_point;






				if (isDown && gameObject.tag != "Selected") {
					if (gameObject.tag != "Completed") {

						gameObject.tag = "Selected";
					}
//					text.color = Random.ColorHSV (0f, 1f, 1f, 1f, 0.5f, 1f);
//					gameObject.GetComponentInParent<Image> ().color = Color.yellow;
					text.color = Color.yellow;
					word += text.text;


//		}
				}
			}

		}
		Times++;
		err = false;
	}
	public void click()
	{ 




//		if (Mathf.Abs(next_point.y - start_point.y ) == gameObject.GetComponent<RectTransform>().rect.height || flag==0) {
//			if (flag > 0) {
//				start_point = next_point;
//			}
//			flag++;
		if(Input.touchCount<=1)
		{
	
			if (clk.isActiveAndEnabled) {
				clk.Play ();
			}

		if (Input.GetMouseButton (0)) {
			

			ChangeButton ();
			start_point = int.Parse (transform.parent.GetChild (0).name);
			Debug.Log ("NAMMMMMMMMMMMMMMMMMMME"+ start_point.ToString());
				if (gameObject.tag != "Selected") {
					if (gameObject.tag != "Completed") {

						gameObject.tag = "Selected";
					}
					//text.color = Random.ColorHSV (0f, 1f, 1f, 1f, 0.5f, 1f);
//					
					//gameObject.GetComponentInParent<Image> ().color = Color.yellow;
					text.color = Color.yellow;
					word += text.text;

		
			
				}
			}

		}
	}
	void ResizeButtons()
	{
		List <GameObject[]> CHS = new List<GameObject[]> 
		{
			GameObject.FindGameObjectsWithTag ("Completed"),
			GameObject.FindGameObjectsWithTag ("Selected"),
			GameObject.FindGameObjectsWithTag ("notSelected")
		};
		foreach (var arr in CHS) {
			foreach (var item in arr) {
				item.transform.parent.GetComponent<RectTransform> ().sizeDelta= new Vector2(oSize.x,oSize.y);
			}
		}
	}
	void ChangeButton ()
	{
		transform.parent.GetComponent<RectTransform> ().sizeDelta= new Vector2(oSize.x+12,oSize.y+12);
	}
}
