/****************************************************************************
* File Name: SoundSO.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: A scriptable object made using this file provides the values
*              for the music themes in the game.
*
****************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSO", menuName = "Scriptable Objects/SoundSO")]
public class SoundSO : ScriptableObject
{
    [Header("Sounds in other SOs: Tower onAttack/onPlace/onBuy, Enemy onHurt/onDeath")]
    //TODO impelement
    public AudioClip mainMusic;
    public AudioClip startMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
}
