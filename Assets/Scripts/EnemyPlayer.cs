using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class EnemyPlayer : Player
{
    [SerializeField] protected float _chanceToAttack;
    [SerializeField] protected int _scoreForKill;
    
    private bool _isImpregnable = true;

    public void SetImpregnable(bool isImpregnable)
    {
        _isImpregnable = isImpregnable;
    }

    public override void GetDamage(int damage)
    {
        if (!_isImpregnable)
        {
            base.GetDamage(damage);
        }
    }

    public override async UniTask StartAttack(CancellationToken token)
    {
        while (!token.IsCancellationRequested && IsAlive)
        {
            var rndValue = UnityEngine.Random.Range(0f, 1f);

            if (rndValue < _chanceToAttack)
            {
                Attack();
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_attackSpeed), cancellationToken: token);
        }
    }

    protected override void PlayerDie()
    {
        AbilityManager.TryGenerateAbility(transform.parent);
        ScoreManager.AddScore(_scoreForKill);
        Destroy(gameObject);
    }
}
