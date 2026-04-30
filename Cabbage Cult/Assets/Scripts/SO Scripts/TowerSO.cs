/****************************************************************************
* File Name: TowerSO.cs
* Author: Diana Everman
* DigiPen Email: diana.everman@digipen.edu
* Course: Video Game Programming 1
*
* Description: A scriptable object made using this file provides the values
*              for a tower type.
*
****************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "TowerSO", menuName = "Scriptable Objects/TowerSO")]
public class TowerSO : ScriptableObject
{
    public string towerType;
    public int towerLevel;
    public float cooldown;
    public int damage;
    public float attackRadius;
    public float placeRadius;
    public int towerCost;
    public Color towerColor;
    public string effect;
    //public AudioClip onAttack;
    public AudioClip onPlace;
    //public AudioClip onBuy;
    public AudioResource audioResourceTest;

    public Sprite skin;
    public Sprite iconSkin;
    public RuntimeAnimatorController runtimeAnimator;

    public float effectNum;
    public float effectCooldown;

    public string description;
}
