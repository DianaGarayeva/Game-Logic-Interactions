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
    private bool _isGameOver; 


    void Start()
    {
        _isGameOver = false;
        StartCoroutine(SpawningRoutine());
    }

    IEnumerator SpawningRoutine()
    {
        while (!_isGameOver)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, _startPoint.transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(5, 15));
        }
    }

    public void onGameOver()
    {
        _isGameOver = true; 
    }


    
    

}
