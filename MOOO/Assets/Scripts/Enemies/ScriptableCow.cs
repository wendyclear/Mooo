using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ScriptableCow", order = 1)]
public class ScriptableCow : ScriptableObject
{
    [SerializeField]
    public string _prefabName;
    [SerializeField]
    public int _numberOfCows;
    [SerializeField]
    public Vector3[] _spawnPoints;
}