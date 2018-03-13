using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessGame : MonoBehaviour
{

    public Material[] m_Colors = new Material[4];       // List of the different colors used for the guessing game
    private int m_Index = -1;                           // Used as a sentinel value to switch between colors in the array
    private bool m_Victory;                             // Used to check if the player has the right set of colors

    public List<MyColour> m_Cubes;                      // List of the guessing game's cubes

    private void Update()
    {
        if (CheckVitory())
        {
            Destroy(gameObject);
        }
    }
	
	public Material ChangeColor(Renderer aRenderer, MyColour aCol)
    {
        m_Index++;
        if (m_Index >= m_Colors.Length || m_Index == -1)
        {
            m_Index = 0;
        }

        if (m_Index == 0)
        {
            aCol.m_Colour = "Blue";
        }
        else if (m_Index == 1)
        {
            aCol.m_Colour = "Green";
        }
        else if (m_Index == 2)
        {
            aCol.m_Colour = "Yellow";
        }
        else if (m_Index == 3)
        {
            aCol.m_Colour = "Red";
        }

        return m_Colors[m_Index];
    }

    private bool CheckVitory()
    {
        m_Victory = false;
        if (gameObject.name.Contains("1"))
        {
            if (m_Cubes[0].m_Colour == "Red" &&
                m_Cubes[1].m_Colour == "Yellow" &&
                m_Cubes[2].m_Colour == "Green" &&
                m_Cubes[3].m_Colour == "Blue")
            {
                m_Victory = true;
            }

        }
        else if (gameObject.name.Contains("2"))
        {
            if (m_Cubes[0].m_Colour == "Yellow" &&
                m_Cubes[1].m_Colour == "Green" &&
                m_Cubes[2].m_Colour == "Green" &&
                m_Cubes[3].m_Colour == "Red")
            {
                m_Victory = true;
            }
        }
        else
        {
            if (m_Cubes[0].m_Colour == "Red" &&
                m_Cubes[1].m_Colour == "Blue" &&
                m_Cubes[2].m_Colour == "Green" &&
                m_Cubes[3].m_Colour == "Yellow")
            {
                m_Victory = true;
            }
        }
        return m_Victory;
    }
}
