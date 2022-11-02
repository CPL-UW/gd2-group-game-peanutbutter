using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private Game game;
    private UserInput userInput;
    private Color c;
    private ToggleCamera toggleCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        List<string> playerDeck = Game.GenerateDeck();
        List<string> computerDeck = Game.GenerateDeck(false);
        game = FindObjectOfType<Game>();
        userInput = FindObjectOfType<UserInput>();

        int i = 0;
        foreach (string card in playerDeck)
        {
            if (this.name == card)
            {
                cardFace = game.cardFaces[i];
                break;
            }
            i++;
        }
        i = 0;
        foreach (string card in computerDeck)
        {
            if (this.name == card)
            {
                cardFace = game.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
        c = spriteRenderer.color;
        toggleCamera = GetComponent<ToggleCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectable.faceUp == true)
        {
            spriteRenderer.sprite = cardFace;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
        if (userInput.selectedCard)
        {
            if (name == userInput.selectedCard.name)
            {
                spriteRenderer.color = Color.yellow;
            }
            else
            {
                spriteRenderer.color = c;
            }
        }
        
    }
}