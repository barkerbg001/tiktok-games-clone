using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    private GameObject player;
    public float followSpeed = 5f;
    public float followDistance = 2f; // Distance to maintain from the player
    public float smoothness = 0.1f; // Adjust for smoother following

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Player not found! Ensure the Player GameObject has the correct tag.");
        }
    }

    void Update()
    {
        FollowPlayer();
        CheckOutOfBounds();
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        // Calculate the target position
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 targetPosition = player.transform.position - direction * followDistance;

        // Smoothly move towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness);
    }

    private void CheckOutOfBounds()
    {
        // Destroy the Runner if it falls below the ground
        if (transform.position.y < -2)
        {
            Destroy(gameObject);

            // Update GameManager and remove the runner from the list
            GameManager.instance.Answer--;

            RunnerSpawnerController runnerController = FindObjectOfType<RunnerSpawnerController>();
            if (runnerController != null)
            {
                runnerController.runners.Remove(gameObject);
            }
        }
    }
}
