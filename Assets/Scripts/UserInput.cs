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

    public static bool isP1Turn = true;
    private int[,] p1CombatPos = new int[2,3];
    private int[,] p2CombatPos = new int[2,3];

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
        toggleCamera = FindObjectOfType<ToggleCamera>();
        scoreManager = FindObjectOfType<ScoreManager>();
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
            selectedCard.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y, selected.transform.position.z - 1);
        }
        p1CombatPos[row, col] = selectedCard.GetComponent<Selectable>().value;
        selectedCard = null;
    }

    void PlayerCard(GameObject selected)
    {
        selectedCard = selected;
    }
    
    void P2Combat(GameObject selected, int row, int col)
    {
        if (selectedCard)
        {
            selectedCard.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y, selected.transform.position.z - 1);
        }
        p2CombatPos[row, col] = selectedCard.GetComponent<Selectable>().value;
        selectedCard = null;
    }

    void ComputerCard(GameObject selected)
    {
        selectedCard = selected;
    }

    public void ChangeTurn()
    {
        if (isP1Turn)
        {
            toggleCamera.ToggleP2Camera();
        }
        else
        {
            toggleCamera.ToggleP1Camera();
            EndTurn();
        }
        isP1Turn = !isP1Turn;
        selectedCard = null;
    }
    
    public void EndTurn()
    {
        int p1ColWins = 0;
        int p2ColWins = 0;

        // Calculate who wins round
        int p1LefCol = p1CombatPos[0, 0] + p1CombatPos[1, 0];
        int p1CenCol = p1CombatPos[0, 1] + p1CombatPos[1, 1];
        int p1RigCol = p1CombatPos[0, 2] + p1CombatPos[1, 2];

        int p2LefCol = p2CombatPos[0, 0] + p2CombatPos[1, 0];
        int p2CenCol = p2CombatPos[0, 1] + p2CombatPos[1, 1];
        int p2RigCol = p2CombatPos[0, 2] + p2CombatPos[1, 2];

        if (p1LefCol > p2LefCol)
        {
            p1ColWins++;
        }
        else if (p2LefCol > p1LefCol)
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

        if (p1RigCol > p2RigCol)
        {
            p1ColWins++;
        }
        else if (p2RigCol > p1RigCol)
        {
            p2ColWins++;
        }

        if (p1ColWins > p2ColWins)
        {
            print("Player 1 wins!");
            scoreManager.p1Score++;
        }
        else if (p2ColWins > p1ColWins)
        {
            print("Player 2 wins!");
            scoreManager.p2Score++;
        }
        else
        {
            print("It's a tie!");
        }
    }
}
