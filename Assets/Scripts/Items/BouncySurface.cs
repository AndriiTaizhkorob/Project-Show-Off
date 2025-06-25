using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    public float bounceForce = 12f;
    public float cooldown = 0.2f;

    private bool canBounce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!canBounce) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && other.CompareTag("Player"))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // reset vertical
            rb.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);
            StartCoroutine(BounceCooldown());
        }
    }

    private System.Collections.IEnumerator BounceCooldown()
    {
        canBounce = false;
        yield return new WaitForSeconds(cooldown);
        canBounce = true;
    }
}
