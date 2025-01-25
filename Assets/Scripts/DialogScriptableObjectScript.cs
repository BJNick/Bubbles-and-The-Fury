using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogObject", menuName = "Scriptable Objects/DialogObject")]
public class DialogScriptableObjectScript : ScriptableObject
{
    // multiline
    [Multiline]
    public string text;

    public AudioClip audio;

    public float duration = 10.0f;

    public Sprite image;

    public void Play() {
        var panel = GameObject.Find("GlobalDialogPanel");
        panel.GetComponent<Animator>().SetBool("Dialog Shown", true);
        panel.GetComponentInChildren<TextMeshProUGUI>().text = text;
        if (audio != null && panel.GetComponent<AudioSource>() != null) {
            panel.GetComponent<AudioSource>().clip = audio;
            panel.GetComponent<AudioSource>().Play();
            duration = audio.length;
        }
        if (image != null) {
            panel.transform.Find("DialogPanel").Find("Image").GetComponent<Image>().sprite = image;
        }
        // Trigger stop when duration is over
        panel.GetComponent<GlobalDialogScript>().Invoke("Stop", duration);
    }

    public void Stop() {
        var panel = GameObject.Find("GlobalDialogPanel");
        panel.GetComponent<Animator>().SetBool("Dialog Shown", false);
        
    }
}
