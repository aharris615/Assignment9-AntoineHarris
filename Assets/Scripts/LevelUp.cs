using UnityEngine;
using System.Collections;

public class LevelUp : MonoBehaviour {
	void OnTriggerEnter() {
		GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		gc.LevelUp();
	}
}
