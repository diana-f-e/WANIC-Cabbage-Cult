using UnityEngine;

[CreateAssetMenu(fileName = "CurseSO", menuName = "Scriptable Objects/CurseSO")]
public class CurseSO : ScriptableObject
{
    public string curseName;
    public Color curseColor;

    [Header("each variable below is a modifier to the given tower attribute")]
    [Header("ex. setting damage to 0.5 will multiple damage by 0.5, so tower does half damage")]
    [Header("0 is not a valid multiplier, so any multipliers set to 0 will be skipped")]
    public float cooldown;
    public float damage;
    public float attackRadius;
    public float effectNum;
    public float effectCooldown;

}

