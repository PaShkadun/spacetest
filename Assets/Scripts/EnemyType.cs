using System;
using UnityEngine;

[Serializable]
public class EnemyType
{
    [SerializeField] private EnemyPlayer _enemy;
    [SerializeField] private float _chance;

    public EnemyPlayer Enemy => _enemy;
    public float Chance => _chance;
}
