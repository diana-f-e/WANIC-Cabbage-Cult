/****************************************************************************
* File Name: DaveEnding.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: This file defines functions for the secret dave ending.
*
****************************************************************************/
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DaveEnding : MonoBehaviour
{
    public GameObject[] daveClues;
    private int index = 1;
    public Sprite normSprite;
    public Sprite daveSprite;
    public Sprite daveSpriteNoLeftFish;
    public RectTransform myRectTf;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RestartDave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //set the next barrel to have fish
    public void AdvanceDave()
    {
        if (index >= daveClues.Length)
        {
            myRectTf.anchoredPosition = new Vector2(0, 0);
            return;
        }
        daveClues[index - 1].tag = "Untagged";
        daveClues[index - 1].GetComponent<SpriteRenderer>().sprite = normSprite;
        daveClues[index].tag = "Dave";
        daveClues[index].GetComponent<SpriteRenderer>().sprite = daveSprite;
        //for second barrel, fish clips through wall, so use sprite w it removed
        if(index == 1)
        {
            daveClues[index].GetComponent<SpriteRenderer>().sprite = daveSpriteNoLeftFish;
        }
        index += 1;
    }

    //restart the sequence
    public void RestartDave()
    {
        myRectTf.anchoredPosition = new Vector2(0, 10000);
        foreach (GameObject clue in daveClues)
        {
            clue.tag = "Untagged";
            clue.GetComponent<SpriteRenderer>().sprite = normSprite;
        }
        daveClues[0].tag = "Dave";
        daveClues[0].GetComponent<SpriteRenderer>().sprite = daveSprite;
        index = 1;
    }

    //switch to the dave ending scene
    public void TriggerDave()
    {
        SceneManager.LoadScene("DaveScene");
    }
}
