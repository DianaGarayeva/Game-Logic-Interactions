using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _enemiesText;

    private float _remainingTime=100f; 
    [SerializeField]
    private Text _timerText;

    private EnemySpawningManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<EnemySpawningManager>(); 
        _scoreText.text = "Score: 0";
        _enemiesText.text = "Enemies: 0";
        _timerText.text = "0.0";
    }

    // Update is called once per frame
    void Update()
    {
        _enemiesText.text = "Enemies: " + EnemyAI.enemyCounter;
        if (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(_remainingTime);
            if (_remainingTime <= 0)
            {
                _remainingTime = 0;
                _spawnManager.onGameOver();
            }
            _timerText.text = seconds.ToString();
        }


    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

}
