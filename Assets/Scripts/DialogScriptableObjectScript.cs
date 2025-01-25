using UnityEngine;

[CreateAssetMenu(fileName = "DialogObject", menuName = "Scriptable Objects/DialogObject")]
public class DialogScriptableObjectScript : ScriptableObject
{
    // multiline
    [Multiline]
    public string dialogText;

    public AudioClip dialogAudio;
}
