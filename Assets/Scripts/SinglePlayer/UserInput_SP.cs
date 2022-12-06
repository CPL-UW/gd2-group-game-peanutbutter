using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UserInput_SP : MonoBehaviour
{
    public GameObject selectedCard;
    private Game_SP game;
    private ScoreManager scoreManager;
    private SpriteRenderer spriteRenderer;

    private List<GameObject> p1PlayedCards;
    private List<GameObject> p2PlayedCards;

    public Button endTurnButton;
    public Button nextTurnButton;

    public static bool isP1Turn = true;
    private GameObject[,] p1CombatPos;
    private GameObject[,] p2CombatPos;

    private List<Vector3> p1Prev;
    private List<Vector3> p2Prev;

    public TextMeshProUGUI roundWinText;

    public TextMeshProUGUI playerLeftText;
    public TextMeshProUGUI playerCentText;
    public TextMeshProUGUI playerRighText;
    public TextMeshProUGUI computerLeftText;
    public TextMeshProUGUI computerCentText;
    public TextMeshProUGUI computerRighText;
    public TextMeshProUGUI ruleText;

    [SerializeField] public Animator textFade;

    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        p1CombatPos = new GameObject[2, 3];
        p2CombatPos = new GameObject[2, 3];

        p1PlayedCards = new List<GameObject>();
        p2PlayedCards = new List<GameObject>();

        game = FindObjectOfType<Game_SP>();
        scoreManager = FindObjectOfType<ScoreManager>();

        p1Prev = new List<Vector3>();
        p2Prev = new List<Vector3>();

        endTurnButton.interactable = true;
        nextTurnButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
        if (isP1Turn)
        {
            P1CombatColor(selectedCard);
        }
        else
        {
            P2CombatColor(selectedCard);
        }
        UpdatePlayerText();
        UpdateComputerText();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Detect which game object was clicked on
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && isP1Turn)
            {
                // What has been hit?
                if (hit.collider.CompareTag("p1ComTL"))
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,0,0,game.p1TopLeft);
                }
                else if (hit.collider.CompareTag("p1ComBL"))
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,1,0,game.p1BotLeft);
                }
                else if (hit.collider.CompareTag("p1ComTC"))
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,0,1,game.p1TopCent);
                }
                else if (hit.collider.CompareTag("p1ComBC"))
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,1,1,game.p1BotCent);
                }
                else if (hit.collider.CompareTag("p1ComTR"))
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,0,2,game.p1TopRigh);
                }
                else if (hit.collider.CompareTag("p1ComBR"))
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,1,2,game.p1BotRigh);
                }
                else if (hit.collider.CompareTag("PlayerCard"))
                {
                    // Clicked place to put card
                    PlayerCard(hit.collider.gameObject);
                }
            }
        }
    }

    void P1Combat(GameObject selected, int row, int col, GameObject position)
    {
        if (selectedCard && (position.GetComponent<SpriteRenderer>().sprite == game.p1PortalSelected))
        {
            playCardAudio();

            if (game.playerHand.Contains(selectedCard.name))
            {
                p1Prev.Add(selectedCard.transform.position);
            }

            selectedCard.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y, selected.transform.position.z - 1);
            p1CombatPos[row, col] = selectedCard;
            p1PlayedCards.Add(selectedCard);
            game.playerHand.Remove(selectedCard.name);
            game.p1HandCards.Remove(selectedCard);
            selectedCard = null;
        }
    }

    void PlayerCard(GameObject selected)
    {
        // Check rules for not selecting card
        if ((game.playerHand.Contains(selected.name) && (game.playerHand.Count <= 1)) || game.playerDeck.Contains(selected.name) || game.p1Discard.Contains(selected.name))
        {
            // Do nothing
        }
        // Remove card from combat arena
        else if ( selectedCard && (selected.name == selectedCard.name) && p1PlayedCards.Contains(selectedCard) )
        {
            selectedCard.transform.position = p1Prev.ElementAt<Vector3>(0);
            p1Prev.RemoveAt(0);
            p1PlayedCards.Remove(selectedCard);
            game.playerHand.Add(selectedCard.name);
            game.p1HandCards.Add(selectedCard);
            for (int i = 0; i < p1CombatPos.GetLength(0); i++)
            {
                for (int j = 0; j < p1CombatPos.GetLength(1); j++)
                {
                    if ( p1CombatPos[i,j] && (selectedCard.name == p1CombatPos[i, j].name) )
                    {
                        p1CombatPos[i, j] = null;
                    }
                }
            }
            selectedCard = null;
        }
        else
        {
            selectedCard = selected;
            ruleText.text = selected.GetComponent<Selectable>().rule.ToString();
        }
    }

    void P1CombatColor(GameObject card)
    {
        if (selectedCard && ((card.GetComponent<Selectable>().value < 6) || (card.GetComponent<Selectable>().value > 10)) )
        {
            game.p1TopLeft.GetComponent<SpriteRenderer>().sprite = game.p1PortalSelected;
            game.p1BotLeft.GetComponent<SpriteRenderer>().sprite = game.p1PortalSelected;
            game.p1TopCent.GetComponent<SpriteRenderer>().sprite = game.p1PortalSelected;
            game.p1BotCent.GetComponent<SpriteRenderer>().sprite = game.p1PortalSelected;
            game.p1TopRigh.GetComponent<SpriteRenderer>().sprite = game.p1PortalSelected;
            game.p1BotRigh.GetComponent<SpriteRenderer>().sprite = game.p1PortalSelected;
        }
        else if (selectedCard)
        {
            game.p1TopLeft.GetComponent<SpriteRenderer>().sprite = game.p1PortalSelected;
            game.p1BotLeft.GetComponent<SpriteRenderer>().sprite = game.p1Portal;
            game.p1TopCent.GetComponent<SpriteRenderer>().sprite = game.p1PortalSelected;
            game.p1BotCent.GetComponent<SpriteRenderer>().sprite = game.p1Portal;
            game.p1TopRigh.GetComponent<SpriteRenderer>().sprite = game.p1PortalSelected;
            game.p1BotRigh.GetComponent<SpriteRenderer>().sprite = game.p1Portal;
        }
        else
        {
            game.p1TopLeft.GetComponent<SpriteRenderer>().sprite = game.p1Portal;
            game.p1BotLeft.GetComponent<SpriteRenderer>().sprite = game.p1Portal;
            game.p1TopCent.GetComponent<SpriteRenderer>().sprite = game.p1Portal;
            game.p1BotCent.GetComponent<SpriteRenderer>().sprite = game.p1Portal;
            game.p1TopRigh.GetComponent<SpriteRenderer>().sprite = game.p1Portal;
            game.p1BotRigh.GetComponent<SpriteRenderer>().sprite = game.p1Portal;
        }
    }

    void P2CombatColor(GameObject card)
    {
        if (selectedCard && ((card.GetComponent<Selectable>().value < 6) || (card.GetComponent<Selectable>().value > 10)))
        {
            game.p2TopLeft.GetComponent<SpriteRenderer>().sprite = game.p2PortalSelected;
            game.p2BotLeft.GetComponent<SpriteRenderer>().sprite = game.p2PortalSelected;
            game.p2TopCent.GetComponent<SpriteRenderer>().sprite = game.p2PortalSelected;
            game.p2BotCent.GetComponent<SpriteRenderer>().sprite = game.p2PortalSelected;
            game.p2TopRigh.GetComponent<SpriteRenderer>().sprite = game.p2PortalSelected;
            game.p2BotRigh.GetComponent<SpriteRenderer>().sprite = game.p2PortalSelected;
        }
        else if (selectedCard)
        {
            game.p2TopLeft.GetComponent<SpriteRenderer>().sprite = game.p2PortalSelected;
            game.p2BotLeft.GetComponent<SpriteRenderer>().sprite = game.p2Portal;
            game.p2TopCent.GetComponent<SpriteRenderer>().sprite = game.p2PortalSelected;
            game.p2BotCent.GetComponent<SpriteRenderer>().sprite = game.p2Portal;
            game.p2TopRigh.GetComponent<SpriteRenderer>().sprite = game.p2PortalSelected;
            game.p2BotRigh.GetComponent<SpriteRenderer>().sprite = game.p2Portal;
        }
        else
        {
            game.p2TopLeft.GetComponent<SpriteRenderer>().sprite = game.p2Portal;
            game.p2BotLeft.GetComponent<SpriteRenderer>().sprite = game.p2Portal;
            game.p2TopCent.GetComponent<SpriteRenderer>().sprite = game.p2Portal;
            game.p2BotCent.GetComponent<SpriteRenderer>().sprite = game.p2Portal;
            game.p2TopRigh.GetComponent<SpriteRenderer>().sprite = game.p2Portal;
            game.p2BotRigh.GetComponent<SpriteRenderer>().sprite = game.p2Portal;
        }
    }

    public void NextTurn()
    {
        if ((p2PlayedCards.Count > 0) && CheckPlacement(false))
        {
            selectedCard = null;
            isP1Turn = !isP1Turn;
            endTurnButton.interactable = true;
            nextTurnButton.interactable = false;
            foreach (GameObject card in p2PlayedCards)
            {
                // game.p2HandCards.Remove(card);
                game.computerHand.Remove(card.name);
            }
            P2CombatColor(selectedCard);
            EndTurn();
        }
    }

    public void ChangeTurn()
    {
        if (isP1Turn && (p1PlayedCards.Count>0) && CheckPlacement())
        {
            selectedCard = null;
            isP1Turn = !isP1Turn;
            endTurnButton.interactable = false;
            nextTurnButton.interactable = true;
            P1CombatColor(selectedCard);
            ComputerCombatAI();
        }
    }

    private void ComputerCombatAI()
    {
        game.p2HandCards[0].transform.position = game.p2TopRigh.transform.position - new Vector3(0, 0, 1);
        p2CombatPos[0, 2] = game.p2HandCards[0];
        p2PlayedCards.Add(game.p2HandCards[0]);
        game.p2HandCards[1].transform.position = game.p2BotRigh.transform.position - new Vector3(0, 0, 1);
        p2CombatPos[1, 2] = game.p2HandCards[1];
        p2PlayedCards.Add(game.p2HandCards[1]);
        game.p2HandCards[2].transform.position = game.p2TopCent.transform.position - new Vector3(0, 0, 1);
        p2CombatPos[0, 1] = game.p2HandCards[2];
        p2PlayedCards.Add(game.p2HandCards[2]);
        game.p2HandCards[3].transform.position = game.p2BotCent.transform.position - new Vector3(0, 0, 1);
        p2CombatPos[1, 1] = game.p2HandCards[3];
        p2PlayedCards.Add(game.p2HandCards[3]);
        game.p2HandCards[4].transform.position = game.p2TopLeft.transform.position - new Vector3(0, 0, 1);
        p2CombatPos[0, 0] = game.p2HandCards[4];
        p2PlayedCards.Add(game.p2HandCards[4]);
        foreach (GameObject card in p2PlayedCards)
        {
            card.GetComponent<Selectable>().faceUp = true;
        }
    }

    bool CheckPlacement(bool isP1 = true)
    {
        bool isValid = true;

        // Must be at least three cards placed

        // Top rows must be filled before bottom rows

        // Only values 1-5 & face cards can be in back row

        return isValid;
    }

    public void EndTurn()
    {
        int[] results = CheckWinner();
        int result = results[0];
        int leftColWinner = results[1];
        int centColWinner = results[2];
        int righColWinner = results[3];

        if (result == 1)
        {
            print("Player 1 wins this round!");
            scoreManager.UpdateP1Score();
            roundWinText.text = "Player 1 wins this round!";
            textFade.SetBool("isShowingText", true);
        }
        else if (result == 2)
        {
            print("Player 2 wins the round!");
            scoreManager.UpdateP2Score();
            roundWinText.text = "Player 2 wins this round!";
            textFade.SetBool("isShowingText", true);
        }
        else
        {
            print("It's a tie!");
            roundWinText.text = "It's a tie";
            textFade.SetBool("isShowingText", true);
        }

        foreach (GameObject card in p1PlayedCards)
        {
            card.transform.position = new Vector3(game.p1DiscardPos.transform.position.x, game.p1DiscardPos.transform.position.y, game.p1DiscardPos.transform.position.z-0.1f);
            card.GetComponent<Selectable>().faceUp = true;
            game.p1Discard.Add(card.name);
        }
        foreach (GameObject card in p2PlayedCards)
        {
            card.transform.position = new Vector3(game.p2DiscardPos.transform.position.x, game.p2DiscardPos.transform.position.y, game.p2DiscardPos.transform.position.z - 0.1f);
            card.GetComponent<Selectable>().faceUp = true;
            game.p1Discard.Add(card.name);
        }

        int p1HandCount = game.p1HandCards.Count;
        for (int i = 0; i < p1HandCount; i++)
        {
            GameObject card = game.p1HandCards.ElementAt<GameObject>(0);
            game.p1HandCards.Remove(card);
            Destroy(card);
        }
        int p2HandCount = game.p2HandCards.Count;
        for (int i = 0; i < p2HandCount; i++)
        {
            GameObject card = game.p2HandCards.ElementAt<GameObject>(0);
            game.p2HandCards.Remove(card);
            Destroy(card);
        }

        p1PlayedCards.Clear();
        p2PlayedCards.Clear();

        p1Prev.Clear();
        p2Prev.Clear();

        Array.Clear(p1CombatPos,0,p1CombatPos.Length);
        Array.Clear(p2CombatPos,0,p2CombatPos.Length);

        selectedCard = null;
        game.StartOfTurn();
    }

    double GetValue(GameObject card)
    {
        if (card)
        {
            return card.GetComponent<Selectable>().value;
        }
        else
        {
            return 0;
        }
    }

    void UpdatePlayerText()
    {
        double topLeft = GetValue(p1CombatPos[0, 0]);
        double botLeft = GetValue(p1CombatPos[1, 0]);
        double topCent = GetValue(p1CombatPos[0, 1]);
        double botCent = GetValue(p1CombatPos[1, 1]);
        double topRigh = GetValue(p1CombatPos[0, 2]);
        double botRigh = GetValue(p1CombatPos[1, 2]);

        if (topLeft == 14)
        {
            topLeft = 0;
        }
        if (botLeft == 14)
        {
            botLeft = 0;
        }
        if (topCent == 14)
        {
            topCent = 0;
        }
        if (botCent == 14)
        {
            botCent = 0;
        }
        if (topRigh == 14)
        {
            topRigh = 0;
        }
        if (botRigh == 14)
        {
            botRigh = 0;
        }
        if (botLeft > 10)
        {
            botLeft = botLeft / 2;
        }
        if (botCent > 10)
        {
            botCent = botCent / 2;
        }
        if (botRigh > 10)
        {
            botRigh = botRigh / 2;
        }

        playerLeftText.text = (topLeft+botLeft).ToString();
        playerCentText.text = (topCent+botCent).ToString();
        playerRighText.text = (topRigh+botRigh).ToString();
    }

    void UpdateComputerText()
    {
        double topLeft = GetValue(p2CombatPos[0, 0]);
        double botLeft = GetValue(p2CombatPos[1, 0]);
        double topCent = GetValue(p2CombatPos[0, 1]);
        double botCent = GetValue(p2CombatPos[1, 1]);
        double topRigh = GetValue(p2CombatPos[0, 2]);
        double botRigh = GetValue(p2CombatPos[1, 2]);

        if (topLeft == 14)
        {
            topLeft = 0;
        }
        if (botLeft == 14)
        {
            botLeft = 0;
        }
        if (topCent == 14)
        {
            topCent = 0;
        }
        if (botCent == 14)
        {
            botCent = 0;
        }
        if (topRigh == 14)
        {
            topRigh = 0;
        }
        if (botRigh == 14)
        {
            botRigh = 0;
        }
        if (botLeft > 10)
        {
            botLeft = botLeft / 2;
        }
        if (botCent > 10)
        {
            botCent = botCent / 2;
        }
        if (botRigh > 10)
        {
            botRigh = botRigh / 2;
        }

        computerLeftText.text = (topLeft + botLeft).ToString();
        computerCentText.text = (topCent + botCent).ToString();
        computerRighText.text = (topRigh + botRigh).ToString();
    }

    int[] CheckWinner() // Return 1 if p1 and 2 if p2 and 0 if tie
    {
        int leftColWinner = -1;
        int centColWinner = -1;
        int righColWinner = -1;

        // Calculate value of each card in combat arena
        double p1TopLeft = GetValue(p1CombatPos[0, 0]);
        double p1BotLeft = GetValue(p1CombatPos[1, 0]);
        double p1TopCent = GetValue(p1CombatPos[0, 1]);
        double p1BotCent = GetValue(p1CombatPos[1, 1]);
        double p1TopRigh = GetValue(p1CombatPos[0, 2]);
        double p1BotRigh = GetValue(p1CombatPos[1, 2]);

        double p2TopLeft = GetValue(p2CombatPos[0, 0]);
        double p2BotLeft = GetValue(p2CombatPos[1, 0]);
        double p2TopCent = GetValue(p2CombatPos[0, 1]);
        double p2BotCent = GetValue(p2CombatPos[1, 1]);
        double p2TopRigh = GetValue(p2CombatPos[0, 2]);
        double p2BotRigh = GetValue(p2CombatPos[1, 2]);

        /*
         * Find all win conditions
         */
        //// Red joker wins if opponents column total is odd
        // Player 1
        if ( (p1TopLeft == 14 && p1CombatPos[0,0].GetComponent<Selectable>().suit.Equals("R") && leftColWinner == -1) || 
             (p1BotLeft == 14 && p1CombatPos[1,0].GetComponent<Selectable>().suit.Equals("R") && leftColWinner == -1) )
        {
            if (((p2TopRigh + p2BotRigh) % 2) == 1)
            {
                leftColWinner = 1;
            }
        }
        else if ( (p1TopCent == 14 && p1CombatPos[0,1].GetComponent<Selectable>().suit.Equals("R") && centColWinner == -1) || 
                  (p1BotCent == 14 && p1CombatPos[1,1].GetComponent<Selectable>().suit.Equals("R") && centColWinner == -1) )
        {
            if (((p2TopCent + p2BotCent) % 2) == 1)
            {
                centColWinner = 1;
            }
        }
        else if ( (p1TopRigh == 14 && p1CombatPos[0,2].GetComponent<Selectable>().suit.Equals("R") && righColWinner == -1) || 
                  (p1BotRigh == 14 && p1CombatPos[1,2].GetComponent<Selectable>().suit.Equals("R") && righColWinner == -1) )
        {
            if (((p2TopLeft + p2BotLeft) % 2) == 1)
            {
                righColWinner = 1;
            }
        }
        // Player 2
        if ( (p2TopLeft == 14 && p2CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("R") && righColWinner == -1) ||
             (p2BotLeft == 14 && p2CombatPos[1, 0].GetComponent<Selectable>().suit.Equals("R") && righColWinner == -1) )
        {
            if (((p1TopRigh + p1BotRigh) % 2) == 1)
            {
                leftColWinner = 2;
            }
        }
        else if ( (p2TopCent == 14 && p2CombatPos[0,1].GetComponent<Selectable>().suit.Equals("R") && centColWinner == -1) ||
                  (p2BotCent == 14 && p2CombatPos[1,1].GetComponent<Selectable>().suit.Equals("R") && centColWinner == -1) )
        {
            if (((p1TopCent + p1BotCent) % 2) == 1)
            {
                centColWinner = 2;
            }
        }
        else if ( (p2TopRigh == 14 && p2CombatPos[0,2].GetComponent<Selectable>().suit.Equals("R") && leftColWinner == -1) ||
                  (p2BotRigh == 14 && p2CombatPos[1,2].GetComponent<Selectable>().suit.Equals("R") && leftColWinner == -1) )
        {
            if (((p1TopLeft + p1BotLeft) % 2) == 1)
            {
                righColWinner = 2;
            }
        }

        //// Black joker wins if opponents column total is even
        // Player 1
        if ( (p1TopLeft == 14 && p1CombatPos[0,0].GetComponent<Selectable>().suit.Equals("B") && leftColWinner == -1) ||
             (p1BotLeft == 14 && p1CombatPos[1,0].GetComponent<Selectable>().suit.Equals("B") && leftColWinner == -1) )
        {
            if (((p2TopRigh + p2BotRigh) % 2) == 0)
            {
                leftColWinner = 1;
            }
        }
        else if ( (p1TopCent == 14 && p1CombatPos[0,1].GetComponent<Selectable>().suit.Equals("B") && centColWinner == -1) ||
                  (p1BotCent == 14 && p1CombatPos[1,1].GetComponent<Selectable>().suit.Equals("B") && centColWinner == -1) )
        {
            if (((p2TopCent + p2BotCent) % 2) == 0)
            {
                centColWinner = 1;
            }
        }
        else if ( (p1TopRigh == 14 && p1CombatPos[0,2].GetComponent<Selectable>().suit.Equals("B") && righColWinner == -1) ||
                  (p1BotRigh == 14 && p1CombatPos[1,2].GetComponent<Selectable>().suit.Equals("B") && righColWinner == -1) )
        {
            if (((p2TopLeft + p2BotLeft) % 2) == 0)
            {
                righColWinner = 1;
            }
        }
        // Player 2
        if ( (p2TopLeft == 14 && p2CombatPos[0,0].GetComponent<Selectable>().suit.Equals("B") && righColWinner == -1) ||
             (p2BotLeft == 14 && p2CombatPos[1,0].GetComponent<Selectable>().suit.Equals("B") && righColWinner == -1) )
        {
            if (((p1TopRigh + p1BotRigh) % 2) == 0)
            {
                leftColWinner = 2;
            }
        }
        else if ( (p2TopCent == 14 && p2CombatPos[0,1].GetComponent<Selectable>().suit.Equals("B") && centColWinner == -1) ||
                  (p2BotCent == 14 && p2CombatPos[1,1].GetComponent<Selectable>().suit.Equals("B") && centColWinner == -1) )
        {
            if (((p1TopCent + p1BotCent) % 2) == 0)
            {
                centColWinner = 2;
            }
        }
        else if ( (p2TopRigh == 14 && p2CombatPos[0,2].GetComponent<Selectable>().suit.Equals("B") && leftColWinner == -1) ||
                  (p2BotRigh == 14 && p2CombatPos[1,2].GetComponent<Selectable>().suit.Equals("B") && leftColWinner == -1) )
        {
            if (((p1TopLeft + p1BotLeft) % 2) == 0)
            {
                righColWinner = 2;
            }
        }

        //// Change values of jokers to 0
        // Player 1
        if (p1TopLeft == 14)
        {
            p1TopLeft = 0;
        }
        if (p1BotLeft == 14)
        {
            p1BotLeft = 0;
        }
        if (p1TopCent == 14)
        {
            p1TopCent = 0;
        }
        if (p1BotCent == 14)
        {
            p1BotCent = 0;
        }
        if (p1TopRigh == 14)
        {
            p1TopRigh = 0;
        }
        if (p1BotRigh == 14)
        {
            p1BotRigh = 0;
        }
        // Player 2
        if (p2TopLeft == 14)
        {
            p2TopLeft = 0;
        }
        if (p2BotLeft == 14)
        {
            p2BotLeft = 0;
        }
        if (p2TopCent == 14)
        {
            p2TopCent = 0;
        }
        if (p2BotCent == 14)
        {
            p2BotCent = 0;
        }
        if (p2TopRigh == 14)
        {
            p2TopRigh = 0;
        }
        if (p2BotRigh == 14)
        {
            p2BotRigh = 0;
        }

        //// Face cards are half value when in back row
        // Player 1
        if (p1BotLeft > 10)
        {
            p1BotLeft = p1BotLeft / 2;
        }
        if (p1BotCent > 10)
        {
            p1BotCent = p1BotCent / 2;
        }
        if (p1BotRigh > 10)
        {
            p1BotRigh = p1BotRigh / 2;
        }
        // Player 2
        if (p2BotLeft > 10)
        {
            p2BotLeft = p2BotLeft / 2;
        }
        if (p2BotCent > 10)
        {
            p2BotCent = p2BotCent / 2;
        }
        if (p2BotRigh > 10)
        {
            p2BotRigh = p2BotRigh / 2;
        }

        //// Aces (in front or back row) beat face cards in front rows
        // Player 1
        if ( (p1TopLeft == 1 || p1BotLeft == 1) && p2TopRigh > 10 && leftColWinner == -1)
        {
            leftColWinner = 1;
        }
        if ((p1TopCent == 1 || p1BotCent == 1) && p2TopCent > 10 && centColWinner == -1)
        {
            centColWinner = 1;
        }
        if ((p1TopRigh == 1 || p1BotRigh == 1) && p2TopLeft > 10 && righColWinner == -1)
        {
            righColWinner = 1;
        }
        // Player 2
        if ((p2TopLeft == 1 || p2BotLeft == 1) && p1TopRigh > 10 && righColWinner == -1)
        {
            leftColWinner = 2;
        }
        if ((p2TopCent == 1 || p2BotCent == 1) && p1TopCent > 10 && centColWinner == -1)
        {
            centColWinner = 2;
        }
        if ((p2TopRigh == 1 || p2BotRigh == 1) && p1TopLeft > 10 && leftColWinner == -1)
        {
            righColWinner = 2;
        }

        //// Suit advantages
        //// Spades > Diamonds
        // Player 1
        if (p1CombatPos[0,0] && p1CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("S") && p2CombatPos[0, 2] && p2CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("D"))
        {
            p1TopLeft += 2;
        }
        if (p1CombatPos[0, 1] && p1CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("S") && p2CombatPos[0, 1] && p2CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("D"))
        {
            p1TopCent += 2;
        }
        if (p1CombatPos[0, 2] && p1CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("S") && p2CombatPos[0, 0] && p2CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("D"))
        {
            p1TopRigh += 2;
        }
        // Player 2
        if (p2CombatPos[0, 0] && p2CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("S") && p1CombatPos[0, 2] && p1CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("D"))
        {
            p2TopLeft += 2;
        }
        if (p2CombatPos[0, 1] && p2CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("S") && p1CombatPos[0, 1] && p1CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("D"))
        {
            p2TopCent += 2;
        }
        if (p2CombatPos[0, 2] && p2CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("S") && p1CombatPos[0, 0] && p1CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("D"))
        {
            p2TopRigh += 2;
        }

        //// Diamonds > Clubs
        // Player 1
        if (p1CombatPos[0, 0] && p1CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("D") && p2CombatPos[0, 2] && p2CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("C"))
        {
            p1TopLeft += 2;
        }
        if (p1CombatPos[0, 1] && p1CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("D") && p2CombatPos[0, 1] && p2CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("C"))
        {
            p1TopCent += 2;
        }
        if (p1CombatPos[0, 2] && p1CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("D") && p2CombatPos[0, 0] && p2CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("C"))
        {
            p1TopRigh += 2;
        }
        // Player 2
        if (p2CombatPos[0, 0] && p2CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("D") && p1CombatPos[0, 2] && p1CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("C"))
        {
            p2TopLeft += 2;
        }
        if (p2CombatPos[0, 1] && p2CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("D") && p1CombatPos[0, 1] && p1CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("C"))
        {
            p2TopCent += 2;
        }
        if (p2CombatPos[0, 2] && p2CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("D") && p1CombatPos[0, 0] && p1CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("C"))
        {
            p2TopRigh += 2;
        }

        //// Clubs > Hearts
        // Player 1
        if (p1CombatPos[0, 0] && p1CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("C") && p2CombatPos[0, 2] && p2CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("H"))
        {
            p1TopLeft += 2;
        }
        if (p1CombatPos[0, 1] && p1CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("C") && p2CombatPos[0, 1] && p2CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("H"))
        {
            p1TopCent += 2;
        }
        if (p1CombatPos[0, 2] && p1CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("C") && p2CombatPos[0, 0] && p2CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("H"))
        {
            p1TopRigh += 2;
        }
        // Player 2
        if (p2CombatPos[0, 0] && p2CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("C") && p1CombatPos[0, 2] && p1CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("H"))
        {
            p2TopLeft += 2;
        }
        if (p2CombatPos[0, 1] && p2CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("C") && p1CombatPos[0, 1] && p1CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("H"))
        {
            p2TopCent += 2;
        }
        if (p2CombatPos[0, 2] && p2CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("C") && p1CombatPos[0, 0] && p1CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("H"))
        {
            p2TopRigh += 2;
        }

        //// Hearts > Spades
        // Player 1
        if (p1CombatPos[0, 0] && p1CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("H") && p2CombatPos[0, 2] && p2CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("S"))
        {
            p1TopLeft += 2;
        }
        if (p1CombatPos[0, 1] && p1CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("H") && p2CombatPos[0, 1] && p2CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("S"))
        {
            p1TopCent += 2;
        }
        if (p1CombatPos[0, 2] && p1CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("H") && p2CombatPos[0, 0] && p2CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("S"))
        {
            p1TopRigh += 2;
        }
        // Player 2
        if (p2CombatPos[0, 0] && p2CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("H") && p1CombatPos[0, 2] && p1CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("S"))
        {
            p2TopLeft += 2;
        }
        if (p2CombatPos[0, 1] && p2CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("H") && p1CombatPos[0, 1] && p1CombatPos[0, 1].GetComponent<Selectable>().suit.Equals("S"))
        {
            p2TopCent += 2;
        }
        if (p2CombatPos[0, 2] && p2CombatPos[0, 2].GetComponent<Selectable>().suit.Equals("H") && p1CombatPos[0, 0] && p1CombatPos[0, 0].GetComponent<Selectable>().suit.Equals("S"))
        {
            p2TopRigh += 2;
        }

        //// General winning calculations
        // Left column
        if ((p1TopLeft + p1BotLeft) > (p2TopRigh + p2BotRigh))
        {
            leftColWinner = 1;
        }
        else if ((p1TopLeft + p1BotLeft) < (p2TopRigh + p2BotRigh))
        {
            leftColWinner = 2;
        }
        // Center column
        if ((p1TopCent + p1BotCent) > (p2TopCent + p2BotCent))
        {
            centColWinner = 1;
        }
        else if ((p1TopCent + p1BotCent) < (p2TopCent + p2BotCent))
        {
            centColWinner = 2;
        }
        // Right column
        if ((p1TopRigh + p1BotRigh) > (p2TopLeft + p2BotLeft))
        {
            righColWinner = 1;
        }
        else if ((p1TopRigh + p1BotRigh) < (p2TopLeft + p2BotLeft))
        {
            righColWinner = 2;
        }

        //// Calculate column wins
        int p1ColWins = 0;
        int p2ColWins = 0;
        // Player 1
        if (leftColWinner == 1)
        {
            p1ColWins++;
        }
        if (centColWinner == 1)
        {
            p1ColWins++;
        }
        if (righColWinner == 1)
        {
            p1ColWins++;
        }
        // Player 2
        if (leftColWinner == 2)
        {
            p2ColWins++;
        }
        if (centColWinner == 2)
        {
            p2ColWins++;
        }
        if (righColWinner == 2)
        {
            p2ColWins++;
        }

        // Return who's the winner
        int winner = 0;
        if (p1ColWins > p2ColWins)
        {
            winner = 1;
        }
        else if (p2ColWins > p1ColWins)
        {
            winner = 2;
        }
        return new int[4] {winner, leftColWinner, centColWinner, righColWinner};
    }

    void playCardAudio() {
        source.PlayOneShot(clip);
    }
}
