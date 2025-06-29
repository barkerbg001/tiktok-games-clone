using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class OperationsController : MonoBehaviour
{
    public Operation operation;
    public GameObject cube;
    public TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        operation = GameManager.instance.operations.FirstOrDefault();
        GameManager.instance.operations.Remove(operation);
        text.text = operation.Symbol.ToSymbolString() + " " + operation.Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
