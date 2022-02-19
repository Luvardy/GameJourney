using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    bool isStop = true;
    public GameObject Menu;

    public void OnQuit()
    {
        Application.Quit();                   //退出游戏
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        Menu.SetActive(false);               //返回游戏
    }

    public void GameMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");      //返回Main Menu
    }


    // Update is called once per frame
    void Update()
    {
        if (isStop == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))    //ESC输入暂停
            {
                Time.timeScale = 0;
                isStop = false;
                Menu.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                isStop = true;
                Menu.SetActive(false);
            }
        }
    }
}