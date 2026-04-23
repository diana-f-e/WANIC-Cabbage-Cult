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
    public AudioClip onAttack;
    public AudioClip onPlace;
    public AudioClip onBuy;
    public AudioResource audioResourceTest;

    public Sprite skin;
    public RuntimeAnimatorController runtimeAnimator;

    public float effectNum;
    public float effectCooldown;

    public string description;
}
