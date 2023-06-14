using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate float intensityControlFunction(float x);

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public enum SoundID
    {
        MENU_HOVER, 
        MENU_CLICK,
        SEISMIC_RUMBLE, 
        DEBRIS_COLLISION,
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

    public Pair<AudioSource, Sound> PlaySound(bool _loop, bool _two_dimensional, Vector3 _position, SoundID _sound_id)
    {
        foreach (var source in sources)
        {
            if (!source.second.isPlaying)
            {
                source.first.transform.position = _position;
                source.second.spatialBlend = _two_dimensional ? 0.0f : 1.0f;
                source.second.loop = _loop;

                foreach (var sound in sounds)
                {
                    if (sound.id == _sound_id)
                    {
                        source.second.clip = sound.clip;
                        source.second.volume = sound.volume;
                        source.second.pitch = sound.pitch;
                        source.second.Play();
                        return new Pair<AudioSource, Sound>(source.second, sound);
                    }
                }
            }
        }

        return new Pair<AudioSource, Sound>(null, null);
    }

    public Pair<AudioSource, Sound> PlaySound(bool _loop, bool _two_dimensional, Vector3 _position, Transform _parent, bool _pos_relative_to_parent, SoundID _sound_id)
    {
        var source_sound_pair = PlaySound(_loop, _two_dimensional, _position, _sound_id);
        source_sound_pair.first.transform.SetParent(_parent);
        if (_pos_relative_to_parent)
        {
            source_sound_pair.first.transform.localPosition = _position;
        }

        return source_sound_pair;
    }

    public Pair<AudioSource, Sound> PlaySound(bool _loop, bool _two_dimensional, Vector3 _position, Transform _parent, bool _pos_relative_to_parent, SoundID _sound_id, intensityControlFunction _volume_control, float _duration)
    { 
        var source_sound_pair = PlaySound(_loop, _two_dimensional, _position, _parent, _pos_relative_to_parent, _sound_id);
        StartCoroutine(volumeControl(source_sound_pair.first, source_sound_pair.second.volume, _volume_control, _duration));
        return source_sound_pair;
    }

    private IEnumerator volumeControl(AudioSource _source, float _max_volume, intensityControlFunction _volume_control_function, float _duration)
    {
        float elapsed = 0.0f;
        float vol_change_interval = 0.05f;
        float vol_change_timer = 0.0f;

        while (elapsed < _duration)
        {
            if (vol_change_timer > vol_change_interval)
            {
                float progress = (elapsed / _duration);
                float volume = _max_volume * _volume_control_function(progress);
                _source.volume = volume;

                vol_change_timer = 0.0f;
            }
            
            elapsed += Time.deltaTime;
            vol_change_timer += Time.deltaTime;
            yield return null;
        }
        
        _source.Stop();
    }
    
    public void setMasterVolume(float _volume_value)
    {
        AudioListener.volume = _volume_value;
    }
}
 