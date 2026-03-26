using UnityEngine;

[CreateAssetMenu(fileName = "PlaytestingSO", menuName = "Scriptable Objects/PlaytestingSO")]
public class PlaytestingSO : ScriptableObject
{
    public int money;
    public int health;
    public int tax;

    public float enemySpawnCooldown;
    public int enemiesToSpawn;

    public int moneyPerRound;


}
