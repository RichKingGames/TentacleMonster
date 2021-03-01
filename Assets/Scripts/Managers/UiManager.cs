using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public GameObject CompletePanel;
    public GameObject GameOverPanel;

    public void EnableCompletePanel()
    {
        CompletePanel.SetActive(true);
    }
    public void EnableGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }
    public void NextLevel()
    {
        Application.Quit(); // Temporarily
    }
}
