using UnityEngine;


public class Example : MonoBehaviour, IExample
{
    public void ExampleMethod(int amount)
    {
        Debug.Log($"Example: {amount} on {gameObject.name}");
    }
}