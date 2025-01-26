using UnityEngine;

public class DumbFish : MonoBehaviour
{

    public float speed = 1.0f;

    private float lastSwitchTime = 0.0f;

    public bool flip = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (Time.time - lastSwitchTime < 1.0f) {
            return;
        }
        lastSwitchTime = Time.time;
        // Flip directon
        speed = -speed;
        if (flip) {
            GetComponentInChildren<SpriteRenderer>().flipX = !GetComponentInChildren<SpriteRenderer>().flipX;
        }
    }
}
