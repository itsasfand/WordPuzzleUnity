using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Select_Level : MonoBehaviour {
	//public  Button button;
	public void ChooseLevel()
	{
		Level_Info.oDname = gameObject.GetComponentInChildren<Text> ().text;
		Debug.Log (Level_Info.oDname);
		//Application.LoadLevelAsync ("Resources/Scenes/Load");
		Application.LoadLevel (2);
	}

}
