using System;
using UnityEngine;

[Serializable]
public class Ability : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _chance;

    private const float _speed = 7f;

    public Bullet Bullet => _bullet;
    public float Chance => _chance;

    private void Update()
    {
        transform.position += _speed * -transform.forward * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<RealPlayer>();
        if (player == null) return;

        player.SetNewBullet(_bullet);
        Destroy(gameObject);
    }
}
