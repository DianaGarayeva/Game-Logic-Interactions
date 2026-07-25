using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private enum EnemyState
    {
        Run,
        Hide, 
        Die,
    }

    public static int enemyCounter;

    [SerializeField]
    private GameObject _startPoint;

    [SerializeField]
    private GameObject _endPoint;

    private NavMeshAgent _agent;

    private UIManager _ui;

    private Animator _animator;

    [SerializeField]
    private EnemyState _currentState;

    private bool _isHiding;
    
    void Start()
    {
        _endPoint = GameObject.Find("Endpoint");

        _ui = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (!_ui)
        {
            Debug.LogError("UI is null");
        }

        _animator = GetComponent<Animator>();
        if (!_animator)
        {
            Debug.LogError("Animator is null");
        }

        _agent = GetComponent<NavMeshAgent>();
        if(_agent != null)
        {
         _agent.SetDestination(_endPoint.transform.position);
        }
        else
        {
            Debug.LogError("Agent is NULL");
        }

        _currentState = EnemyState.Run; 
    }

    private void OnEnable()
    {
        enemyCounter++;
    }

    private void OnDestroy()
    {
        enemyCounter--;
    }

    void Update()
    {
        switch(_currentState)
        {
            case EnemyState.Run:
                _agent.isStopped = false;
                _animator.SetBool("isHiding", false);
                break;
            case EnemyState.Hide:
                if (!_isHiding)
                {
                    _isHiding = true;
                    StartCoroutine(HideRoutine());
                }
                break;
            case EnemyState.Die:
                Debug.Log("Died");
                break;
        }

        if (!_agent.pathPending && _agent.hasPath && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.isStopped = true;
        }
    }

    public void Hide()
    {
        _currentState = EnemyState.Hide; 
    }

    IEnumerator HideRoutine()
    {
        _agent.isStopped = true;
        GetComponent<Collider>().enabled = false;
        _animator.SetBool("isHiding", true);
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        _currentState = EnemyState.Run;
        yield return new WaitForSeconds(1f);
        GetComponent<Collider>().enabled = true;
        _isHiding = false; // reset the flag
    }
    public void Die()
    {
        _currentState = EnemyState.Die; 
        _animator.SetBool("IsDead", true); 
        Destroy(this.gameObject, 2f); 
    }

    public void OnGameOver()
    {
        _agent.isStopped = true; 
    }

}
