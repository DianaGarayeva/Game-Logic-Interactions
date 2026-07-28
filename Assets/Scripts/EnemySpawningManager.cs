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

    private UIManager _ui;

    private int count;

    private int _spawned;
    private int _escaped;
    private bool _win;

    void Start()
    {
        _isGameOver = false;
        StartCoroutine(SpawningRoutine());
        _spawned = 0;
        _escaped = 0;

        _ui = GameObject.Find("UI_Manager").GetComponent<UIManager>(); 
    }

    IEnumerator SpawningRoutine()
    {
        while (!_isGameOver)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, _startPoint.transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            newEnemy.name = $"Enemy {count}";
            count++;
            _spawned++;
            yield return new WaitForSeconds(Random.Range(5, 15));
        }
    }

    public void onGameOver()
    {
        _isGameOver = true;
        var enemiesLeft = GetComponentsInChildren<Collider>();
        int count = 0;
        foreach(var enemy in enemiesLeft)
        {
            enemy.GetComponent<EnemyAI>().OnGameOver();
            Debug.Log("Stop enemies");
            count++; 
        }
        if (_escaped >= _spawned * 50 / 100 | count>= _spawned * 50 / 100)
        {
            _win = false;
        }
        else
        {
            _win = true; 
        }
        _ui.WinnerOrLoser(_win); 
    }

    public void Escaped()
    {
        _escaped++; 
    }
}
