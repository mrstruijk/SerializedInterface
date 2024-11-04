using UnityEngine;


[CreateAssetMenu(fileName = "ExampleAsset", menuName = "SOSXR/SerializedInterface/ExampleAsset")]
public class ExampleAsset : ScriptableObject, IExample {
    public void ExampleMethod(int amount) {
        Debug.Log($"Example: {amount}");
    }
}