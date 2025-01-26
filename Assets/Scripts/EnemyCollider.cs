using UnityEngine;

public class EnemyCollider : MonoBehaviour
{

    public float damageAmount = 10.0f;

    public float stunAmount = 0.0f;

    public ParticleSystem hitEffect;

    public AK.Wwise.Event hitEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(GameObject obj) {
        Debug.Log("Player collided with enemy");
        if (hitEvent != null &&  OxygenOverlay.instance.IsAlreadDying() == false) {
            hitEvent.Post(obj);
        }
        OxygenOverlay.instance.RemoveOxygen(damageAmount);
        if (stunAmount > 0.0f) {
            obj.GetComponent<DiverController>().GetShocked(stunAmount);
        }
        if (hitEffect != null) {
            hitEffect.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Damage(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Damage(other.gameObject);
        }
    }
}
