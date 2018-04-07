using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndDirector : MonoBehaviour {

    public Text bulletNumText;
    public Text playTimeText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (float.Parse(bulletNumText.text) <= 0.0 || playTimeText.text == "0.00") {
            SceneManager.LoadScene("RankingScene");
        }
	}
}
