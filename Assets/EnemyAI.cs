using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
public class EnemyAI : MonoBehaviour
{
    private enum EnemyState
    {
        Run,
        Hide, 
        Die,
    }

    [SerializeField]
    private GameObject _startPoint;
    [SerializeField]
    private GameObject _endPoint;
    private NavMeshAgent _agent;
   
    //private Animator _animator;

    [SerializeField]
    private EnemyState _currentState;

    void Start()
    {
        //_animator = GetComponent<Animator>();   
        _agent = GetComponent<NavMeshAgent>();
        _endPoint = GameObject.Find("Endpoint");
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

    // Update is called once per frame
    void Update()
    {
        switch (_currentState) 
        {
            case EnemyState.Run:
                //_animator.SetBool("isHiding", false); 
                _agent.isStopped = false;
                break;
            case EnemyState.Hide:
                //_animator.SetBool("isHiding", true);
                StartCoroutine(HideRoutine());
                break;
            case EnemyState.Die:
                Debug.Log("Died"); 
                break;
            default:
                Debug.Log("Default");
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
        _agent.GetComponent<Collider>().enabled = false; 
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        _currentState = EnemyState.Run;
        yield return new WaitForSeconds(2f);
        _agent.GetComponent<Collider>().enabled = true;
        
    }
    public void Die()
    {
        Debug.Log("Died"); 
    }
}
