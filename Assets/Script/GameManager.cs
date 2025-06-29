using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int Answer = 0;
    public List<Operation> operations = new();
    public List<Operation> usedOperations = new();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        GenerateOperations();
    }

    public void GenerateOperations()
    {
        for (int i = 0; i < 10; i++)
        {
            var operation = new Operation();
            operation.Symbol = (Symbol)Random.Range(0, 4);
            operation.Value = Random.Range(1, 10);
            operations.Add(operation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Invoke("ReloadLevel", 1f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene("Game");
    }


}
