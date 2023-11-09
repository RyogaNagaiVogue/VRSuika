using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using TMPro;

public class CSVReader : MonoBehaviour
{

    //CSVよみこむやつ
    TextAsset csvFile; // CSVファイル
    [SerializeField] List<string> csvDatas = new List<string>(); // CSVの中身を入れるリスト;
    public TextMeshProUGUI[] ranking;//ランキングを表示するやつ
    private StreamWriter sw;
    private string dirPath;                                 // 作成するcsvのディレクトリ
    private string fileName;                                // 作成するcsvのファイル名

    void Start()
    {

        dirPath = Application.dataPath + "/Resources/CSV/";   // Assets/Resources/CSVを指定
        fileName = "RankingCSV";                          // RankingCSVに指定
        csvFile = Resources.Load("CSV/RankingCSV") as TextAsset; // Resources下のRankingCSV読み込み
        if (!csvFile)//RankingCSVがなかったら，/Resources/CSV/RankingCSV.csvを作ります
        {
            if (!Directory.Exists(dirPath))
            {                     // Assets/Resources/CSVがなければディレクトリの作成
                Directory.CreateDirectory(dirPath);
            }
        }
        else//RankingCSVがあったら，その中身をランキングに反映します
        {
            StringReader reader = new StringReader(csvFile.text);
            while (reader.Peek() != -1) // reader.Peekが-1になるまで
            {
                string line = reader.ReadLine(); // csvFileの中身を一行ずつ読み込み
                csvDatas.Add(line); //lineをListに追加
            }
        }
        // 上位3行を表示する
        if (csvDatas.Count > 0) ranking[0].text = "1st:" + csvDatas[0];
        else ranking[0].text = "1st:-----";
        if (csvDatas.Count > 1) ranking[1].text = "2nd:" + csvDatas[1];
        else ranking[1].text = "2nd:-----";
        if (csvDatas.Count > 2) ranking[2].text = "3rd:" + csvDatas[2];
        else ranking[2].text = "3rd:-----";

    }

    public void RankingOutput(int score)//ゲームオーバーになったあとに今回のスコアをListにぶちこんでCSVを上書きする関数
    {
        sw = new StreamWriter(dirPath + fileName + ".csv", false, Encoding.GetEncoding("UTF-8"));

        int i = 0;
        for (i = 0; i < csvDatas.Count; i++)
        {
            if (Int32.Parse(csvDatas[i]) < score) break;
        }//今回のScoreが入るところまでIndexを進める
        csvDatas.Insert(i, score.ToString());                 //インサートします

        foreach (string dataStr in csvDatas)                 // csvファイルの中身を書き込み
        {
            sw.WriteLine(dataStr);
        }
        sw.Close();
        Debug.Log(dirPath + fileName + ".csvを作成しました"); // csvを作成したことをコンソールに表示 
    }

    public void RankingUpdate()//再度CSVを読み込んでランキングを更新する
    {

        csvFile = Resources.Load("CSV/RankingCSV") as TextAsset; // Resources下のRankingCSV読み込み

        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1) // reader.Peekが-1になるまで
        {
            string line = reader.ReadLine(); // csvFileの中身を一行ずつ読み込み
            csvDatas.Add(line); //lineをListに追加
        }
        // 上位3行を表示する
        if (csvDatas.Count > 0) ranking[0].text = "1st:" + csvDatas[0];
        else ranking[0].text = "1st:-----";
        if (csvDatas.Count > 1) ranking[1].text = "2nd:" + csvDatas[1];
        else ranking[1].text = "2nd:-----";
        if (csvDatas.Count > 2) ranking[2].text = "3rd:" + csvDatas[2];
        else ranking[2].text = "3rd:-----";
    }
}