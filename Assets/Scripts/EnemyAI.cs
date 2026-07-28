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

    [SerializeField]
    private EnemySpawningManager _spawning;

    [SerializeField]
    private GameObject _barrier;


    private bool _isHiding;

    private AudioSource _audio;
    [SerializeField]
    private AudioClip _clip;
    
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

        _spawning = GameObject.Find("Spawn_Manager").GetComponent<EnemySpawningManager>();
        if(_spawning == null)
        {
            Debug.LogError("Spawning Manager is NULL");
        }

        _audio = GetComponent<AudioSource>();
        if (!_audio)
        {
            Debug.LogError("AudioSourse is null");
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
               
                break;
        }


        if (!_agent.pathPending &&
            _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _spawning.Escaped();
            Destroy(this.gameObject); 
        }
    }

    public void Hide()
    {
        _currentState = EnemyState.Hide;
        _agent.velocity = Vector3.zero;
    }

    IEnumerator HideRoutine()
    {
        _agent.isStopped = true;
        GetComponent<Collider>().enabled = false;
        _animator.SetBool("isHiding", true);
        yield return new WaitForSeconds(Random.Range(0, 2f));
        _currentState = EnemyState.Run;
        yield return new WaitForSeconds(1f);
        GetComponent<Collider>().enabled = true;
        _isHiding = false; // reset the flag
    }
    public void Die()
    {
        _currentState = EnemyState.Die;
        _agent.isStopped = true;
        _animator.SetTrigger("dead");
        Destroy(this.GetComponent<Collider>());
        Destroy(this.gameObject, 3f);
        _audio.PlayOneShot(_clip);
    }
 
    public void OnGameOver()
    {
        Debug.Log("GameOVer");
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }

    
}
