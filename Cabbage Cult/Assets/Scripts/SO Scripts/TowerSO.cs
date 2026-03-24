using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerSO", menuName = "Scriptable Objects/TowerSO")]
public class TowerSO : ScriptableObject
{
    public string towerType;
    public float cooldown;
    public int damage;
    public float attackRadius;
    public int towerCost;
}
