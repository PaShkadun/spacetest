using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class RealPlayer : Player
{
    [SerializeField] private GameObject _loseScreen;

    public override async UniTask StartAttack(CancellationToken token)
    {
        while (!token.IsCancellationRequested && IsAlive)
        {
            Attack();
            await UniTask.Delay(TimeSpan.FromSeconds(_attackSpeed), cancellationToken: token);
        }
    }

    public void SetNewBullet(Bullet bullet)
    {
        _bullet = bullet;
    }

    protected override void PlayerDie()
    {
        _loseScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
