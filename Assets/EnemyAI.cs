using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPoint;
    [SerializeField]
    private GameObject _endPoint;
    private NavMeshAgent _agent;
    void Start()
    {
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
        Debug.Log(_agent.pathStatus);
        Debug.Log(_agent.hasPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent.pathPending && _agent.hasPath && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.isStopped = true;
        }
    }
}
