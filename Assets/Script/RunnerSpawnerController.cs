using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSpawnerController : MonoBehaviour
{
    public GameObject player;
    public GameObject runner;
    public List<GameObject> runners = new List<GameObject>();
    public float followSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Follow the player
        if (player != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, player.transform.position, followSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
    }

    public void DuplicateSphere(Operation operation)
    {
        var lstOoperations = GameManager.instance.operations;


        //Calculate answer
        GameManager.instance.Answer = operation.Symbol switch
        {
            Symbol.Plus => GameManager.instance.Answer + operation.Value,
            Symbol.Minus => GameManager.instance.Answer - operation.Value,
            Symbol.Multiply => GameManager.instance.Answer * operation.Value,
            Symbol.Divide => GameManager.instance.Answer / operation.Value,
            _ => GameManager.instance.Answer
        };

        if (GameManager.instance.Answer < 0)
        {
            GameManager.instance.Answer = 0;
        }

        // Destroy all existing runners
        foreach (GameObject runner in runners)
        {
            Destroy(runner);
        }

        for (int i = 0; i < GameManager.instance.Answer; i++)
        {
            // Instantiate a new sphere as a child of the RunnerSpawner
            GameObject newSphere = Instantiate(runner, transform.position, Quaternion.identity);
            newSphere.transform.SetParent(transform);
            runners.Add(newSphere);
        }

    }
}
