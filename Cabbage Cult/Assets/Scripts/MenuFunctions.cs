/****************************************************************************
* File Name: MenuFunctions.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: This file describes functions to be used by menu buttons.
*
****************************************************************************/
using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
    public Texture2D cursorOpen;
    public Texture2D cursorClosed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(cursorOpen, new Vector2(16, 16), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        //show cursor clicks
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Cursor.SetCursor(cursorClosed, new Vector2(16, 16), CursorMode.Auto);
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            Cursor.SetCursor(cursorOpen, new Vector2(16, 16), CursorMode.Auto);
        }
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
