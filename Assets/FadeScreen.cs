using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fade_on_start = true;
    public float fade_duration = 2;
    public Color fade_color;
    private Renderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if(fade_on_start) FadeIn();
    }

    public void FadeIn()
    {
        Fade(1,0);
    }
    
    public void FadeOut()
    {
        Fade(0,1);
    }

    public void Fade(float _alpha_in, float _alpha_out)
    {
        StartCoroutine(FadeRoutine(_alpha_in, _alpha_out));
    }

    public IEnumerator FadeRoutine(float _alpha_in, float _alpha_out)
    {
        float timer = 0;

        while (timer <= fade_duration)
        {
            Color new_color = fade_color;
            new_color.a = Mathf.Lerp(_alpha_in, _alpha_out, timer / fade_duration);
            
            rend.material.SetColor("_Color", new_color);
            
            timer += Time.deltaTime;
            yield return null;
        }
        
        Color new_color2 = fade_color;
        new_color2.a = _alpha_out;
        rend.material.SetColor("_Color", new_color2);
    }
}
