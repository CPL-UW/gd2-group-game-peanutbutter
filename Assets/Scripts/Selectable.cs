using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool faceUp = false;
    public string suit;
    public int value;

    private string valueString;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set suit and values of player cards
        if (CompareTag("PlayerCard"))
        {
            suit = transform.name[0].ToString();

            for (int i = 1; i < transform.name.Length; i++)
            {
                char c = transform.name[i];
                valueString = valueString + c.ToString();
            }

            if (valueString == "A")
            {
                value = 1;
            }
            if (valueString == "1")
            {
                value = 2;
            }
            if (valueString == "2")
            {
                value = 3;
            }
            if (valueString == "3")
            {
                value = 4;
            }
            if (valueString == "4")
            {
                value = 5;
            }
            if (valueString == "5")
            {
                value = 6;
            }
            if (valueString == "6")
            {
                value = 7;
            }
            if (valueString == "7")
            {
                value = 8;
            }
            if (valueString == "8")
            {
                value = 9;
            }
            if (valueString == "9")
            {
                value = 10;
            }
            if (valueString == "J")
            {
                value = 11;
            }
            if (valueString == "Q")
            {
                value = 12;
            }
            if (valueString == "K")
            {
                value = 13;
            }
            if (valueString == "O")
            {
                value = 14;
            }
            
        }

        // Set suit and values of computer cards
        if (CompareTag("ComputerCard"))
        {
            suit = transform.name[1].ToString();
            valueString = valueString + transform.name[0].ToString();

            if (valueString == "A")
            {
                value = 1;
            }
            if (valueString == "1")
            {
                value = 2;
            }
            if (valueString == "2")
            {
                value = 3;
            }
            if (valueString == "3")
            {
                value = 4;
            }
            if (valueString == "4")
            {
                value = 5;
            }
            if (valueString == "5")
            {
                value = 6;
            }
            if (valueString == "6")
            {
                value = 7;
            }
            if (valueString == "7")
            {
                value = 8;
            }
            if (valueString == "8")
            {
                value = 9;
            }
            if (valueString == "9")
            {
                value = 10;
            }
            if (valueString == "J")
            {
                value = 11;
            }
            if (valueString == "Q")
            {
                value = 12;
            }
            if (valueString == "K")
            {
                value = 13;
            }
            if (valueString == "O")
            {
                value = 14;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
