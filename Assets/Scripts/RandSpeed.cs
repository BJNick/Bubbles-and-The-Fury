using UnityEngine;

public class RandSpeed : MonoBehaviour
{
    public float minSpeed = 0.8f;
    public float maxSpeed = 1.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Animator>().SetFloat("Speed", Random.Range(minSpeed, maxSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
