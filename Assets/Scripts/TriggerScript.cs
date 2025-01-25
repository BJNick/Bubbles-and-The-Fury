using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    
    public DialogScriptableObjectScript dialog;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // TODO: Display dialog
            Destroy(gameObject);
        }
    }
}
