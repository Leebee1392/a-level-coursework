using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadStoryScene1()
    {
        SceneManager.LoadScene(6);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(7);
    }

    public void LoadStoryScene2()
    {
        SceneManager.LoadScene(8);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(9);
    }

    public void LoadStoryScene3()
    {
        SceneManager.LoadScene(10);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene(11);
    }

    public void LoadStoryScene4()
    {
        SceneManager.LoadScene(12);
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene(13);
    }

    public void LoadStoryScene5()
    {
        SceneManager.LoadScene(14);
    }

    public void LoadLevel5()
    {
        SceneManager.LoadScene(15);
    }

    public void LoadStoryScreen6()
    {
        SceneManager.LoadScene(16);
    }

    public void LoadClassicScreen()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadEndlessRunnerScreen()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

