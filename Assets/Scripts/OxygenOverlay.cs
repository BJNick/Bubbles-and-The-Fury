using UnityEngine;

public class OxygenOverlay : MonoBehaviour
{
    public float oxygenLevel = 10f;

    public float maxOxygenLevel = 10f;

    public float maxBarSize = 1080f;

    public float oxygenDropRate = 1f;

    public float barWidth = 50;

    public static OxygenOverlay instance;

    public bool isDying = false;

    public AK.Wwise.Event deathEvent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Reduce oxygen level
        oxygenLevel -= Time.deltaTime * oxygenDropRate;
        if (oxygenLevel < 0) {
            oxygenLevel = 0;
            StartDying();   
        }
        // Need to make height of oxygen bar proportional to oxygen level
        transform.Find("OxygenPanel").Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(barWidth, maxBarSize * oxygenLevel / maxOxygenLevel);
    }

    public void AddOxygen(float amount) {
        oxygenLevel += amount;
        if (oxygenLevel > maxOxygenLevel) {
            oxygenLevel = maxOxygenLevel;
        }
        if (isDying) {
            GetComponent<Animator>().SetBool("Dying", false);
            isDying = false;
        }
    }

    public void FullOxygen() {
        oxygenLevel = maxOxygenLevel;
        if (isDying) {
            GetComponent<Animator>().SetBool("Dying", false);
            isDying = false;
        }
    }

    public void RemoveOxygen(float amount) {
        oxygenLevel -= amount;
        if (oxygenLevel < 0) {
            oxygenLevel = 0;
            StartDying();   
        }
    }

    public void SetRate(float rate) {
        oxygenDropRate = rate;
    }

    public void StartDying() {
        // Die
        if (!isDying) {
            GetComponent<Animator>().SetBool("Dying", true);
            Invoke("DoneDying", 4f);
        }
        isDying = true;
    }

    public void DoneDying() {
        if (oxygenLevel > 0) {
            return;
        }
        Debug.Log("Dying done");
        deathEvent.Post(gameObject);
        GameObject.Find("Diver").GetComponent<DiverController>().Respawn();
        FullOxygen();
    }

    public bool IsAlreadDying() {
        return isDying;
    }
}
