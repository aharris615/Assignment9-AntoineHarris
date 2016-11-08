using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {


	void OnTriggerEnter(Collider other) 
	{
		GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		gc.takeDamage();
	}
}

