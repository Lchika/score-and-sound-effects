using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingListManager : MonoBehaviour {

	private struct scoreInfo
	{
		public string userName;
		public int score;
	}

	public static RankingListManager singleton;
	private const int NumberOfScoreInfo = 100;
	private static scoreInfo[] scoreInfos = new scoreInfo[NumberOfScoreInfo];

	void Awake () {
		//　スクリプトが設定されていなければゲームオブジェクトを残しつつスクリプトを設定
		if (singleton == null) {
			DontDestroyOnLoad (gameObject);
			singleton = this;
			//　既にGameStartスクリプトがあればこのシーンの同じゲームオブジェクトを削除
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		scoreInfos [0].userName = "test";
		scoreInfos [0].score = 500;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int registerRankingList(int score, string name){
		bool changeFlag = false;		// スコア上書き管理用
		int	rank = -1;					// 順位情報（返り値）

		// 全ランキング情報を順に見ていく
		for (int i = 0; i < NumberOfScoreInfo; i++) {
			// 引数のスコアより小さいスコアのところまできたらスコア情報を上書きし、changeFlagをたてる
			if (score >= scoreInfos [i].score) {
				int tmpScore = scoreInfos [i].score;
				string tmpName = scoreInfos [i].userName;
				scoreInfos [i].score = score;
				scoreInfos [i].userName = name;
				score = tmpScore;
				name = tmpName;
				// 初めてスコア情報を上書きした時はその時の順位を順位情報として記憶しておく
				if (!changeFlag) {
					rank = i + 1;
					changeFlag = true;
				}
			}
		}

		return rank;	// (保存しているランキング内に入らなかったら-1を返す)
	}

	public int getRankFromRankingList(int score){
		int	rank = -1;					// 順位情報（返り値）

		// 全ランキング情報を順に見ていく
		for (int i = 0; i < NumberOfScoreInfo; i++) {
			// 引数のスコアより小さいスコアのところまできたらスコア情報を上書きする
			if (score >= scoreInfos [i].score) {
				rank = i + 1;
				break;
			}
		}

		return rank;	// (保存しているランキング内に入らなかったら-1を返す)
	}

	public int getScoreByRank(int rank){
		return scoreInfos [rank - 1].score;
	}

	public string getNameByRank(int rank){
		return scoreInfos [rank - 1].userName;
	}
}
