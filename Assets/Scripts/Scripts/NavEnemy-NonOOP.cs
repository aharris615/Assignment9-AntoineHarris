using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavEnemyNonOPP : MonoBehaviour {
	protected EnemyAIStates state = EnemyAIStates.Partolling;
	static protected List<GameObject> navPatrolPoints = null;

	public float walkingSpeed = 3.0f;
	public float chasingSpeed = 5.0f;
	public float attackingSpeed = 1.5f;

	public float attackingDistance = 1.0f;

	protected GameObject patrollingInterestPoint;
	protected GameObject playerOfInterest;

	protected NavMeshAgent navMeshAgent;

	void Start () {
		if(navPatrolPoints==null) {
			navPatrolPoints = new List<GameObject>();
			foreach(GameObject go in GameObject.FindGameObjectsWithTag("NavPatrolPoints")) {
				Debug.Log("Adding NavEnemy Patrol Point: " + go.transform.position);
				navPatrolPoints.Add(go);
			}
		}
		GameObject hair = GameObject.Find("Hair");
		hair.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f),Random.Range(0.0f, 1.0f),Random.Range(0.0f, 1.0f));
		SwitchToPatrolling();
	}
	
	void Update () {
		switch(state) {
			case EnemyAIStates.Attacking:
				OnAttackingUpdate();
				break;
			case EnemyAIStates.Chasing:
				OnChasingUpdate();
				break;
			case EnemyAIStates.Partolling:
				OnPatrollingUpdate();
				break;
		}
	}

	void OnAttackingUpdate() {
		navMeshAgent.SetDestination(playerOfInterest.transform.position);
		
		float distance = Vector3.Distance(transform.position, playerOfInterest.transform.position);
		if(distance>attackingDistance) {
			SwitchToChasing(playerOfInterest);
		}
	}

	void OnChasingUpdate() {
		navMeshAgent.SetDestination(playerOfInterest.transform.position);
		
		float distance = Vector3.Distance(transform.position, playerOfInterest.transform.position);
		if(distance<=attackingDistance) {
			SwitchToAttacking(playerOfInterest);
		}
	}

	void OnPatrollingUpdate() {
		navMeshAgent.SetDestination(patrollingInterestPoint.transform.position);
		
		float distance = Vector3.Distance(transform.position, patrollingInterestPoint.transform.position);
		Debug.Log("Nav Enemy Distance: " + distance);
		if(distance<=navMeshAgent.stoppingDistance) {
			SelectRandomPatrolPoint();
		}
	}

	void OnTriggerEnter(Collider collider) {
		SwitchToChasing(collider.gameObject);
	}

	void OnTriggerExit(Collider collider) {
		SwitchToPatrolling();
	}

	void SwitchToPatrolling() {
		state = EnemyAIStates.Partolling;
		GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f);
		SelectRandomPatrolPoint();
		playerOfInterest = null;
	}

	void SwitchToAttacking(GameObject target) {
		state = EnemyAIStates.Attacking;
		GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
	}

	void SwitchToChasing(GameObject target) {
		state = EnemyAIStates.Chasing;
		GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 0.0f);
		playerOfInterest = target;
	}

	void SelectRandomPatrolPoint() {
		int choice = Random.Range(0,navPatrolPoints.Count);
		patrollingInterestPoint = navPatrolPoints[choice];
		navMeshAgent.SetDestination(patrollingInterestPoint.transform.position);
		Debug.Log("Nav Enemy navigating to patrol to point " + choice + " at " + navPatrolPoints[choice].transform.position.ToString());
	}
}
