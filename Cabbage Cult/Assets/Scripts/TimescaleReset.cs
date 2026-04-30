/****************************************************************************
* File Name: TimescaleReset.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: This file is to be attached to an empty object to reset the
*               time scale to 1 then destroy the object.
*
****************************************************************************/
using UnityEngine;

public class TimescaleReset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
