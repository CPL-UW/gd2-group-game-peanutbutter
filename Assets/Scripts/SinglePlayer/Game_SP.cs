using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Game_SP : MonoBehaviour
{
    public Sprite[] p1CardFaces;
    public Sprite[] p2CardFaces;
    public GameObject playerCardPrefab;
    public GameObject computerCardPrefab;
    public GameObject playerDeckPos;
    public GameObject computerDeckPos;
    public GameObject playerHandPos;
    public GameObject computerHandPos;

    public GameObject p1DiscardPos;
    public GameObject p2DiscardPos;

    public ScoreManager scoreManager;

    public static string[] suits = new string[] {"C","D","H","S"};
    public static string[] values = new string[] { "A", "1", "2", "3", "4", "5", "6", "7", "8", "9", "J", "Q", "K" };

    public List<string> playerDeck;
    public List<string> computerDeck;

    public List<string> playerHand;
    public List<string> computerHand;

    public List<GameObject> p1HandCards;
    public List<GameObject> p2HandCards;

    public List<string> p1Discard;
    public List<string> p2Discard;

    public Sprite p1Portal;
    public Sprite p1PortalSelected;
    public Sprite p2Portal;
    public Sprite p2PortalSelected;

    public GameObject p1TopLeft;
    public GameObject p1BotLeft;
    public GameObject p1TopCent;
    public GameObject p1BotCent;
    public GameObject p1TopRigh;
    public GameObject p1BotRigh;

    public GameObject p2TopLeft;
    public GameObject p2BotLeft;
    public GameObject p2TopCent;
    public GameObject p2BotCent;
    public GameObject p2TopRigh;
    public GameObject p2BotRigh;

    public const int handSize = 6;

    public TextMeshProUGUI gameWinText;

    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        p1HandCards = new List<GameObject>();
        p2HandCards = new List<GameObject>();

        scoreManager = FindObjectOfType<ScoreManager>();
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreManager.p1Score >= 7)
        {
            print("Player 1 Wins the Game!!!");
            gameWinText.text = "Player 1 Wins the Game!!!";
            scoreManager.ResetScore();
            RestartGame();
        }
        else if (scoreManager.p2Score >= 7)
        {
            print("Player 2 Wins the Game!!!");
            gameWinText.text = "Player 2 Wins the Game!!!";
            scoreManager.ResetScore();
            RestartGame();
        }
        if (playerDeck.Count < 5)
        {
            foreach (string card in p1Discard)
            {
                playerDeck.Add(card);
            }
        }
        if (computerDeck.Count < 5)
        {
            foreach (string card in p2Discard)
            {
                playerDeck.Add(card);
            }
        }
    }

    public void RestartGame()
    {
        p1HandCards.Clear();
        p2HandCards.Clear();
        PlayCards();
    }

    public void PlayCards()
    {
        source.PlayOneShot(clip);
        // Player methods
        playerDeck = GenerateDeck();
        Shuffle(playerDeck);
        GameSort(playerDeck, playerHand);
        Deal(playerDeck, playerDeckPos, playerCardPrefab, playerHand, playerHandPos, Quaternion.identity);

        // Computer methods
        computerDeck = GenerateDeck(false);
        Shuffle(computerDeck);
        GameSort(computerDeck, computerHand, false);
        Deal(computerDeck, computerDeckPos, computerCardPrefab, computerHand, computerHandPos, Quaternion.Euler(new Vector3(0, 0, 180)), false);

        // StartOfTurn();
    }

    public void StartOfTurn()
    {
        GameSort(playerDeck, playerHand);
        DealHand(playerCardPrefab, playerHand, playerHandPos, Quaternion.identity);
        GameSort(computerDeck, computerHand, false);
        DealHand(computerCardPrefab, computerHand, computerHandPos, Quaternion.Euler(new Vector3(0,0,180)), false);
    }

    public static List<string> GenerateDeck(bool isP1 = true)
    {
        List<string> newDeck = new List<string>();
        
        // Add standard cards to each deck
        foreach (string s in suits)
        {
            if (isP1)
            {
                foreach (string v in values)
                {
                    newDeck.Add(s + v);
                }
            }
            else
            {
                foreach (string v in values)
                {
                    newDeck.Add(v + s);
                }
            }
            
        }

        // Add jokers to each deck
        if (isP1)
        {
            newDeck.Add("RO");
            newDeck.Add("BO");
        }
        else
        {
            newDeck.Add("OR");
            newDeck.Add("OB");
        }

        return newDeck;
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random rand = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = rand.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void Deal(List<string> deck, GameObject deckPos, GameObject cardPrefab, List<string> hand, GameObject handPos, Quaternion rotation, bool isP1 = true)
    {
        float zOffset = 0.03f;
        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(
                                        cardPrefab,
                                        new Vector3(deckPos.transform.position.x, deckPos.transform.position.y, deckPos.transform.position.z - zOffset),
                                        rotation,
                                        deckPos.transform
                                  );
            newCard.name = card;
            zOffset += 0.03f;
        }

        DealHand(cardPrefab, hand, handPos, rotation, isP1);
    }

    void DealHand(GameObject cardPrefab, List<string> hand, GameObject handPos, Quaternion rotation, bool isP1 = true)
    {
        float xOffset = 1.05f;
        float zOffset = 0.03f;
        foreach (string card in hand)
        {
            GameObject newCard = Instantiate(
                                        cardPrefab,
                                        new Vector3(handPos.transform.position.x - xOffset, handPos.transform.position.y, handPos.transform.position.z - zOffset),
                                        rotation,
                                        handPos.transform
                                  );
            newCard.name = card;
            if (isP1)
            {
                newCard.GetComponent<Selectable>().faceUp = true;
            }
            if (isP1)
            {
                p1HandCards.Add(newCard);
            }
            else
            {
                p2HandCards.Add(newCard);
            }
            xOffset += 1.42f;
            zOffset += 0.03f;
        }
    }

    void GameSort(List<string> deck, List<string> hand, bool isP1 = true)
    {
        for (int i = hand.Count; i < handSize; i++)
        {
            hand.Add(deck.Last<string>());
            deck.Remove(deck.Last<string>());
        }
    }
}
