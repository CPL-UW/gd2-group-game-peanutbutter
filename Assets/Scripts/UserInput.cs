using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UserInput : MonoBehaviour
{
    public GameObject selectedCard;
    
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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            // print(mousePosition);
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
                    PlayerCombat(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("PlayerCard"))
                {
                    // Clicked place to put card
                    PlayerCard(hit.collider.gameObject);
                }
            }
        }
    }

    void PlayerDeck()
    {
        //print("Clicked on deck");
        // Debug.Log("deck");
    }

    void PlayerHand()
    {
        // print("Clicked on hand");
    }

    void PlayerCombat(GameObject selected)
    {
        // print("Clicked on arena");
        if (selectedCard)
        {
            selectedCard.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y, selected.transform.position.z - 1);
        }
        selectedCard = null;
    }

    void PlayerCard(GameObject selected)
    {
        // print("Clicked on card");

        selectedCard = selected;
    }
}
