using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GM : MonoBehaviour
{
	public GameObject WinMenu;
	public GameObject pauseMenu;
	public GameObject ListOfWords;
	public AudioSource Background;
	public Text WordsToFind;
	public TextAsset TextFile;
	public GameObject canvas;
	public Camera camera;
	public GameObject obj;
	public RectTransform rectt;
	public static int GridX = Level_Info.GridX;
	public static int GridY = Level_Info.GridY;
	private Text[,] text = new Text[Level_Info.GridX, Level_Info.GridY];
	private GameObject[,] chars = new GameObject[Level_Info.GridX, Level_Info.GridY];
	public Vector3 Opos;
	public float posY;
	public float posX;
	private string alphabets;
	public GameObject[] array;
	public List<string> Words = new List<string> ();
	public static List<string> ToFind;
	public GameObject[,] Path = new GameObject[Level_Info.GridX, Level_Info.GridY];
	void Awake ()
	{
		Time.timeScale = 1;
		//
		TextFile = Level_Info.ODict;
		canvas.GetComponentInChildren<Text> ().text =TextFile.name;

		//Background.clip = Level_Info.background_music;	

//		canvas.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width*2f,Screen.height*2f);


		Debug.Log ("Screen adgust " + canvas.GetComponent<RectTransform> ().rect.width + "Scree Widt " + Screen.width);

		CreateWordsList ();


		ToFind = new List<string> (Words);

		Debug.Log ("to Find COunt" + GM.ToFind.Count.ToString ());


		posX = obj.GetComponent<RectTransform> ().rect.width + obj.GetComponent<RectTransform> ().rect.width / 2.5f;
		posY = obj.GetComponent<RectTransform> ().rect.height + obj.GetComponent<RectTransform> ().rect.height / 2.5f;

		Opos = new Vector3 (rectt.localPosition.x, rectt.localPosition.y, 0);
		alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		CreateGrid ();
		List<int> pos = new List<int> (){1,2,3};
		//Debug.Log (CheckForEmptyCells(1,3,3,"asfa"));
		//

		CheckEachAndPut ();
		//AdjustWord(0,3,3,"rat",pos);



	}

	public IEnumerator wait(int s)
	{
		ListOfWords.SetActive(true);

	   yield return new WaitForSeconds (s);
		Debug.Log ("%SECONDS");
		ListOfWords.SetActive(false);
		//canvas.SetActive (true);
	}
	void ShowWordsToFind ()
	{
		WordsToFind.text = '\n'.ToString ();;

		foreach (var word in GM.ToFind) {
			WordsToFind.text += word + '\n';
		}
//		if (GM.ToFind.Count == 0) {
//			WordsToFind.text = " You Win ";
//			WordsToFind.color = UnityEngine.Random.ColorHSV (0f, 1f, 1f, 1f, 0.5f, 1f);
//			WordsToFind.fontSize = 40;
//		}

	}

	void CreateWordsList ()
	{ 
		string[] linesInFile = TextFile.text.Split ('\n');
		int run_times = 0;
//		foreach (string line in linesInFile)
		while (Words.Count < Level_Info.maximum_words ) { 
			run_times++;
			string line = linesInFile [UnityEngine.Random.Range (0, linesInFile.Length - 1)];
			string owrd = "";
			int inc = 0;
			if (line.Length <= GridX)
			{
				while (inc < line.Length - 1) {
					owrd += line [inc];
					inc++;
				}

				Debug.Log ("Word:" + owrd + "Length  :" + owrd.Length.ToString ());
				if (!Words.Contains (owrd)) {
					Words.Add (owrd);
				
				}
			}
			if (run_times >1000) {
				break;
			}
		}

	}

	string GetChar (string Char)
	{
		
		int inc = 0;
		foreach (var item in array) {
			if (item.name == Char.ToString ()) {
				
				return inc.ToString ();

			}
			inc++;
		}
		return "1";

	}

	void Update ()
	{
		if (GM.ToFind.Count == 0)
		{
			win ();
		
		}

		if (Background.isPlaying == false && Background.isActiveAndEnabled) {
			Background.Play ();
		}
		ShowWordsToFind ();
		for (int i = 0; i < GridX; i++) {
			for (int j = 0; j < GridY; j++) {

				if (text [i, j].text == "!") {	
					int index = UnityEngine.Random.Range (0, 26);
					text [i, j].text = alphabets [index].ToString ();

				}
				
			}
		}

	}


	public void ShowListofWords()
	{


		StartCoroutine (wait (2));

	}

	public void PauseMenu()
	{
		Time.timeScale = 0;
		pauseMenu.SetActive (true);

		
	}

	public void win()
	{
		Time.timeScale = 0;
		Background.mute = true;
		WinMenu.SetActive (true);


	}

	void CreateGrid ()
	{
		int inc = 1;
		Vector3 pos = Opos;
		for (int i = 0; i < GridX; i++) {
			if (i != 0) {
				pos.y -= posY;
				pos.x = Opos.x;
			}
			for (int j = 0; j < GridY; j++) {
				if (j != 0) {
					pos.x += posX;
				}

				chars [i, j] = Instantiate (obj, pos, Quaternion.identity) as GameObject;
				chars [i, j].transform.SetParent (canvas.transform, false);
				Path [i, j] = chars [i, j].transform.Find ("1").gameObject;
				Path [i, j].name = inc++.ToString ();

				text [i, j] = chars [i, j].GetComponentInChildren<Text> ();
				text [i, j].text = "!";
				//text[i,j].text =alphabets[ UnityEngine.Random.Range(0,26)].ToString();
			}
		}
	}

	int   CheckForEmptyCells (int i, int j, int rorc, string word)
	{
		int empty_cells = 0;

		if (rorc == 0) {  // o Mean row and 1 mean coloum//ROW  MEAN I SAME AND J CHANGES 
			//int i = Random.Range(0,GridX);
			int max = word.Length - 1 + j;
			while (j <= max) {
				if (j >= GridY) {
					Debug.Log ("And LINE IS   :   " + GetLine (i, j, rorc) + "  Word : " + word);
					return empty_cells;
				}
				if (text [i, j].text == "!") {
					//Debug.Log ("ROws EMpty : " + empty_cells.ToString ());
					empty_cells++;

				} else {
					empty_cells = 0;
				}
				j++;
				
	
			} 
		} else if (rorc  ==1){
			//column  MEAN j SAME AND i CHANGES 
			int max = word.Length - 1 + i;
			while (i <= GridY) {

				if (i >= max) {
					Debug.Log ("And LINE IS   :   " + GetLine (i, j, rorc) + "  Word : " + word);
				
					return empty_cells;
				}
				if (text [i, j].text == "!") {
					//Debug.Log ("colom EMpty : " + empty_cells.ToString () + "I+WORDLEN  :"+ (max).ToString());
					empty_cells++;
				} else {
					empty_cells = 0;
				}

				i++;

			}



		}

		else if(rorc == 2)  // top left to bottom right
		{
			
			if (j >= i) {
				int max = word.Length - 1 + j;
				while (j < GridY) {

					if (j >= max) {
						return empty_cells;
					}
				
					if (text [i, j].text == "!") {
						//Debug.Log ("colom EMpty : " + empty_cells.ToString () + "I+WORDLEN  :"+ (max).ToString());
						empty_cells++;
					} else {
						empty_cells = 0;
					}

					i++;
					j++;
				
				}
			} else {
				int max = word.Length-1+i;
				while (i < GridX) {
					if(i>=max)
					{
						return empty_cells;
					}
					if (text [i, j].text == "!") {
						//Debug.Log ("colom EMpty : " + empty_cells.ToString () + "I+WORDLEN  :"+ (max).ToString());
						empty_cells++;
					} else {
						empty_cells = 0;
					}

					i++;
					j++;

				}
			}




		}
		else if(rorc == 3)  // top left to bottom right
		{
			int max = word.Length - 1 + i;
			while (true)
			{
				if (i > max) 
				{
					Debug.Log ("ITs HERE" +i);
					return empty_cells;
				}
				if (j < 0 || i > GridX - 1)
				{
					return empty_cells;
				}

				if (text [i, j].text == "!")
				{	
					Debug.Log (i);
					empty_cells++;
				}
				else 
				{
					empty_cells = 0;
				}

				i++;
				j--;


			}







	}

		Debug.Log ("And LINE IS   :   " + GetLine (i, j, rorc));
		Debug.Log ("Empty Cells" + empty_cells.ToString ());
		return empty_cells;
	}


	void CheckEachAndPut () //check each word and put it in the Grid
	{
		List<string> Added_Words = new List<string> ();
		int Tries = 0;

		while (Tries < 2000) { //Try For N Times To Put each WOrd If Possible
			
//			try {
			Debug.Log ("NUMBER OF TRIES    : " + Tries.ToString ());
			foreach (var item in Words) {
				int i = 0;
				int j = 0;
				int rorc =UnityEngine.Random.Range (0, 4);

				//0 Mean row and 1 mean coloum
				if (rorc == 0) {
					while (true) { //Mean ROw Selected TO put Word
						i = UnityEngine.Random.Range (0, GridX);
						j = UnityEngine.Random.Range (0, GridY);

						if (GridY - j >= item.Length) { //Random J means start putting Word in Random Cell
							break;

						} else {
							continue;
						}


					}

				} else if (rorc == 1) { //Mean Coloumn Selected TO put Word
					while (true) {
							
						i = UnityEngine.Random.Range (0, GridX);
						j = UnityEngine.Random.Range (0, GridY);
						if (GridX - i >= item.Length) { //Random I means start putting Word in Random Cell
							break;

						} else {
							continue;
						}
					}
				} else if (rorc == 2) { //Mean Coloumn Selected TO put Word
					while (true) {

						i = UnityEngine.Random.Range (0, GridX);
						j = UnityEngine.Random.Range (0, GridY);
						if (j >= i) {
							if (GridY - j >= item.Length) {
								break;
							} else {
								continue;
							}
						} else {
							if (GridX - i >= item.Length) {
								break;
							} else {
								continue;
							}
						}
					}
				}
				else if (rorc == 3) { //Mean Coloumn Selected TO put Word
					while (true) {

						i = UnityEngine.Random.Range (0, GridX);
						j = UnityEngine.Random.Range (0, GridY);
						try{
						if (j > i) {
							if (GridY - j >= item.Length) {
								break;
							} else {
								continue;
							}
						} else {
							if (GridX - i >= item.Length) {
								break;
							} else {
								continue;
							}
						}
						}
						catch( Exception IndexOutOfRangeException) {
							 continue;
						}
					}
				}

				int rev = UnityEngine.Random.Range (0, 2);
				string word = ""; //Reverse Word Or Not
				if (rev == 0) {
					word = item;
				} else {
					word = Reverse (item);
				}
				if (PutWord (word, i, j, rorc)) {
					Added_Words.Add (item);
				} else {
					Tries++;
					continue;
				}

			}
			foreach (var item in Added_Words) {
				Words.Remove (item);
					
			}
			if (Words.Count == 0) {
				Debug.Log ("BREAK");
				break;
			}
				

			Tries++;

		}
		ToFind = new List<string> (Added_Words);


	}

	bool PutWord (string word, int i, int j, int rorc) // Check And Put the Given Word
	{

		int a = CheckForEmptyCells (i, j, rorc, word);
		Debug.Log ("EMPTY cRELLS :  " + a.ToString ());
		if (CheckForEmptyCells (i, j, rorc, word) >= word.Length) {
			if (rorc == 0) { 
//				while (true) {
				foreach (var item in word) {

					text [i, j].text = item.ToString ();
					j++;
				}
				

//				} 
				j = 0;
				Debug.Log ("JIIIIIIIII");
				return true;
			} else if (rorc==1){
//				while (i < GridX) {
				foreach (var item in word) {

					text [i, j].text = item.ToString ();
					i++;
				}

//				} 
				i = 0;
				Debug.Log ("JIIIIIIIII");
				return true;

			}
			else if (rorc==2){
				//				while (i < GridX) {
				foreach (var item in word) {

					text [i, j].text = item.ToString ();
					i++;
					j++;
				}

				//				} 
				i = 0;
				j = 0;
				Debug.Log ("JIIIIIIIII");
				return true;

			}
			else if (rorc==3){
				//				while (i < GridX) {
				foreach (var item in word) {

					text [i, j].text = item.ToString ();
					i++;
					j--;
				}

				//				} 
				i = 0;
				j = 0;
				Debug.Log ("JIIIIIIIII");
				return true;

			}


		} else {  //if row  or column not empty
			string line = GetLine (i, j, rorc);
//			List <int> positions_list = new List<int> ();
			List<int> pos_put = new List<int> ();
			bool x = CheckIfCanAdjust (word, line, out pos_put);
			if (x) {
				if (pos_put.Count>=word.Length){
				AdjustWord (i, j, rorc, word, pos_put);
				return true;
				}
			}


		}
		return false;
	}

	void AdjustWord (int i, int j, int rorc, string word, List <int> positions)
	{
		
		if (rorc == 0) { ///mean row selected
			int inc = 0;
			foreach (int pos in positions) {

				text [i, pos].text = word [inc].ToString ();
				inc++;
			}
		} else if (rorc ==1) {
			int inc = 0;
			foreach (int pos in positions) {

				text [pos, j].text = word [inc].ToString ();
				inc++;
			}
			
		}
		else if (rorc ==2) {
			int inc = 0;
			foreach (int pos in positions) {

				text [pos+i, pos+j].text = word [inc].ToString ();
				inc++;
			}

		}
		else if (rorc ==3) {
			int inc = 0;
			foreach (int pos in positions) {

				text [pos+i, -pos+j].text = word [inc].ToString ();
				inc++;
			}

		}
		
	}

	string  Convert (List<int>POS)
	{
		string word = "";
		foreach (var item in POS) {
			word += item.ToString ();
		}
		return word;
	}

	bool CheckIfCanAdjust (string word, string  line, out List<int> pos_to_put)
	{  //Check if Word Can Adjust in the Given row or not and give the positions
		int inc = 0;
		int flag = 0;
		int time = 0;
		pos_to_put = new List<int> ();
		while (time < 10) {
			
			if (inc < GridX) {
				foreach (var item in word) {
					if (inc >= GridX) {
						return false;
					}
					if (inc < line.Length) {
						if (item.ToString () == line [inc].ToString () || line [inc].ToString () == "!") {
							flag++;
							pos_to_put.Add (inc);
						} else {
							flag = 0;
							pos_to_put.Clear ();
						}

						inc++;
					}
				}
			}

			if (flag == word.Length) {
				Debug.Log ("POSITONS AND FLAG : " + Convert (pos_to_put) + "amd " + flag.ToString ());
				Debug.Log ("Word : " + word + "In LIne  :  " + line);
				return true;
			} else { 
				pos_to_put.Clear ();
				time++;
				inc = time;
				continue;
			}	

		}
		return false;


		
	}

	string GetLine (int i, int j, int rorc) //Return The given row or coloumn elements in String Form
	{

		string complete_line = "";

		//o Mean row and 1 mean coloum
		if (rorc == 0) {
			
			for (int c = 0; c < GridY; c++) {

				complete_line += text [i, c].text;
			}
		} else if (rorc == 1) {
			
			for (int r = 0; r < GridY; r++) {

				complete_line += text [r, j].text;
			}
		} else if (rorc == 2) {  // top left to bottom right
			if (j >= i) {
				while (j < GridY) {

					complete_line += text [i, j].text;
					i++;
					j++;
				}
			} else {
				while (i < GridX) {

					complete_line += text [i, j].text;
					i++;
					j++;
				}
			}
		
		} else if (rorc == 3) {  // top left to bottom right
			while (true) {
				if (j < 0 || i > GridX - 1) {
					break;
				} else {
					complete_line += text [i, j].text;
					i++;
					j--;
				}
			}
		}

		return complete_line;
	}

	void isHit () //Check for Hit
	{
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0)) {
			Debug.Log ("HIT");

			Destroy (hit.transform.gameObject);
		}

	}

	public static string Reverse (string s) //Reverse the Given String
	{
		char[] charArray = s.ToCharArray ();
		Array.Reverse (charArray);
		return new string (charArray);
	}
}



