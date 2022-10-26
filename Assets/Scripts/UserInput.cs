using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UserInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            print(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                // What has been hit?
                if (hit.collider.CompareTag("PlayerDeck"))
                {
                    // Clicked deck
                    PlayerDeck();
                }
                else if (hit.collider.CompareTag("PlayerHand"))
                {
                    // Clicked card in hand
                    PlayerHand();
                }
                else if (hit.collider.CompareTag("PlayerCombat"))
                {
                    // Clicked place to put card
                    PlayerCombat();
                }
                else if (hit.collider.CompareTag("PlayerCard"))
                {
                    // Clicked place to put card
                    PlayerCard();
                }
            }
        }
    }

    void PlayerDeck()
    {
        //print("Clicked on deck");
        Debug.Log("deck");
    }

    void PlayerHand()
    {
        print("Clicked on hand");
    }

    void PlayerCombat()
    {
        print("Clicked on arena");
    }

    void PlayerCard()
    {
        print("Clicked on card");
    }
}
