using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UserInput : MonoBehaviour
{
    public GameObject selectedCard;
    private Game game;
    private ToggleCamera toggleCamera;
    private ScoreManager scoreManager;

    private List<GameObject> p1PlayedCards;
    private List<GameObject> p2PlayedCards;

    public static bool isP1Turn = true;
    private GameObject[,] p1CombatPos;
    private GameObject[,] p2CombatPos;

    private List<Vector3> p1Prev;
    private List<Vector3> p2Prev;

    // Start is called before the first frame update
    void Start()
    {
        p1CombatPos = new GameObject[2, 3];
        p2CombatPos = new GameObject[2, 3];

        p1PlayedCards = new List<GameObject>();
        p2PlayedCards = new List<GameObject>();

        game = FindObjectOfType<Game>();
        toggleCamera = FindObjectOfType<ToggleCamera>();
        scoreManager = FindObjectOfType<ScoreManager>();

        p1Prev = new List<Vector3>();
        p2Prev = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Detect which game object was clicked on
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                // What has been hit?
                if (hit.collider.CompareTag("p1ComTL") && toggleCamera.p1Camera.activeSelf)
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,0,0);
                }
                else if (hit.collider.CompareTag("p1ComBL") && toggleCamera.p1Camera.activeSelf)
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,1,0);
                }
                else if (hit.collider.CompareTag("p1ComTC") && toggleCamera.p1Camera.activeSelf)
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,0,1);
                }
                else if (hit.collider.CompareTag("p1ComBC") && toggleCamera.p1Camera.activeSelf)
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,1,1);
                }
                else if (hit.collider.CompareTag("p1ComTR") && toggleCamera.p1Camera.activeSelf)
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,0,2);
                }
                else if (hit.collider.CompareTag("p1ComBR") && toggleCamera.p1Camera.activeSelf)
                {
                    // Clicked place to put card
                    P1Combat(hit.collider.gameObject,1,2);
                }
                else if (hit.collider.CompareTag("PlayerCard") && toggleCamera.p1Camera.activeSelf)
                {
                    // Clicked place to put card
                    PlayerCard(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("p2ComTL") && toggleCamera.p2Camera.activeSelf)
                {
                    // Clicked place to put card
                    P2Combat(hit.collider.gameObject,0,0);
                }
                else if (hit.collider.CompareTag("p2ComBL") && toggleCamera.p2Camera.activeSelf)
                {
                    // Clicked place to put card
                    P2Combat(hit.collider.gameObject,1,0);
                }
                else if (hit.collider.CompareTag("p2ComTC") && toggleCamera.p2Camera.activeSelf)
                {
                    // Clicked place to put card
                    P2Combat(hit.collider.gameObject,0,1);
                }
                else if (hit.collider.CompareTag("p2ComBC") && toggleCamera.p2Camera.activeSelf)
                {
                    // Clicked place to put card
                    P2Combat(hit.collider.gameObject,1,1);
                }
                else if (hit.collider.CompareTag("p2ComTR") && toggleCamera.p2Camera.activeSelf)
                {
                    // Clicked place to put card
                    P2Combat(hit.collider.gameObject,0,2);
                }
                else if (hit.collider.CompareTag("p2ComBR") && toggleCamera.p2Camera.activeSelf)
                {
                    // Clicked place to put card
                    P2Combat(hit.collider.gameObject,1,2);
                }
                else if (hit.collider.CompareTag("ComputerCard") && toggleCamera.p2Camera.activeSelf)
                {
                    // Clicked place to put card
                    ComputerCard(hit.collider.gameObject);
                }
            }
        }
    }

    void P1Combat(GameObject selected, int row, int col)
    {
        if (selectedCard)
        {
            if (game.playerHand.Contains(selectedCard.name))
            {
                p1Prev.Add(selectedCard.transform.position);
            }
            
            selectedCard.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y, selected.transform.position.z - 1);
            p1CombatPos[row, col] = selectedCard;
            p1PlayedCards.Add(selectedCard);
            game.playerHand.Remove(selectedCard.name);
            game.p1HandCards.Remove(selectedCard);
        }
        selectedCard = null;
    }

    void PlayerCard(GameObject selected)
    {
        // Check rules for not selecting card
        if ((game.playerHand.Contains(selected.name) && (game.playerHand.Count <= 1)) || game.playerDeck.Contains(selected.name))
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
        }
    }
    
    void P2Combat(GameObject selected, int row, int col)
    {
        if (selectedCard)
        {
            if (game.computerHand.Contains(selectedCard.name))
            {
                p2Prev.Add(selectedCard.transform.position);
            }

            selectedCard.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y, selected.transform.position.z - 1);
            p2CombatPos[row, col] = selectedCard;
            p2PlayedCards.Add(selectedCard);
            game.computerHand.Remove(selectedCard.name);
            game.p2HandCards.Remove(selectedCard);
        }
        selectedCard = null;
    }

    void ComputerCard(GameObject selected)
    {
        // Check rules for not selecting card
        if (game.computerHand.Contains(selected.name) && (game.computerHand.Count <= 1) || game.computerDeck.Contains(selected.name))
        {
            // Do nothing
        }
        // Remove card from combat arena
        else if (selectedCard && (selected.name == selectedCard.name) && p2PlayedCards.Contains(selectedCard))
        {
            selectedCard.transform.position = p2Prev.ElementAt<Vector3>(0);
            p2Prev.RemoveAt(0);
            p2PlayedCards.Remove(selectedCard);
            game.computerHand.Add(selectedCard.name);
            game.p2HandCards.Add(selectedCard);
            for (int i = 0; i < p2CombatPos.GetLength(0); i++)
            {
                for (int j = 0; j < p2CombatPos.GetLength(1); j++)
                {
                    if (p2CombatPos[i, j] && (selectedCard.name == p2CombatPos[i, j].name))
                    {
                        p2CombatPos[i, j] = null;
                    }
                }
            }
            selectedCard = null;
        }
        else
        {
            selectedCard = selected;
        }
    }

    public void DiscardCards()
    {
        selectedCard = null;
    }

    public void ChangeTurn()
    {
        selectedCard = null;
        if (isP1Turn)
        {
            foreach (GameObject card in game.p1HandCards)
            {
                card.GetComponent<Selectable>().faceUp = false;
            }
            foreach (GameObject card in p1PlayedCards)
            {
                card.GetComponent<Selectable>().faceUp = false;
            }
            foreach (GameObject card in game.p2HandCards)
            {
                card.GetComponent<Selectable>().faceUp = true;
            }
            toggleCamera.ToggleP2Camera();
        }
        else
        {
            toggleCamera.ToggleP1Camera();
            EndTurn();
        }
        isP1Turn = !isP1Turn;
    }
    
    public void EndTurn()
    {
        int p1ColWins = 0;
        int p2ColWins = 0;

        // Calculate who wins round
        int p1LefCol = GetValue(p1CombatPos[0, 0]) + GetValue(p1CombatPos[1, 0]);
        int p1CenCol = GetValue(p1CombatPos[0, 1]) + GetValue(p1CombatPos[1, 1]);
        int p1RigCol = GetValue(p1CombatPos[0, 2]) + GetValue(p1CombatPos[1, 2]);

        int p2LefCol = GetValue(p2CombatPos[0, 0]) + GetValue(p2CombatPos[1, 0]);
        int p2CenCol = GetValue(p2CombatPos[0, 1]) + GetValue(p2CombatPos[1, 1]);
        int p2RigCol = GetValue(p2CombatPos[0, 2]) + GetValue(p2CombatPos[1, 2]);

        if (p1LefCol > p2RigCol)
        {
            p1ColWins++;
        }
        else if (p2RigCol > p1LefCol)
        {
            p2ColWins++;
        }

        if (p1CenCol > p2CenCol)
        {
            p1ColWins++;
        }
        else if (p2CenCol > p1CenCol)
        {
            p2ColWins++;
        }

        if (p1RigCol > p2LefCol)
        {
            p1ColWins++;
        }
        else if (p2LefCol > p1RigCol)
        {
            p2ColWins++;
        }

        if (p1ColWins > p2ColWins)
        {
            print("Player 1 wins!");
            scoreManager.UpdateP1Score();
        }
        else if (p2ColWins > p1ColWins)
        {
            print("Player 2 wins!");
            scoreManager.UpdateP2Score();
        }
        else
        {
            print("It's a tie!");
        }

        foreach (GameObject card in p1PlayedCards)
        {
            card.transform.position = new Vector3(game.playerDeckPos.transform.position.x, game.playerDeckPos.transform.position.y, game.playerDeckPos.transform.position.z+10);
        }
        foreach (GameObject card in p2PlayedCards)
        {
            card.transform.position = new Vector3(game.computerDeckPos.transform.position.x, game.computerDeckPos.transform.position.y, game.computerDeckPos.transform.position.z);
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

        selectedCard = null;
        game.StartOfTurn();
    }

    int GetValue(GameObject card)
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
}
