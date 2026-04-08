using UnityEngine;
using UnityEngine.Rendering;

public class Tutorial : MonoBehaviour
{
    public GameObject[] steps;
    private int index = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdvanceTutorial();
        }
    }

    public void AdvanceTutorial()
    {
        if(index >= steps.Length)
        {
            gameObject.SetActive(false);
            return;
        }
        steps[index - 1].SetActive(false);
        steps[index].SetActive(true);
        index += 1;
    }
}
