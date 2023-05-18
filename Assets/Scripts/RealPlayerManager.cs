using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class RealPlayerManager : MonoBehaviour
{
    [SerializeField] private RealPlayer _realPlayer;
    [SerializeField] private Transform _playerEndPosition;

    private CancellationTokenSource _cts = new CancellationTokenSource();
    private float _moveTime = 2f;

    private void Start()
    {
        InitRealPlayer();
    }

    private async UniTask InitRealPlayer()
    {
        await _realPlayer.MoveToPosition(_playerEndPosition.position, _moveTime);
        _realPlayer.StartAttack(_cts.Token);
    }
}
