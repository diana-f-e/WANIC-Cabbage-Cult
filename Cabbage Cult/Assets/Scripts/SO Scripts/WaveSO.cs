using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
    public MiniWaveSO[] miniWaves;

    [TextArea]
    public string note;
}
