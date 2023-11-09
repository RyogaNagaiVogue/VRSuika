using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        score = 0;
        scoreToString();
    }

    public void scoreToString()
    {
        scoreText.text = score.ToString();       // �\������X�R�A�̍X�V
    }

    public void UpdateScore(String fruitName)//フルーツ名に応じて加算するスコアを変えます
    {
        switch (fruitName)
        {
            case "Cherry(Clone)":
                score += 1;
                break;
            case "Strawberry(Clone)":
                score += 3;
                break;
            case "Grape(Clone)":
                score += 6;
                break;
            case "Orange(Clone)":
                score += 10;
                break;
            case "Kaki(Clone)":
                score += 15;
                break;
            case "Apple(Clone)":
                score += 21;
                break;
            case "Pear(Clone)":
                score += 28;
                break;
            case "Peach(Clone)":
                score += 36;
                break;
            case "Pine(Clone)":
                score += 45;
                break;
            case "Melon(Clone)":
                score += 55;
                break;
            case "WaterMelon(Clone)":
                score += 66;
                break;

            //============================
            //ここからハロウィン仕様　カボチャ同士なら点数0なのでbreakする
            case "SmallPumpkin(Clone)":
                break;
            case "MiddlePumpkin(Clone)":
                break;
            case "BigPumpkin(Clone)":
                break;
        }

        scoreToString();
    }

}
