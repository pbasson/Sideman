using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ScoreGame : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text winText;
    public static GameObject[] collect;
    public AudioClip winSF;
    public AudioSource backgroundSF;

    private int collInt;
    private int collLeft = 1;


    void Start()
    {
        backgroundSF.Play();
        winText.enabled = false;
        scoreText.enabled = false;
        collect = GameObject.FindGameObjectsWithTag("Collectable");
        collInt = collect.Length;
        StartCoroutine(textDelay());

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
      if (collider.gameObject.CompareTag("Collectable"))
      {
        if (collInt != score)
        {
          score += 1;
          SetText();
        }
      }
    }

    IEnumerator MenuDelay()
    {
        yield return new WaitForSeconds(2f);
        Menu.MenuLoad();
    }


    IEnumerator textDelay()
    {
        yield return new WaitForEndOfFrame();
        scoreText.enabled = true;
        SetText();
    }

    void SetText()
    {
      collLeft = collInt - score;
      if (collLeft > 0)
      {
        scoreText.text = "Score: " + score.ToString()+  " - Total: " + collLeft.ToString();
      }
      else {
        StartCoroutine(MenuDelay());
        scoreText.enabled = false;
        backgroundSF.Stop();
        winText.enabled = true;
        winText.text = "You Win";
        AudioSource.PlayClipAtPoint(winSF, transform.position);
        GetComponent<PlayerController>().enabled = false;
      }
  }
}
