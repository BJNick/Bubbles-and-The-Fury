using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogObject", menuName = "Scriptable Objects/DialogObject")]
public class DialogScriptableObjectScript : ScriptableObject
{
    // multiline
    [Multiline]
    public string text;

    public AK.Wwise.Event audio;

    public float duration = 10.0f;

    public Sprite image;

    public AK.Wwise.Event treasureEvent;

    public bool isTreasure = false;

    private GameObject panel;

    public void Play() {
        panel = GameObject.Find("GlobalDialogPanel");
        panel.GetComponent<Animator>().SetBool("Dialog Shown", true);
        panel.GetComponentInChildren<TextMeshProUGUI>().text = text;
        if (isTreasure && treasureEvent != null) {
            treasureEvent.Post(panel);
            if (image != null) {
                panel.GetComponent<GlobalDialogScript>().dialogEvent = audio;
                panel.GetComponent<GlobalDialogScript>().Invoke("PlayAudio", 1f);
            }
        } else
        if (audio != null) {
            audio.Post(panel);
        }
        if (image != null) {
            panel.transform.Find("DialogPanel").Find("Image").GetComponent<Image>().sprite = image;
        }
        // Trigger stop when duration is over
        panel.GetComponent<GlobalDialogScript>().Invoke("Stop", duration);
    }

    public void StartLater() {
        audio.Post(panel);
    }

    public void Stop() {
        var panel = GameObject.Find("GlobalDialogPanel");
        panel.GetComponent<Animator>().SetBool("Dialog Shown", false);
        
    }
}
