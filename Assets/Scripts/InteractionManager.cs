using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InteractionManager : MonoBehaviour
{
    private MechInfo mechInfo = null; //stored information

    [SerializeField] private Camera arCamera; //camera used in application
    [SerializeField] private UIManager uiManager; //manager that handles all UI functions
    [SerializeField] private RendersManager rendersManager; //manager that manages getting render textures for UI
    [SerializeField] private LayerMask mechLayer;

    private void Update()
    {
        //check if screen has been touched
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //check that input was start of touch input
            if (touch.phase == TouchPhase.Began)
            {
                //get ray to cast
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                //perform raycast
                if (Physics.Raycast(ray, out RaycastHit hit, mechLayer))
                {
                    mechInfo = hit.collider.GetComponent<MechInfo>();//retreiving info
                    rendersManager.RenderModel(mechInfo.renderModel);//call setup for reder moodel of mech
                    uiManager.UpdateInfo(mechInfo.infoFields);//update UI to display selected mech info
                    uiManager.SwitchUI();//toggle UI to info menu
                }
            }
        }
    }
}
