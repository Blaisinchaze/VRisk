using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public enum SoundID
    {
        MENU_HOVER, 
        MENU_CLICK,
        SEISMIC_RUMBLE, 
        DEBRIS,
        BUILDING_DAMAGE, 
        BUILDING_COLLAPSE
    }

    public List<Sound> sounds;
    public List<Pair<GameObject, AudioSource>> sources;
    public int number_of_sources = 8;
    private void Awake()
    {
        for (int i = 0; i < number_of_sources; i++)
        {
            GameObject game_object = new GameObject("ManagedAudioSource");
            game_object.transform.SetParent(gameObject.transform);

            var audio_source = game_object.AddComponent<AudioSource>();
            
            sources.Add(new Pair<GameObject, AudioSource>(game_object, audio_source));
        }
    }

    public void PlaySound(bool _two_dimensional, Vector3 _position, SoundID _sound_id)
    {
        foreach (var source in sources)
        {
            if (!source.second.isPlaying)
            {
                source.first.transform.position = _position;
                source.second.spatialBlend = _two_dimensional ? 0.0f : 1.0f;

                foreach (var sound in sounds)
                {
                    if (sound.id == _sound_id)
                    {
                        source.second.clip = sound.clip;
                        source.second.volume = sound.volume;
                        source.second.pitch = sound.pitch;
                        source.second.Play();
                        return;
                    }
                }
            }
        }
    }

    public void setMasterVolume(float _volume_value)
    {
        AudioListener.volume = _volume_value;
    }
}
 