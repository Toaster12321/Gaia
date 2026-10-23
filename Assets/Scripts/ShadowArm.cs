using UnityEngine;

public class ShadowArm : MonoBehaviour
{
    public Transform playerBody; // The Player object
    private float rotationX; // You need to get this from your FirstPersonController

    private void LateUpdate()
    {
        transform.rotation = playerBody.rotation * Quaternion.Euler(rotationX, 0, 0);
    }
}
