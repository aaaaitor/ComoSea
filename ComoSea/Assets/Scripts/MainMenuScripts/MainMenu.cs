using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame ()
    {
        // Loads the next scene in the queue
        SceneManager.LoadScene("Game");
    }   

    public void QuitGame ()
    {
        // Quits the game
        Application.Quit();
    }
}
