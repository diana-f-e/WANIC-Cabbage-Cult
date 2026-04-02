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
