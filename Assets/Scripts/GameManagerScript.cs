using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart() 
    {
        SceneManager.LoadScene("Scene_MazeGeneration");   
    }

    public void mainmenu() 
    {
        SceneManager.LoadScene("MainMenu");   
    }

}
