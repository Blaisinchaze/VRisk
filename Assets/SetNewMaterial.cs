using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewMaterial : MonoBehaviour
{
    public MeshRenderer meshRend;

    public enum MaterialSelection : int
    {
        CYAN,
        GREEN,
        GREY,
        PINK,
        WHITE,
        YELLOW
    }

    private MaterialSelection selectedMaterial;
    [SerializeField] private Material cyanMat;
    [SerializeField] private Material greenMat;
    [SerializeField] private Material greyMat;
    [SerializeField] private Material pinkMat;
    [SerializeField] private Material whiteMat;
    [SerializeField] private Material yellowMat;
    
    public void setMat(MaterialSelection selection)
    {
        selectedMaterial = selection;
    }

    public MaterialSelection getMat()
    {
        return selectedMaterial;
    }

    //This is horrible. I hate it
    public void ChangeMaterial()
    {
        meshRend.material = selectedMaterial switch
        {
            MaterialSelection.CYAN => cyanMat,
            MaterialSelection.GREEN => greenMat,
            MaterialSelection.GREY => greyMat,
            MaterialSelection.PINK => pinkMat,
            MaterialSelection.WHITE => whiteMat,
            MaterialSelection.YELLOW => yellowMat,
            _ => meshRend.material
        };
    }
}
