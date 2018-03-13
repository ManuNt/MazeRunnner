using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject m_FinalPanelPrefab;   // The actual end game panel

    private void OnTriggerEnter(Collider aCol)
    {
        if (aCol.tag == "Player")
        {
            GameObject finalPanel = Instantiate(m_FinalPanelPrefab);
        }
    }

}
