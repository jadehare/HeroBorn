using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform PatrolRoute;

    public List<Transform> Locations;

    public Transform Player;

    private int _locationIndex = 0;

    private NavMeshAgent _agent;

    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            Debug.Log("Enemy lives: " + _lives);
            if (_lives <= 0)
            {
                Debug.Log("Enemy defeated!");
                Destroy(gameObject); // Destroy the enemy object
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        Player = GameObject.Find("Player").transform;
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in PatrolRoute)
        {
            Locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        _agent.destination = Locations[_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % Locations.Count;
        Debug.Log("Moving to next patrol location: " + Locations[_locationIndex].name);
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance < 0.2f && !_agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player has entered the enemy's trigger area!");
            _agent.destination = Player.position; // Move towards the player
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player has exited the enemy's trigger area!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)") { 
            EnemyLives -= 1;
        }
    }
}
