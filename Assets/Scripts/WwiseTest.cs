using UnityEngine;


public class WwiseTest : MonoBehaviour
{

    public AK.Wwise.Event testEvent;

    public AK.Wwise.Switch testSwitch;

    public bool triggerEvent = false;

    public GameObject testObject;

    public static WwiseTest instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        /* if (triggerEvent && testEvent != null) {
            testEvent.Post(gameObject);
            triggerEvent = false;
        } else */ 
        if (triggerEvent && testSwitch != null) {
            testSwitch.SetValue(testObject);
            Debug.Log("Set switch");
            if (triggerEvent && testEvent != null) {
                testEvent.Post(gameObject);
                Debug.Log("Set event");
                triggerEvent = false;
            }
            triggerEvent = false;
        }
        if (triggerEvent && testEvent != null) {
            testEvent.Post(gameObject);
            Debug.Log("Set event");
            triggerEvent = false;
        }
    }
}
