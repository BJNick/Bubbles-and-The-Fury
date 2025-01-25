using UnityEngine;

public class DiverController : MonoBehaviour
{

    public float swimSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Move the diver
        transform.position += swimSpeed * controlsDir.magnitude * transform.right * Time.deltaTime;

    }
}
