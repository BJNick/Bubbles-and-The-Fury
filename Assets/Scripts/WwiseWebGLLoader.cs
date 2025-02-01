using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class WwiseWebGLLoader : MonoBehaviour
{
    private string streamingAssetsPath;
    private string idbfsPath;

    void Start()
    {
        // StreamingAssets Path
        streamingAssetsPath = Application.streamingAssetsPath + "/Audio/GeneratedSoundBanks/Web/";
        
        // IndexedDB Path (idbfs)
        idbfsPath = Application.persistentDataPath + "/Audio/GeneratedSoundBanks/Web/";

        // Start Copying Files
        StartCoroutine(CopySoundBanks());
    }

    IEnumerator CopySoundBanks()
    {
        // Ensure the directories exist
        if (!Directory.Exists(idbfsPath))
        {
            Debug.Log("[WwiseWebGLLoader] Creating missing directories in idbfs...");
            Directory.CreateDirectory(idbfsPath);
        }

        // List of required files
        string[] soundBankFiles = {
            "Bubble_Soundbank.bnk",
            "Init.bnk",
            "SoundbanksInfo.json"
        };

        foreach (string file in soundBankFiles)
        {
            string sourcePath = Path.Combine(streamingAssetsPath, file);
            string destinationPath = Path.Combine(idbfsPath, file);

            // Check if file already exists in IndexedDB
            if (File.Exists(destinationPath))
            {
                Debug.Log($"[WwiseWebGLLoader] {file} already exists in idbfs, skipping copy.");
                continue;
            }

            Debug.Log($"[WwiseWebGLLoader] Copying {file} to IndexedDB...");

            // Load from StreamingAssets (WebGL requires UnityWebRequest)
            using (UnityWebRequest request = UnityWebRequest.Get(sourcePath))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    // Write file to IndexedDB
                    File.WriteAllBytes(destinationPath, request.downloadHandler.data);
                    Debug.Log($"[WwiseWebGLLoader] Copied {file} to idbfs.");
                }
                else
                {
                    Debug.LogError($"[WwiseWebGLLoader] Failed to load {file}: {request.error}");
                }
            }
        }


        // Sync IndexedDB
        #if UNITY_WEBGL && !UNITY_EDITOR
            SyncIndexedDB(ReloadWwiseSoundBanks);  // Call Wwise reload after sync
        #else
            ReloadWwiseSoundBanks();
        #endif
    }

    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void SyncIndexedDB(System.Action onComplete);
    #endif

    void ReloadWwiseSoundBanks()
    {
        Debug.Log("[WwiseWebGLLoader] Reloading Wwise SoundBanks...");

        // Call Wwise API to reload SoundBanks
        AkSoundEngine.ClearBanks(); // Unload all existing banks
        AkSoundEngine.SetBasePath(idbfsPath); // Set correct path
        AkSoundEngine.LoadBank("Init.bnk", out _);
        AkSoundEngine.LoadBank("Bubble_Soundbank.bnk", out _);

        Debug.Log("[WwiseWebGLLoader] SoundBanks reloaded successfully.");
    }
}