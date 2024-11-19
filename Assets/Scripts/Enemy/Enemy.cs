using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private Vector3 lastKnownPlayerPosition;

    public NavMeshAgent NavMeshAgent { get => navMeshAgent; }
    public GameObject Player { get => player; }
    public Vector3 LastKnownPlayerPosition { get => lastKnownPlayerPosition; set => lastKnownPlayerPosition = value; }

    public Path path;
    [Header("Sight Settings")]
    public float sightDistance = 20f;
    public float sightFOV = 65f;
    public float eyeHeight = 1.5f;
    [Header("Attack Settings")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;
    [SerializeField]
    private string state;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        state = stateMachine.currentState.ToString();
    }


    public bool CanSeePlayer()
    {
        if (player != null)
        {
            //Check if player is within sight distance
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 directionToPlayer = player.transform.position - transform.position - Vector3.up * eyeHeight;
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
                if(angleToPlayer >= -sightFOV && angleToPlayer <= sightFOV)
                {
                    //Check if player is in line of sight
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), directionToPlayer);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if(hitInfo.transform.gameObject == player)
                        {
                            Debug.Log("Player is in sight");
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                    
                }
            }
        }
        return false;
    }
}
