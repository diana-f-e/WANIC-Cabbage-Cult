using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerSO", menuName = "Scriptable Objects/TowerSO")]
public class TowerSO : ScriptableObject
{
    public string towerType;
    public int towerLevel;
    public float cooldown;
    public int damage;
    public float attackRadius;
    public int towerCost;
    public Color towerColor;
    public string effect;
}
