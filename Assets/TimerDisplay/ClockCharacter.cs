using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClockCharacter : MonoBehaviour
{
    public enum SegmentType
    {
        TOP_LEFT,
        TOP_MIDDLE,
        TOP_RIGHT,
        MIDDLE_MIDDLE,
        BOTTOM_LEFT,
        BOTTOM_MIDDLE,
        BOTTOM_RIGHT
    }
    
    public enum Character
    {
        ZERO,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE, 
        SIX,
        SEVEN,
        EIGHT,
        NINE
    }
    
    public SerializedDictionary<SegmentType, GameObject> segments;
    public ClockCharacterSegmentsMap character_segments_map;
    public Character thing;

    public void setCharacter(Character _character)
    {
        var char_segments = character_segments_map.characters[_character];
        
        for (int i = 0; i < 7; ++i)
        {
            SegmentType current_segment = (SegmentType)i;

            if (char_segments.Contains(current_segment))
            {
                segments[current_segment].SetActive(true);
            }
            else
            {
                segments[current_segment].SetActive(false);
            }
        }
    }

    private void Start()
    {
        GameManager.Instance.InputHandler.input_asset.InputActionMap.Debug.started += test;
    }

    private void test(InputAction.CallbackContext _context)
    {
        setCharacter(thing);
    }
}
