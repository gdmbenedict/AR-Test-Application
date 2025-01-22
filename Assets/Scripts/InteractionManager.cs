using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class InteractionManager : MonoBehaviour
{
    private MechInfo mechInfo = null; //stored information

    [SerializeField] private Camera arCamera; //camera used in application
    [SerializeField] private UIManager uiManager; //manager that handles all UI functions
    [SerializeField] private GameObject rendersManagerObject; //object used to enable or disable the renders manager and attached camera
    [SerializeField] private RendersManager rendersManager; //manager that manages getting render textures for UI
    [SerializeField] private LayerMask mechLayer; // layermask mech models are on

    public void OnTouch(InputAction.CallbackContext context)
    {
        //check that input was start of touch input
        if (context.performed && !uiManager.getInfoScreenOn())
        {
            Debug.Log("Touch activated");

            //get ray to cast
            Vector3 screenPos = context.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            
            //perform raycast
            if (Physics.Raycast(ray, out RaycastHit hit, mechLayer))
            {
                mechInfo = hit.collider.GetComponent<MechInfo>();//retreiving info
                rendersManagerObject.SetActive(true); //turning on renders and render textures
                rendersManager.RenderModel(mechInfo.renderModel);//call setup for reder moodel of mech
                uiManager.UpdateInfo(mechInfo.infoFields);//update UI to display selected mech info
                uiManager.SwitchUI();//toggle UI to info menu
            }
            
        }

    }
}

