using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    public void Quit() {
        Debug.Log("Application quit");
        Application.Quit();
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
