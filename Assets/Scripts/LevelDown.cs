using UnityEngine;
using System.Collections;

public class LevelDown : MonoBehaviour {
	void OnTriggerEnter() {
		GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		gc.LevelDown();
	}
}
