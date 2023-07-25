using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere
    public float stoppingDistance = 0.5f; // Adjust this value as needed
    private Animator animator;
    public AudioSource chickenSound;
    public AudioSource chickenWalk;

    public Transform centrePoint; //centre of the area the agent wants to move around in

    // New variables for player detection and running away
    public LayerMask playerLayer; // LayerMask to define the player's layer
    public float detectionDistance = 10.0f; // Distance within which the chicken detects the player
    public float runAwayDistance = 5.0f; // Distance from the player at which the chicken starts running
    public float runSpeed = 5.0f; // Speed at which the chicken runs when escaping
    private float startTime;
    private float timePassed;
    private bool isPlayingSound = false;
    private bool isGrounded; // Variable to check if the chicken is grounded
    private float gravity = -9.81f; // Gravity value

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
        animator = GetComponent<Animator>();
        startTime = Time.time;
        chickenSound.enabled = false;
    }

    void Update()
    {
        float timePassed = Time.time - startTime;

        // Check if 5 seconds have passed and the sound is not already playing
        if (timePassed >= 4f && !isPlayingSound)
        {
            // Play the sound
            chickenSound.enabled = true;
            isPlayingSound = true;
        }

        // Check if 1 second has passed and the sound is playing
        if (timePassed >= 5f && isPlayingSound)
        {
            // Stop the sound
            chickenSound.enabled = false;
            isPlayingSound = false;

            // Reset the timer for the next sound
            startTime = Time.time;
        }

        // Check for player detection
        Collider[] detectedPlayers = Physics.OverlapSphere(transform.position, detectionDistance, playerLayer);

        if (detectedPlayers.Length > 0)
        {
            // Player detected, run away from the player
            Vector3 playerDirection = transform.position - detectedPlayers[0].transform.position;
            Vector3 targetPoint = transform.position + playerDirection.normalized * runAwayDistance;
            agent.SetDestination(targetPoint);
            agent.speed = runSpeed;
        }
        else
        {
            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    agent.SetDestination(point);
                }
            }
        }
        float speed = agent.velocity.magnitude;
        if (speed > 0.01)
        {
            animator.SetBool("Walk", true);
            chickenWalk.enabled = true;
        }
        else if (Mathf.Approximately(speed, runSpeed))
        {
            chickenWalk.enabled = true;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            chickenWalk.enabled = false;
        }
    }

    void FixedUpdate()
    {
        // Check if the chicken is grounded using a raycast
        CheckGrounded();

        // Apply gravity to the chicken
        ApplyGravity();
    }

    void CheckGrounded()
    {
        // Cast a ray from slightly above the chicken's position to check if it's grounded
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f);
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            // Calculate the new position with gravity
            Vector3 newPosition = transform.position + Vector3.up * gravity * Time.fixedDeltaTime;

            // Check if the new position is above the ground using a raycast
            RaycastHit hit;
            if (Physics.Raycast(newPosition, Vector3.down, out hit, 0.2f))
            {
                // If the new position is above the ground, set the position to the hit point
                newPosition.y = hit.point.y;
            }

            // Set the chicken's position to the new position
            transform.position = newPosition;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
