using UnityEngine;

public class BigBubble : MonoBehaviour
{
    public bool runningCutscene = false;

    public float riseSpeed = 1f;

    public float currentSpeed = 0.0f;

    public float acceleration = 0.1f;

    public DialogScriptableObjectScript dialog;

    public float eventDelay = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (runningCutscene) {
            // Rise the bubble
            currentSpeed += acceleration * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + currentSpeed * Time.fixedDeltaTime, 0);

            // Snap player to the bubble
            GameObject.Find("Diver").transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            GameObject.Find("Diver").GetComponent<Collider2D>().enabled = false;

            OxygenOverlay.instance.FullOxygen();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (runningCutscene == false) {
                Invoke("PlayDialog", eventDelay);
            }
            runningCutscene = true;
        }
    }

    void PlayDialog() {
        if (dialog != null) {
            dialog.Play();
        }
    }
}
