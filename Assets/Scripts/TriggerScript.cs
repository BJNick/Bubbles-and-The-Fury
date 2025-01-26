using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    
    public DialogScriptableObjectScript dialog;

    public AK.Wwise.Switch switchToSet;


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
            if (dialog != null) {
                dialog.Play();
            }
            if (switchToSet != null) {
                switchToSet.SetValue(WwiseTest.instance.gameObject);
                Debug.Log("Set switch");
            }
            Destroy(gameObject);
        }
    }
}
