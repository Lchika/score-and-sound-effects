using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartCountController : MonoBehaviour {

	private Text countText;
	private const int maxCount = 5;

	// Use this for initialization
	void Start () {
		countText = GetComponent<Text>();
		StartCoroutine("countDownStartTime", maxCount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//「コルーチン」で呼び出すメソッド
	private IEnumerator countDownStartTime(int maxCount){
		for(int i = maxCount; i > 0; i--){
			countText.text = i.ToString ();
			yield return new WaitForSeconds(1);  //1秒待つ
		}
		SceneManager.LoadScene("PlayingGameScene");
	}
}
