using UnityEngine;

[CreateAssetMenu(fileName = "MiniWaveSO", menuName = "Scriptable Objects/MiniWaveSO")]
public class MiniWaveSO : ScriptableObject
{
    //different implementations: 
    [Header("how to: 2 construction types - ratio and complex.")]
    [Header("for ratio, each type should only be listed once, with the corresponding index in")]
    [Header("the chances array denoting the chance of the type spawning.")]
    [Header("for complex, the chances array can be thought of as a count array,")]
    [Header("where ")]
    public string construction;
    public int count;
    public int delay;
    public EnemySO[] types;
    public float[] chances;
}
