using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    public float oxygenAmount = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && OxygenOverlay.instance != null) {
            OxygenOverlay.instance.AddOxygen(oxygenAmount);
            Destroy(gameObject);
        }
    }
}
