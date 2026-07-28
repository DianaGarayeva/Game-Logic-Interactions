using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private float explosionRadius = 5f;
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _clip;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (!_audio)
        {
            Debug.LogError("AudioSourse is null");
        }
    }

    public void Explode()
    {
        GameObject newExplosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        DamageEnemies();
        Destroy(this.gameObject, 0.5f);
        Destroy(newExplosion, 3f);
        _audio.PlayOneShot(_clip);
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
