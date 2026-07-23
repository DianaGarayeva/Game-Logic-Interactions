using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _startPoint;

    [SerializeField]
    private bool _isGameRunning;
    void Start()
    {
        _isGameRunning = true;
        StartCoroutine(SpawningRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawningRoutine()
    {
        while (_isGameRunning)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, _startPoint.transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(5, 15));
        }
    }

}
