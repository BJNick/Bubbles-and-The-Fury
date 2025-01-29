using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class BigBubble : MonoBehaviour
{
    public bool runningCutscene = false;

    public float riseSpeed = 1f;

    public float currentSpeed = 0.0f;

    public float acceleration = 0.1f;

    public DialogScriptableObjectScript dialog;

    public float eventDelay = 0.5f;

    public float cutToMenuAfter = 15.0f;

    public float stopAt = -210.0f;


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
            if (currentSpeed > riseSpeed) {
                currentSpeed = riseSpeed;
            }
            if (transform.position.y < stopAt) {
                transform.position = new Vector3(transform.position.x, transform.position.y + currentSpeed * Time.fixedDeltaTime, 0);
            } else {
                currentSpeed = 0;
            }

            // Snap player to the bubble
            GameObject.Find("Diver").transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            GameObject.Find("Diver").GetComponent<Collider2D>().enabled = false;

            GameObject.Find("Diver").GetComponentInChildren<Light2D>().enabled = false;
            GameObject.Find("Diver").GetComponentInChildren<Light2D>().enabled = false;

            OxygenOverlay.instance.FullOxygen();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (runningCutscene == false) {
                Invoke("PlayDialog", eventDelay);
                Invoke("CutToBlack", cutToMenuAfter - 4.0f);
                Invoke("CutToMenu", cutToMenuAfter);
            }
            runningCutscene = true;
        }
    }

    void PlayDialog() {
        if (dialog != null) {
            dialog.Play();
        }
    }

    void CutToBlack() {
        OxygenOverlay.instance.GetComponent<Animator>().SetBool("Dying", true);
    }

    void CutToMenu() {
        SceneManager.LoadScene(0);
    }
}
