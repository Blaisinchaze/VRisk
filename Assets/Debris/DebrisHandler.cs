using System;
using UnityEngine;

public class DebrisHandler : MonoBehaviour
{
    public enum DebrisType
    {
        BRICK,
    }

    public enum Direction
    {
        FORWARD,
        BACKWARD, 
        LEFT, 
        RIGHT
    }

    public Pair<DebrisType, GameObject> debris_prefabs;

    public void createDebris(DebrisType _type, int _building_id)
    {
        /*
         * Notes:
         * need to selected building by id.
         * select the wall by local forward (or other)
         * and eject debris at a point within that wall
         * determined by an x and y percentage cross wall.
         * 
         * Need to randomly generate the data determining
         * which building, which wall, at what location,
         * at what time and store it within a scriptable object?
         *
         * tie into timeline manager?
         *
         * Yes can use a scriptable object,
         * can also create a custom EditorWindow for populating it.
         * Can randomise debris amounts and locations.
         * amounts dependent on risk type.
         */
    }
}
