using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float launchForce= 10f;

    private void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if(playerRigidbody != null)
            {
                playerRigidbody.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
            }
        }
    }
}
