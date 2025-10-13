using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUII : MonoBehaviour
{
    [SerializeField] private GameControl gameControl;
    public void Startgame()
    {
        gameControl.StartGame();
    }
    public void QuitGame()
    {
       
        Application.Quit();
    }
    public void ContinueGame()
    {
       
        gameControl.ResumeGame();
    }

    public void MainMenu()
    {
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
} 
