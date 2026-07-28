using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private float explosionRadius = 5f;

    public void Explode()
    {
        GameObject newExplosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        DamageEnemies();
        Destroy(this.gameObject, 1f);
        Destroy(newExplosion, 3f);
    }

    private void DamageEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hits)
        { var enemy = hit.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Die();
            }
        }
    }
}
