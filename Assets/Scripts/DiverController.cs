using UnityEngine;

public class DiverController : MonoBehaviour
{

    public float swimSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    private Rigidbody2D rb;

    public AK.Wwise.Event swimEvent;

    public float swimEventInterval = 1.0f;

    private float lastSwimEventTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var controlsDir = new Vector3(h, v, 0);

        // Smoothly adjust direction to match controls using Mathf.LerpAngle
        var controlsAngle = Mathf.Atan2(controlsDir.y, controlsDir.x) * Mathf.Rad2Deg;
        if (controlsDir.magnitude > 0.1f) {
            var angle = Mathf.LerpAngle(transform.eulerAngles.z, controlsAngle, Time.deltaTime * turnSpeed); 
            rb.MoveRotation(angle);

            if (Time.time - lastSwimEventTime > swimEventInterval) {
                swimEvent.Post(gameObject);
                lastSwimEventTime = Time.time;
            }
        }

        // Move the diver
        //rb.MovePosition(rb.position +fromVector3(swimSpeed * controlsDir.magnitude * transform.right * Time.deltaTime));
        rb.AddForce(swimSpeed * controlsDir.magnitude * transform.right);
    }

    private Vector2 fromVector3(Vector3 v) {
        return new Vector2(v.x, v.y);
    }
}
