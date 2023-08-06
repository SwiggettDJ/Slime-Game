using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManagerBehaviour : MonoBehaviour
{
    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
    public void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public void LoadLevel1()
    {
        ResumeGame();
        SceneManager.LoadScene("Level 1");
    }
    
}
