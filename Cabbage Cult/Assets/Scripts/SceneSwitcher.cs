using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to be used by buttons for switching scenes
    public void goToScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
