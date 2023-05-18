using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected Bullet _bullet;
    [SerializeField] protected int _health;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected float _speed;
    [SerializeField] protected bool _isEnemy;

    public float Speed => _speed;
    public int Health => _health;
    public bool IsEnemy => _isEnemy;

    public bool IsAlive => _health > 0;

    public virtual void GetDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            PlayerDie();
        }
    }

    public async UniTask MoveToPosition(Vector3 endPosition, float time)
    {
        await transform.DOMove(endPosition, time).AsyncWaitForCompletion();
    }

    protected void Attack()
    {
        var bullet = Instantiate(_bullet);
        bullet.SetIsPlayerBullet(!_isEnemy);
        bullet.transform.position = transform.position;
    }

    public abstract UniTask StartAttack(CancellationToken token);

    protected abstract void PlayerDie();
}
