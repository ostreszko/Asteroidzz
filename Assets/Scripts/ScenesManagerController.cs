using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagerController : MonoBehaviour
{
    public void GoToNextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevelByBuildIndex(int levelBuildindex)
    {
        SceneManager.LoadScene(levelBuildindex);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
