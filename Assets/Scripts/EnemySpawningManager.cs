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

    private int _spawned;
    private int _escaped;
    private bool _win;


    void Start()
    {
        _isGameOver = false;
        _spawned = 0;
        _escaped = 0;
        StartCoroutine(SpawningRoutine());
        _ui = GameObject.Find("UI_Manager").GetComponent<UIManager>(); 
    }

    IEnumerator SpawningRoutine()
    {
        while (!_isGameOver)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, _startPoint.transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            _spawned++;
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
    }
        
    public void onGameOver()
    {
        _isGameOver = true;
        var enemiesLeft = GetComponentsInChildren<Collider>();
        int count = 0;
        foreach (var enemy in enemiesLeft)
        {
            count++;
            enemy.GetComponent<EnemyAI>().OnGameOver();
        }
        if (_escaped >= _spawned * 0.5f | GameDevHQ.FileBase.Plugins.FPS_Character_Controller.FPS_Controller.score<=50)
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
        _ui.EnemiesEscaped(_escaped, _spawned); 
    }
}
