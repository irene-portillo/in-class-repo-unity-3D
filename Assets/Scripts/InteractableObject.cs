using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    
    private void OnTriggerEnter(Collider other)
    {
        cube.transform.localScale *= 2;
    }

    private void OnTriggerExit(Collider other)
    {
        cube.transform.localScale *= 0.5f;
    }
}
