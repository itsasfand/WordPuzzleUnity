using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class myLoadingScreen : MonoBehaviour {

	void Update () {

		if (Application.GetStreamProgressForLevel (2) == 1 && Time.timeSinceLevelLoad>6.0f) {
			Application.LoadLevel (2);
		}
	
	}
}
