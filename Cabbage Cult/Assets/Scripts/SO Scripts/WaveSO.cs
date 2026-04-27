/****************************************************************************
* File Name: WaveSO.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: A scriptable object made using this file provides the values
*              for a wave type.
*
****************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
    public MiniWaveSO[] miniWaves;

    [TextArea]
    public string note;
}
