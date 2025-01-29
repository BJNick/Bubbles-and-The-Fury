using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    
    public DialogScriptableObjectScript dialog;

    public AK.Wwise.Switch switchToSet;

    public AK.Wwise.Event eventToPlay;

    public float lightIntensity = -1;

    public Color lightColor = Color.white;


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
            if (eventToPlay != null) {
                eventToPlay.Post(gameObject);
            }
            if (lightIntensity >= 0) {
                GlobalLights.instance.SetIntensity(lightIntensity);
                GlobalLights.instance.SetColor(lightColor);
            }
            Destroy(gameObject);
        }
    }
}
