using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    [SerializeField] private float timer;
    public TimerDisplay timer_display;

    private bool active = true;
    
    private void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;
            timer_display.updateTimer(timer);
        }
    }

    public void recordTime()
    {
        active = false;
        
        // track time
    }
}
