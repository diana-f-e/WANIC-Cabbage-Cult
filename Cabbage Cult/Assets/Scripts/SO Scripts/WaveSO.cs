using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
    public string waveType;
    [Header("how to: 2 construction types - ratio and complex.")]
    [Header("- for ratio, each miniWave type should only be listed once, with the corresponding index in")]
    [Header("the chances array denoting the chance of the type spawning.")]
    [Header("- for complex, the miniWaves array will spawn a number of enemies of that type")]
    [Header("equal to the corresponding index in the chances array. count will be disregarded.")]
    public string construction;
    public int count;
    public int delay;
    public MiniWaveSO[] miniWaves;
    public float[] chances;
}
