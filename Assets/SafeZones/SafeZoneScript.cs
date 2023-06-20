using UnityEngine;

public class SafeZoneScript : MonoBehaviour
{
    public GameObject head;
    public NavigationArrow nav_arrow;
    public TimerDisplay timer;
    public GameObject celebration_effects;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AudioManager.PlaySound(false, false, head.transform.position,
                AudioManager.SoundID.WIN);
            
            celebration_effects.SetActive(true);
            Debug.Log("transition");

            nav_arrow.navigating = false;
            timer.stopTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nav_arrow.navigating = true;
        timer.startTimerDisplay();
    }
}
