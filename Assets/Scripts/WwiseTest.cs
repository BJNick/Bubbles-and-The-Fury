using UnityEngine;


public class WwiseTest : MonoBehaviour
{

    public AK.Wwise.Event testEvent;

    public AK.Wwise.Switch testSwitch;

    public bool triggerEvent = false;

    public GameObject testObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerEvent && testEvent != null) {
            testEvent.Post(gameObject);
            triggerEvent = false;
        } else if (triggerEvent && testSwitch != null) {
            testSwitch.SetValue(testObject);
        }
    }
}
