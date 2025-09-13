using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{

    public void ResetScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
