using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveGameObject(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
