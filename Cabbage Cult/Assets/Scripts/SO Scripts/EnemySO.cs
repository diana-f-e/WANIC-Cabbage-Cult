using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int enemyDamage;
    public int enemyHealth;
    public float enemySpeed;
    public int helmetHealth;
    public AudioResource onHurt;
    public Sprite skin;
    public RuntimeAnimatorController runtimeAnimator;
}
