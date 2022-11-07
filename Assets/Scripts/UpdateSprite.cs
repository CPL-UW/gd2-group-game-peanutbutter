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
                transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                cardFace = game.p1CardFaces[i];
                break;
            }
            i++;
        }
        i = 0;
        foreach (string card in computerDeck)
        {
            if (this.name == card)
            {
                transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                cardFace = game.p2CardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
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
                spriteRenderer.color = Color.white;
            }
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
