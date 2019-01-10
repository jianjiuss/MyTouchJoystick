using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonA : MonoBehaviour
{
    public MeshRenderer cubeMesh;
   
    private int invert = 1;

    private void Start()
    {
    }

    public void OnClick()
    {

        invert++;

        if(invert % 2 == 0)
        {
            cubeMesh.material.color = Color.red;
        }
        else
        {
            cubeMesh.material.color = Color.white;
        }
    }
}
