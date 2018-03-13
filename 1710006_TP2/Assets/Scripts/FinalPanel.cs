using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPanel : MonoBehaviour
{
	public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
