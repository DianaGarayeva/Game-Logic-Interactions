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

    [SerializeField]
    private float _remainingTime=100f; 
    [SerializeField]
    private Text _timerText;

    [SerializeField]
    private Text _winnerText;

    [SerializeField]
    private Text _loserText;

    [SerializeField]
    private Image _crossImg;

    [SerializeField]
    private Text _enemiesEscaped;

    [SerializeField]
    private Text _enemiesSpawned;

    private EnemySpawningManager _spawnManager;

    [SerializeField]
    private Button _button; 

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
            if (_remainingTime <= 1)
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

    public void WinnerOrLoser(bool isWinner)
    {
        if (isWinner)
        {
            _winnerText.gameObject.SetActive(true);
        }
        else
        {
            _loserText.gameObject.SetActive(true); 
        }
        _crossImg.gameObject.SetActive(false);
        _button.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        _enemiesEscaped.gameObject.SetActive(true);
        _enemiesSpawned.gameObject.SetActive(true);

    }

    public void EnemiesEscaped(int escaped, int spawned)
    {
        _enemiesEscaped.text = "Enemies escaped: " + escaped.ToString();
        _enemiesSpawned.text = "Enemies spawned: " + spawned.ToString();
    }
}
