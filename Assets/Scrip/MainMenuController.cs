using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
	// Start is called before the first frame update
	private void Start()
	{
		AudioManager.instance.Play("Menu");
	}
	public void Play()
    {
        SceneManager.LoadScene("SampleScene");
        
    }
}
