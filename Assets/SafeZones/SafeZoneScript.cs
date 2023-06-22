using UnityEngine;

public class SafeZoneScript : MonoBehaviour
{
    public GameObject head;
    public NavigationArrow nav_arrow;
    public GameObject celebration_effects;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Celebrate
            GameManager.Instance.AudioManager.PlaySound(false, false, head.transform.position, AudioManager.SoundID.WIN);
            celebration_effects.SetActive(true);
            
            
            nav_arrow.navigating = false;
            // TRIGGER DATA STORING.
            // TRIGGER TRANSITION.
            
            Debug.Log("transition");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nav_arrow.navigating = true;
        }
    }
}
