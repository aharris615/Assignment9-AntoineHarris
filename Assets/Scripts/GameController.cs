using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

public class GameController : MonoBehaviour {
	Text hudText;

	public int score;
	public int health;
	public float timeInGame;
	public int currentLevel;




	void Awake() {
		if (PlayerPrefs.HasKey ("score")) {
			RestorePlayerValues ();
		} else {
			StartNewGame ();
		}
	}

	void Start () {
		// We need to call this in start so that when this is called on other levels
		// we have access to the hudText and not 
		hudText = GameObject.Find("HUDText").GetComponent<Text>();
	}
	
	void Update () {
		string hudInfo = "";

		timeInGame += Time.deltaTime;


		hudInfo += "Level: " + (currentLevel+1) + "\n";
		hudInfo += "Score: " + score + "\n";
		hudInfo += "Time: " + timeInGame.ToString ("F2");

		hudText.text = hudInfo;


	}

	public void StartNewGame() {
		if (PlayerPrefs.HasKey ("score")) {
			DeletePlayerValues();
		}
		score = 0;
		timeInGame = 0;

		currentLevel = 0;
		StorePlayerValues ();
	}

	public void LevelUp() {		
		currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
		LoadLevel();
	}

	public void LevelDown() {
		currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
		LoadLevel();
	}

	private void LoadLevel() {
		StorePlayerValues ();
		SceneManager.LoadScene(currentLevel);
	}

	public void CollectCoin() {
		score += 100;
	}

	public void takeDamage() {
		score -= 10;
	}

	void StorePlayerValues() {
		PlayerPrefs.SetInt ("score", score);
		PlayerPrefs.SetInt ("health", health);
		PlayerPrefs.SetFloat ("timeInGame", timeInGame);
		PlayerPrefs.SetInt ("currentLevel", currentLevel);
	}

	void RestorePlayerValues() {
		score = PlayerPrefs.GetInt ("score");
		health = PlayerPrefs.GetInt ("health");
		timeInGame = PlayerPrefs.GetFloat ("timeInGame");
		currentLevel = PlayerPrefs.GetInt ("currentLevel");
	}

	void DeletePlayerValues() {
		PlayerPrefs.DeleteKey ("score");
		PlayerPrefs.DeleteKey ("health");
		PlayerPrefs.DeleteKey ("timeInGame");
		PlayerPrefs.DeleteKey ("currentLevel");
	}
		
}