using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int enemyDamage;
    public int enemyHealth;
    public float enemySpeed;
    public int helmetHealth;
    public AudioClip onHurt;
    public AudioClip onDeath;
    public Sprite skin;
}
