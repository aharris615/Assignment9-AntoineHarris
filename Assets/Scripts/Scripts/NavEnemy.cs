using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavEnemy : Enemy {
	static protected List<GameObject> navPatrolPoints = null;

	protected NavMeshAgent navMeshAgent;

	protected override void Start () {
		print ("START NAVENEMY!!");
		if(navPatrolPoints==null) {
			navPatrolPoints = new List<GameObject>();
			foreach(GameObject go in GameObject.FindGameObjectsWithTag("NavPatrolPoints")) {
				Debug.Log("Adding NavEnemy Patrol Point: " + go.transform.position);
				navPatrolPoints.Add(go);
			}
		}
		GameObject hair = GameObject.Find("Hair");
		hair.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f),Random.Range(0.0f, 1.0f),Random.Range(0.0f, 1.0f));
		navMeshAgent = GetComponent<NavMeshAgent>();

		SwitchToPatrolling();
	}

	protected override void OnAttackingUpdate() {
		navMeshAgent.SetDestination(playerOfInterest.transform.position);
		
		float distance = Vector3.Distance(transform.position, playerOfInterest.transform.position);
		if(distance>attackingDistance) {
			SwitchToChasing(playerOfInterest);
		}
	}
	
	protected override void OnChasingUpdate() {
		navMeshAgent.SetDestination(playerOfInterest.transform.position);
		
		float distance = Vector3.Distance(transform.position, playerOfInterest.transform.position);
		if(distance<=attackingDistance) {
			SwitchToAttacking(playerOfInterest);
		}
	}
	
	protected override void OnPatrollingUpdate() {
		navMeshAgent.SetDestination(patrollingInterestPoint.transform.position);
		
		float distance = Vector3.Distance(transform.position, patrollingInterestPoint.transform.position);
		Debug.Log("Nav Enemy Distance: " + distance);
		if(distance<=navMeshAgent.stoppingDistance) {
			SelectRandomPatrolPoint();
		}
	}

	protected override void SelectRandomPatrolPoint() {
		print ("navPatrolPoints.Count: " + navPatrolPoints.Count);
		int choice = Random.Range(0,navPatrolPoints.Count);
		patrollingInterestPoint = navPatrolPoints[choice];
		navMeshAgent.SetDestination(patrollingInterestPoint.transform.position);
		
		Debug.Log("Nav Enemy navigating to patrol to point " + patrollingInterestPoint.name + " at " + patrollingInterestPoint.transform.position.ToString());
	}
}
