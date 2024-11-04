using UnityEngine;


public class ExampleReferencer : MonoBehaviour {
    public InterfaceReference<IExample> damageableReference;

    private void Start() {
        damageableReference.Value.ExampleMethod(1);
    }
}