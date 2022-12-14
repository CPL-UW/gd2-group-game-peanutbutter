using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite_SP : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private Game_SP game;
    private UserInput_SP userInput;
    private ToggleCamera toggleCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        List<string> playerDeck = Game_SP.GenerateDeck();
        List<string> computerDeck = Game_SP.GenerateDeck(false);
        game = FindObjectOfType<Game_SP>();
        userInput = FindObjectOfType<UserInput_SP>();
        Vector3 scale = new Vector3(0.25f, 0.25f, 1.0f);

        int i = 0;
        foreach (string card in playerDeck)
        {
            if (this.name == card)
            {
                transform.localScale = scale;
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
                transform.localScale = scale;
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
