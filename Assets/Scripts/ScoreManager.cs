using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
   /* public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;
    public TextMeshProUGUI p1Score2PText;
    public TextMeshProUGUI p2Score1PText;*/

    public int p1Score = 0;
    public int p2Score = 0;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*p1ScoreText.text = "Score: " + p1Score.ToString();
        p2ScoreText.text = "Score: " + p2Score.ToString();
        p1Score2PText.text = "Score: " + p1Score.ToString();
        p2Score1PText.text = "Score: " + p2Score.ToString();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore()
    {
        p1Score = 0;
        p2Score = 0;
        /*p1ScoreText.text = "Score: " + p1Score.ToString();
        p2ScoreText.text = "Score: " + p2Score.ToString();
        p1Score2PText.text = "Score: " + p1Score.ToString();
        p2Score1PText.text = "Score: " + p2Score.ToString();*/
    }

    public void UpdateP1Score()
    {
        p1Score++;
        /*p1ScoreText.text = "Score: " + p1Score.ToString();
        p1Score2PText.text = "Score: " + p1Score.ToString();*/
    }

    public void UpdateP2Score()
    {
        p2Score++;
        /*p2ScoreText.text = "Score: " + p2Score.ToString();
        p2Score1PText.text = "Score: " + p2Score.ToString();*/
    }
}
