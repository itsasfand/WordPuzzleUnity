using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Level_Info : MonoBehaviour 
{
	public static int GridX =7;
	public static int GridY=7;
	public static int maximum_words = 10;
	public  TextAsset[] Dicts; 
	public  AudioClip background_music ;
	public  AudioClip tap_sound;
	public static TextAsset ODict ;
	public static string oDname;





	public void SelectAnotherLevel()
	{
		Time.timeScale = 1;
		int index=0;
		int inc = 0;
		while (inc <= Dicts.Length - 1) 
		{
			if (Dicts[inc].name==oDname)
			{
				break;
				
			}
			index++;
			inc++;
	    }
		index += 1;
		if (index <= Dicts.Length - 1) {
			oDname = Dicts [index].name;
			SetDict (Dicts [index].name);

		} else {
			oDname = Dicts [0].name;
			SetDict (Dicts [0].name);
		}
		;
		Application.LoadLevel (2);
	}


	public  void SetDict( string name)
	{
		foreach (var dict in Dicts) 
		{
			if (dict.name == name) 
			{
				Debug.Log (name);
				ODict = dict;
			}
		}
	}
	void Awake()
	{
		SetDict (oDname);
	}




}