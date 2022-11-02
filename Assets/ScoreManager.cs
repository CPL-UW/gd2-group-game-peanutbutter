using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    public int p1Score = 0;
    public int p2Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        p1ScoreText.text = "Score: " + p1Score.ToString();
        p2ScoreText.text = "Score: " + p2Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
