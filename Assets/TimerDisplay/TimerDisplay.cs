using System;
using System.Collections;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public ClockCharacter seconds_ones_char;
    public ClockCharacter seconds_tens_char;
    public ClockCharacter minutes_ones_char;
    public ClockCharacter minutes_tens_char;

    private int seconds;
    private int minutes;

    private bool active = false;

    private void Start()
    {
        startTimerDisplay();
    }

    public void startTimerDisplay()
    {
        seconds = 0;
        minutes = 0;

        active = true;
        StartCoroutine(updateTimer());
    }

    public void stopTimer()
    {
        active = false;
    }

    public void resetTimer()
    {
        seconds = 0;
        minutes = 0;
        
        updateDisplay();
    }

    public void updateDisplay()
    {
        ClockCharacter.Character secondsOnes = (ClockCharacter.Character)(seconds % 10);
        ClockCharacter.Character secondsTens = (ClockCharacter.Character)(seconds / 10);

        ClockCharacter.Character minutesOnes = (ClockCharacter.Character)(minutes % 10);
        ClockCharacter.Character minutesTens = (ClockCharacter.Character)(minutes / 10);

        seconds_ones_char.setCharacter(secondsOnes);
        seconds_tens_char.setCharacter(secondsTens);

        minutes_ones_char.setCharacter(minutesOnes);
        minutes_tens_char.setCharacter(minutesTens);
    }

    IEnumerator updateTimer()
    {
        while (active)
        {
            yield return new WaitForSeconds(1);

            seconds++;

            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
            }

            updateDisplay();
        }
    }
}
