using System.Collections;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Graph graphPrefab;
    private Graph graphInstance;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        graphInstance = Instantiate(graphPrefab) as Graph;
        graphInstance.Generate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          Application.Quit();
        }
    }
}
