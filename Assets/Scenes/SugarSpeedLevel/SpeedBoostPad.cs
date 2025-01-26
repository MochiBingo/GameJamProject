using UnityEngine;
using System.Collections;

public class SpeedBoostPad : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float boostDuration = 2f;
    [SerializeField] private Color activeColor = Color.cyan;
    
    private Material padMaterial;
    private Color originalColor;
    private bool isBoostAvailable = true;
    private float cooldownTime = 3f;

    private void Start()
    {
        padMaterial = GetComponent<MeshRenderer>().material;
        originalColor = padMaterial.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isBoostAvailable)
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                StartCoroutine(ApplySpeedBoost(playerManager));
                StartCoroutine(BoostPadCooldown());
            }
        }
    }
    private IEnumerator ApplySpeedBoost(PlayerManager player)
    {
        Debug.Log("Applying speed boost");
        
        // Store the original base speed
        float originalBaseSpeed = player.baseSpeed;
        
        // Apply boost directly to base speed
        player.baseSpeed *= speedMultiplier;
        
        // Change pad color to indicate active boost
        padMaterial.color = activeColor;
        
        // Wait for duration
        yield return new WaitForSeconds(boostDuration);
        
        // Reset base speed
        player.baseSpeed = originalBaseSpeed;
        
        Debug.Log("Speed boost ended");
        
        // Reset pad color
        padMaterial.color = originalColor;
    }

    private IEnumerator BoostPadCooldown()
    {
        isBoostAvailable = false;
        
        // Wait for cooldown
        yield return new WaitForSeconds(cooldownTime);
        
        // Reset pad color and availability
        padMaterial.color = originalColor;
        isBoostAvailable = true;
    }
}

