using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendersManager : MonoBehaviour
{
    private GameObject targetModel = null;
    [SerializeField] private Transform modelLocation;

    //Function that sets up model for render textures
    public void RenderModel(GameObject target)
    {
        //destroy previous model
        if (targetModel != null)
        {
            Destroy(targetModel);
        }

        //instantiate target as new model
        targetModel = Instantiate(targetModel, modelLocation);

        //size up model for render
        targetModel.transform.localScale *= 2;
    }
}
