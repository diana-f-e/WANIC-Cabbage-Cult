using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DaveEnding : MonoBehaviour
{
    public GameObject[] daveClues;
    private int index = 1;
    public Sprite normSprite;
    public Sprite daveSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RestartDave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AdvanceDave()
    {
        if (index >= daveClues.Length)
        {
            gameObject.transform.position = new Vector3(440, 240);
            return;
        }
        daveClues[index - 1].tag = "Untagged";
        daveClues[index - 1].GetComponent<SpriteRenderer>().sprite = normSprite;
        daveClues[index].tag = "Dave";
        daveClues[index].GetComponent<SpriteRenderer>().sprite = daveSprite;
        index += 1;
    }

    public void RestartDave()
    {
        gameObject.transform.position = new Vector3(0,10000);
        foreach (GameObject clue in daveClues)
        {
            clue.tag = "Untagged";
            clue.GetComponent<SpriteRenderer>().sprite = normSprite;
        }
        daveClues[0].tag = "Dave";
        daveClues[0].GetComponent<SpriteRenderer>().sprite = daveSprite;
        index = 1;
    }

    public void TriggerDave()
    {
        SceneManager.LoadScene("DaveScene");
    }
}
