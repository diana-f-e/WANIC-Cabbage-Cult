/****************************************************************************
* File Name: PlaytestingSO.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: A scriptable object made using this file provides the values
*              for a gameManager.
*
****************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "PlaytestingSO", menuName = "Scriptable Objects/PlaytestingSO")]
public class PlaytestingSO : ScriptableObject
{
    public int money;
    public int health;
    public int[] taxes;

    public float enemySpawnCooldown;
    public int enemiesToSpawn;

    public int moneyPerRound;


}
