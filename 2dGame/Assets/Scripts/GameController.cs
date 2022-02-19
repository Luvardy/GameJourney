using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;

    public static GameController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowWinPanel()
    {
        gameWinPanel.SetActive(true);
    }

    public void RestartLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
