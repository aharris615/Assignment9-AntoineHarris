using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour
{
    public Canvas titleScreenUI;
    AsyncOperation async = null;

    void Awake ()
    {
        titleScreenUI.enabled = true;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void startGame ()
    {
        async = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        Destroy(this.gameObject);
    }
}