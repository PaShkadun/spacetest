using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private bool _isPlayerBullet;
    private Vector3 forward;

    public void SetIsPlayerBullet(bool isPlayer)
    {
        _isPlayerBullet = isPlayer;
        forward = _isPlayerBullet ? transform.forward : -transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player == null || player.IsEnemy == !_isPlayerBullet) return;

        player.GetDamage(_damage);
        Destroy(gameObject);
    }
}
