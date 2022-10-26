using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject playerCardPrefab;
    public GameObject computerCardPrefab;
    public GameObject playerDeckPos;
    public GameObject computerDeckPos;
    public GameObject playerHandPos;
    public GameObject computerHandPos;

    public static string[] suits = new string[] {"C","D","H","S"};
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

    public List<string> playerDeck;
    public List<string> computerDeck;

    public List<string> playerHand;
    public List<string> computerHand;

    public const int handSize = 6;

    // Start is called before the first frame update
    void Start()
    {
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCards()
    {
        playerDeck = GenerateDeck();
        Shuffle(playerDeck);
        computerDeck = GenerateDeck();
        Shuffle(computerDeck);

        // Test the cards in the deck
        foreach (string card in playerDeck)
        {
            //print(card);
        }
        GameSort(playerDeck, playerHand);
        Deal(playerDeck, playerDeckPos, playerCardPrefab, playerHand, playerHandPos);
        GameSort(computerDeck, computerHand);
        Deal(computerDeck, computerDeckPos, computerCardPrefab, computerHand, computerHandPos);
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
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

    void Deal(List<string> deck, GameObject deckPos, GameObject cardPrefab, List<string> hand, GameObject handPos)
    {
        float zOffset = 0.03f;
        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(
                                        cardPrefab,
                                        new Vector3(deckPos.transform.position.x, deckPos.transform.position.y, deckPos.transform.position.z - zOffset),
                                        Quaternion.identity,
                                        deckPos.transform
                                  );
            newCard.name = card;
            zOffset += 0.03f;
        }

        float xOffset = 1.15f;
        foreach (string card in hand)
        {
            GameObject newCard = Instantiate(
                                        cardPrefab,
                                        new Vector3(handPos.transform.position.x - xOffset, handPos.transform.position.y, handPos.transform.position.z),
                                        Quaternion.identity,
                                        handPos.transform
                                  );
            newCard.name = card;
            newCard.GetComponent<Selectable>().faceUp = true;
            xOffset += 1.15f;
        }
    }

    void GameSort(List<string> deck, List<string> hand)
    {
        for (int i = hand.Count; i < handSize; i++)
        {
            hand.Add(deck.Last<string>());
            deck.RemoveAt(deck.Count - 1);
        }
    }
}
