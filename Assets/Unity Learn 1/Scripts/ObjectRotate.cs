 using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 5.0f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed);
    }
}
