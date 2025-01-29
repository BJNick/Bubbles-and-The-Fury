using UnityEngine;

public class DiverController : MonoBehaviour
{

    public float swimSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    private Rigidbody2D rb;

    public AK.Wwise.Event swimEvent;

    public float swimEventInterval = 1.0f;

    private float lastSwimEventTime = 0.0f;

    private Animator animator;

    private float shockedUntil = 0.0f;

    public Vector3 lastSavedPosition;

    public GameObject[] teleportPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SetSpawnPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var controlsDir = new Vector3(h, v, 0);
        // Normalize controlsDir so that diagonal movement isn't faster
        controlsDir.Normalize();
        
        if (shockedUntil > Time.time) {
            controlsDir = Vector2.zero;
        }

        // Smoothly adjust direction to match controls using Mathf.LerpAngle
        var controlsAngle = Mathf.Atan2(controlsDir.y, controlsDir.x) * Mathf.Rad2Deg;
        if (controlsDir.magnitude > 0.1f) {
            var angle = Mathf.LerpAngle(transform.eulerAngles.z, controlsAngle, Time.fixedDeltaTime * turnSpeed); 
            rb.MoveRotation(angle);

            if (Time.time - lastSwimEventTime > swimEventInterval && swimEventInterval > 0f) {
                swimEvent.Post(gameObject);
                lastSwimEventTime = Time.time;
            }


            // Dir 0 if horizontal angle between -30 and 30
            // Dir 1 if diagonal angle between 30 and 60 or -30 and -60
            // Dir 2 if vertical angle between 60 and 120 or -60 and -120
            // Flip sprite if facing left
            /* if (transform.right.x > 0) {
                transform.localScale = new Vector3(1, 1, 1);
            } else {
                transform.localScale = new Vector3(-1, 1, 1);
            } */
            if (angle > 90) {
                angle = 270 - angle;
            }
            if (angle > -30 && angle < 30) {
                animator.SetInteger("Dir", 0);
            } else if ((angle > 30 && angle < 60) || (angle > -60 && angle < -30)) {
                animator.SetInteger("Dir", 1);
            } else {
                animator.SetInteger("Dir", 2);
            }
        }

        // Move the diver
        //rb.MovePosition(rb.position +fromVector3(swimSpeed * controlsDir.magnitude * transform.right * Time.deltaTime));
        rb.AddForce(swimSpeed * controlsDir.magnitude * transform.right);
        animator.SetFloat("Speed", controlsDir.magnitude);
    }

    void Update() {
        for (int i = 0; i < teleportPoints.Length; i++) {
            if (Input.GetKeyDown((i + 1).ToString()) && Input.GetKey(KeyCode.LeftShift) && teleportPoints[i] != null) {
                transform.position = teleportPoints[i].transform.position;
            }
        }
    }

    private Vector2 fromVector3(Vector3 v) {
        return new Vector2(v.x, v.y);
    }

    public void GetShocked(float duration) {
        shockedUntil = Time.time + duration;
    }

    public void PlaySwimEvent() {
        swimEvent.Post(gameObject);
    }

    public void Respawn() {
        transform.position = lastSavedPosition;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        shockedUntil = 0;
        rb.rotation = 0;
    }

    public void SetSpawnPoint() {
        lastSavedPosition = transform.position;
    }
}
