using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2RoundCounter : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Sprite score1;
    public Sprite score2;
    public Sprite score3;
    public Sprite score4;
    public Sprite score5;
    public Sprite score6;
    public Sprite score7;

    // Update is called once per frame
    void Update()
    {
        //Player 2
        if (scoreManager.p2Score >= 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = score1;
        }
        if (scoreManager.p2Score >= 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = score2;
        }
        if (scoreManager.p2Score >= 3)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = score3;
        }
        if (scoreManager.p2Score >= 4)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = score4;
        }
        if (scoreManager.p2Score >= 5)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = score5;
        }
        if (scoreManager.p2Score >= 6)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = score6;
        }
        if (scoreManager.p2Score >= 7)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = score7;
        }
    }
}

