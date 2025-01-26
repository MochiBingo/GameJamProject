using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint; // Reference to spawn point in scene
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {  
            Debug.Log("Player hit death trigger");
            
            if(player != null)
            {
                // Reset position
                player.transform.position = spawnPoint != null 
                    ? spawnPoint.position 
                    : new Vector3(-3.69692039f, 26.3799992f, -25.8098297f);
                
                // Reset velocity
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            else
            {
                Debug.LogWarning("Player reference not set in RespawnTrigger");
            }
        }
    }
}

