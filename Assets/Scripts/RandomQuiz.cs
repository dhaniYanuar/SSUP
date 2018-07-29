using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomQuiz : MonoBehaviour {

    public static int Point;
    public float Timer = 30.0f;
    public GameObject CanvasQuiz;
    public GameObject[] PosJwb;
    public string[] noQuiz, jawaban;
    public Text quizLabel, choice1Label, choice2Label, choice3Label, choice4Label, pointLabel, timerLabel;
    public int noRandom;
    public Touch3D intRandom;

    public void randomNilai()
    {
        noRandom = Random.Range(intRandom.MinRandom, intRandom.MaxRandom);
        changeQuiz();
    }

    public void Start()
    {
        randomNilai();
    }

    public void Update()
    {
        pointLabel.text = "Point: " + Point.ToString();
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            CanvasQuiz.SetActive(false);
            Timer = 30.0f;
        }
        timerLabel.text = Timer.ToString();
    }

    public void changeQuiz()
    {
        quizLabel.text = noQuiz[noRandom - 1];
        choice1Label.text = jawaban[(noRandom - 1) * 4 + 0];
        choice2Label.text = jawaban[(noRandom - 1) * 4 + 1];
        choice3Label.text = jawaban[(noRandom - 1) * 4 + 2];
        choice4Label.text = jawaban[(noRandom - 1) * 4 + 3];
        RanomTrueButton();
    }

    public void Benar()
    {
        Point = Point + 50;
        CanvasQuiz.SetActive(false);
    }
    public void Salah()
    {
        Point = Point -10;
        CanvasQuiz.SetActive(false);
    }

    public void RanomTrueButton()
    {
        int nilaiRandom = Random.Range(1, 5);
        PosJwb[0].transform.position = PosJwb[nilaiRandom].transform.position;
        PosJwb[nilaiRandom].transform.position = PosJwb[1].transform.position;
        PosJwb[1].transform.position = PosJwb[0].transform.position;
    }
}
