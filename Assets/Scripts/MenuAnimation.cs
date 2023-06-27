using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public float rotationSpeed = 60f; // Degrees per second

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
