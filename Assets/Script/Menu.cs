using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Button startButton;
    public Button optionButton;
    public Button quitButton;
		public Text start;
		public Text options;
    public Text quit;
    public AudioClip ButtonSF;
    public AudioSource menuSF;
    public Text title;
    public Text nameMy;

    private Text loseText;
    private int sizeCount = 1;


    void Awake()
    {
        title.text = "SideMan";
        nameMy.text = "By Preetpal Basson";
        startButton.name = "Start Game";
        optionButton.name = "Options";
        quitButton.name = "Quit Game";
        quit.text = "Quit";
        start.text = "Start";
        options.text = "Options";
        menuSF.Play();
        Debug.Log("Test " + ScoreGame.collect);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
    }




    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(ButtonSF,transform.position);
		    Debug.Log("Quit");
		    Application.Quit();}

    public void MenuLoader()
    {
        SceneManager.LoadScene("scene1");
    }


    public static void MenuLoad()
    {
      SceneManager.LoadScene("Menu");
    }

}
