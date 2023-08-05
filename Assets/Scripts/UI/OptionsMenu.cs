using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public GameObject volSlider;
    public GameObject bloodToggle;

	public Image startBtn;
    public Image optionsBtn;
    public Image quitBtn;

	public GameObject volLabel;
	public GameObject bloodLabel;

	public Image backBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShowOptions()
    {
        volSlider.SetActive(true);
        bloodToggle.SetActive(true);
		startBtn.gameObject.SetActive(false);
		optionsBtn.gameObject.SetActive(false);
		quitBtn.gameObject.SetActive(false);
		volLabel.SetActive(true);
		bloodLabel.SetActive(true);
		backBtn.gameObject.SetActive(true);
    }

	public void BackToMain()
    {
        volSlider.SetActive(false);
        bloodToggle.SetActive(false);
		startBtn.gameObject.SetActive(true);
		optionsBtn.gameObject.SetActive(true);
		quitBtn.gameObject.SetActive(true);
		volLabel.SetActive(false);
		bloodLabel.SetActive(false);
		backBtn.gameObject.SetActive(false);
    }

	public void loadGame()
    {
        SceneManager.LoadScene("Locomotion");
    }

	public void endGame()
	{
		Application.Quit();
	}
}
