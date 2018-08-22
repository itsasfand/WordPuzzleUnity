using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour {

	public static bool AudioMuted;
	public GameObject pauseMenu;
	public void Resume()
	{
		Time.timeScale = 1;
		pauseMenu.SetActive (false);
	

	}

	public void DisableGameAudio()
	{
		if (!AudioMuted) {
			AudioListener.volume = 0.0f;
			AudioMuted = true;
		} else {
			AudioMuted = false;
			AudioListener.volume = 1.0f;
		}
	}
	public void MainMenu()
	{
		Application.LoadLevel (0);
		
	}
	public void Restart()
	{
		Time.timeScale = 1;
		Application.LoadLevel (SceneManager.GetActiveScene ().buildIndex);
		
	}
}
