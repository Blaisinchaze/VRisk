using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewMaterial : MonoBehaviour
{
    public Material newMat;
    public MeshRenderer meshRend;

    public void ChangeMaterial()
    {
        meshRend.material = newMat;
    }
}
