using UnityEngine;

public class DiverController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var move = new Vector3(h, v, 0);
        transform.position += move * Time.deltaTime * 5;

        // Make player face the direction of movement
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, move);
        }

    }
}
