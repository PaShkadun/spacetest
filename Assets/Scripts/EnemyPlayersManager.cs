using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class EnemyPlayersManager : MonoBehaviour
{
    [SerializeField] private Transform _enemyStartPosition;
    [SerializeField] private List<Transform> _enemiesPositions;
    [SerializeField] private Transform _enemyEndPosition;
    [SerializeField] private List<EnemyType> enemies;
    
    private List<EnemyPlayer> _enemyInstances = new();

    private CancellationTokenSource _cts = new CancellationTokenSource();
    private float _moveTime = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        InitEnemies();
    }

    private async UniTask InitEnemies()
    {
        GenerateEnemies();
        await MoveEnemies();
        SetImpregnable();
        StartAttack();
        WaitWaveEnd();
    }

    public void StartAttack()
    {
        foreach (var enemy in _enemyInstances)
        {
            enemy.StartAttack(_cts.Token);
        }
    }

    public void SetImpregnable()
    {
        foreach (var enemy in _enemyInstances)
        {
            enemy.SetImpregnable(false);
        }
    }

    private void Restart()
    {
        _enemyInstances.RemoveAll(x => x);
        transform.position = _enemyStartPosition.position;
        InitEnemies();
    }

    private async UniTask WaitWaveEnd()
    {
        await UniTask.WaitWhile(() => _enemyInstances.Any(x => x != null), cancellationToken: _cts.Token);
        Debug.Log("Complete");

        if (!_cts.Token.IsCancellationRequested)
            Restart();
    }

    private async UniTask MoveEnemies()
    {
        await transform.DOMove(_enemyEndPosition.position, _moveTime).AsyncWaitForCompletion();
    }

    private void GenerateEnemies()
    {
        foreach (var enemyPos  in _enemiesPositions)
        {
            var rndValue = Random.Range(0f, 1f);
            var enemyType = (EnemyPlayer)null;

            var value = 0f;

            foreach (var e in enemies)
            {
                value += e.Chance;

                if (value >= rndValue)
                {
                    enemyType = e.Enemy;
                    break;
                }
            }

            if (enemyType == null)
            {
                enemyType = enemies[Random.Range(0, enemies.Count - 1)].Enemy;
            }

            var enemyInstance = Instantiate(enemyType, enemyPos);
            enemyInstance.transform.position = enemyPos.position;

            _enemyInstances.Add(enemyInstance);
        }
    }

    private void OnDestroy()
    {
        _cts.Cancel();
    }
}
