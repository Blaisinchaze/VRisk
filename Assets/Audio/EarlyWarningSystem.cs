using System.Collections.Generic;
using UnityEngine;

public class EarlyWarningSystem : MonoBehaviour
{
    public List<AudioSource> sources;

    private void Awake()
    {
        sources.AddRange(transform.GetComponentsInChildren<AudioSource>());
    }

    public void triggerWarningSiren(float _duration)
    {
        foreach (var source in sources)
        {
            GameManager.Instance.AudioManager.PlaySound(source, true, false, source.transform.position, AudioManager.SoundID.WARNING_SIREN, _duration);
        }
    }
}
