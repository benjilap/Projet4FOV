using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolAction : MonoBehaviour {

    [SerializeField]
    bool _patrolWaiting;

    [SerializeField]
    float _waitTime = 3f;

    [SerializeField]
    bool _nonBoucle;

    [SerializeField]
    List<PatrolWaypoint> _patrolPoints;

    [HideInInspector]
    public NavMeshAgent _navMeshAgent;
    public int _currentPatrolIndex;
    public bool _travelling;
    bool _patrolForward;

    public bool _waiting;
    public float _waitTimer;
    public float _totalWaitTime;
    public bool _knoked;


    void Start () {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null) {
            Debug.LogError("navmesha no attach" + this.name);
        }
        else
        {
            if (_patrolPoints != null )
            {
                _currentPatrolIndex = 0;
                SetDestination();
                _patrolForward = true;
            }
        }

	}
	


	void Update () {
        if (_travelling && _navMeshAgent.remainingDistance<= 0.1f)
        {
            _travelling = false;
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
                _totalWaitTime = _waitTime;
            }
            else if (_patrolPoints.Count >= 2) 
            {
                ChangePatrolPoint();
                SetDestination();
            }

        } 

        if (_waiting)
        {

            if (_knoked)
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
                if (this.transform.GetChild(3).GetComponent<ParticleSystem>().isPlaying == false)
                { 
                    this.transform.GetChild(3).GetComponent<ParticleSystem>().Play(true);
                }
            }
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;
                if (_patrolWaiting)
                {
                    ChangePatrolPoint();
                    SetDestination();
                }
                else
                if (_knoked)
                {
                    this.transform.GetChild(0).gameObject.SetActive(true);
                    this.transform.GetChild(3).GetComponent<ParticleSystem>().Stop(true);
                    _knoked = false;
                }
            }
        }

    }

    private void SetDestination()
    {
        if (_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;

        }
    }

    private void ChangePatrolPoint()
    {
        if (_patrolForward)
        {

            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
            if (_nonBoucle)
            {

                if (_currentPatrolIndex >= _patrolPoints.Count - 1)
                {
                    _patrolForward = !_patrolForward;
                    _currentPatrolIndex = _patrolPoints.Count - 1;
                }
            }
        }
        else
        {
            if (_currentPatrolIndex > 0)
            {

                _currentPatrolIndex -= 1;
                if (_currentPatrolIndex <= 0)
                {
                    _patrolForward = !_patrolForward;
                }
            }
        }

    }


}

