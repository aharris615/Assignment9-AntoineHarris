using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	Vector3 autoRotate = new Vector3(0.0f, 0.0f, 10.0f);
	
	void Update () {
		transform.Rotate (autoRotate);
	}

	void OnTriggerEnter(Collider other) {
		Destroy (this.gameObject);
		GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		gc.CollectCoin();
	}
}
