using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartDirector : MonoBehaviour {

    public SerialHandler serialHandler;

    // Use this for initialization
    void Start () {
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
    }

    // Update is called once per frame
    void Update () {
		
	}

    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        try
        {
            if (message == "s") {
                SceneManager.LoadScene("PlayingGameScene");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
