using UnityEngine;

public class GlobalDialogScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AK.Wwise.Event dialogEvent;

    public void Stop() {
        var panel = GameObject.Find("GlobalDialogPanel");
        panel.GetComponent<Animator>().SetBool("Dialog Shown", false);
    }

    public void PlayAudio() {
        dialogEvent.Post(gameObject);
    }
}
