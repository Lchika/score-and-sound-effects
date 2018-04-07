using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotReactor : MonoBehaviour {

    public SerialHandler serialHandler;
    public Text bulletNumText;
    public Text scoreText;
    int bulletNum = 20;
    public static int score = 0;

    // Use this for initialization
    void Start()
    {
		score = 0;
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        try
        {
            if (message == "h")
            {
                bulletNum--;
                Debug.Log("bulletNum = " + bulletNum.ToString());
                bulletNumText.text = bulletNum.ToString();
            }
            if (message == "p")
            {
                score++;
                Debug.Log("score = " + score.ToString());
                scoreText.text = score.ToString();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}
