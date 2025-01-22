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
    [SerializeField] private RendersManager rendersManager; //manager that manages getting render textures for UI
    [SerializeField] private LayerMask mechLayer;

    //controls
    [SerializeField] private InputAction press, screenPos;
    private Vector3 cursorScreenPos;

    private void Awake()
    {
        press.Enable();
        screenPos.Enable();
        screenPos.performed += context => { cursorScreenPos = context.ReadValue<Vector2>(); }; //stores the press / mouse position
        press.performed += OnTouch;
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        //check that input was start of touch input
        if (context.performed)
        {
            //Debug.Log("Touch activated");

            //get ray to cast
            Ray ray = Camera.main.ScreenPointToRay(cursorScreenPos);

            
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

