using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool faceUp = false;
    public string suit;
    public int value;
    public string rule;

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
                rule += "+1 Value";
                rule += " & Beats Face Cards Placed in Front Row";
            }
            if (valueString == "1")
            {
                value = 2;
                rule += "+2 Value";
            }
            if (valueString == "2")
            {
                value = 3;
                rule += "+3 Value";
            }
            if (valueString == "3")
            {
                value = 4;
                rule += "+4 Value";
            }
            if (valueString == "4")
            {
                value = 5;
                rule += "+5 Value";
            }
            if (valueString == "5")
            {
                value = 6;
                rule += "+6 Value";
            }
            if (valueString == "6")
            {
                value = 7;
                rule += "+7 Value";
            }
            if (valueString == "7")
            {
                value = 8;
                rule += "+8 Value";
            }
            if (valueString == "8")
            {
                value = 9;
                rule += "+9 Value";
            }
            if (valueString == "9")
            {
                value = 10;
                rule += "+10 Value";
            }
            if (valueString == "J")
            {
                value = 11;
                rule += "+11 Value";
                rule += " & is Face Card";
                rule += " & is Half Value in Back Row";
            }
            if (valueString == "Q")
            {
                value = 12;
                rule += "+12 Value";
                rule += " & is Face Card";
                rule += " & is Half Value in Back Row";
            }
            if (valueString == "K")
            {
                value = 13;
                rule += "+13 Value";
                rule += " & is Face Card";
                rule += " & is Half Value in Back Row";
            }
            if (valueString == "O")
            {
                value = 14;
                rule += "+0 Value";
                rule += " & if Red, Beats Opposing Odd Column, else loses";
                rule += " & if Black, Beats Opposing Even Column, else loses";
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
