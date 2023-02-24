using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
   
    public static bool IsPaused = false;
    private Text txtLife;

    public GameObject pauseMenuUI;
    private void Start()
    {
        txtLife = GameObject.Find("LifeTxt").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPaused) { Resume(); }
            else { Pause(); }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused= false;
    }
    public void Pause()
    {
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		IsPaused = true;
	}
    public void NewGame()
    {
		Time.timeScale = 1f;
        txtLife.text = "0";
        SceneManager.LoadScene("SampleScene");
    }
	public void Quit()
	{

        SceneManager.LoadSceneAsync("MainMenu");
	}
}
