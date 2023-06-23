using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
     public enum CameraAction
     {
          NONE,
          PAN,
          ZOOM,
          ROTATE,
          MOVE
     }
     
     public float panning_speed;
     public float zoom_speed;
     public float rotate_speed;
     public float move_speed;
     public float speed_modifier;

     public CameraAction current_action;
     
     private Input.DataVisualiserInputMapActions action_map;
     private Dictionary<string, CameraAction> string_to_camera_dictionary = new Dictionary<string, CameraAction>
     {
          {"PanView", CameraAction.PAN}, {"ZoomView", CameraAction.ZOOM}, {"RotateView", CameraAction.ROTATE},
          {"MoveView", CameraAction.MOVE}
     };

     private void Start()
     {
          action_map = GameManager.Instance.InputHandler.input_asset.DataVisualiserInputMap;

          action_map.PanView.started += setAction;
          action_map.PanView.performed += setAction;
          action_map.PanView.canceled += setAction;
          
          action_map.ZoomView.started += setAction;
          action_map.ZoomView.performed += setAction;
          action_map.ZoomView.canceled += setAction;
          
          action_map.RotateView.started += setAction;
          action_map.RotateView.performed += setAction;
          action_map.RotateView.canceled += setAction;
          
          action_map.MoveView.started += setAction;
          action_map.MoveView.performed += setAction;
          action_map.MoveView.canceled += setAction;
     }

     private void setAction(InputAction.CallbackContext _context)
     {
          CameraAction action = string_to_camera_dictionary.TryGetValue(_context.action.name, out var value) 
               ? value : CameraAction.NONE;
          
          bool action_canceled = isActionCanceled(_context);

          switch (_context.phase)
          {
               case InputActionPhase.Performed:
               case InputActionPhase.Started:
                    if (action_canceled)
                    {
                         current_action = current_action == action ? CameraAction.NONE : current_action;
                         break;
                    }
                    
                    current_action = action;
                    break;
               
               case InputActionPhase.Canceled:
               case InputActionPhase.Disabled:
                    current_action = current_action == action ? CameraAction.NONE : current_action;
                    break;
               
               default:
                    throw new ArgumentOutOfRangeException();
          }
     }
     
     private bool isActionCanceled(InputAction.CallbackContext _context)
     {
          var value_type = _context.action.expectedControlType;
          
          if (value_type == nameof(Vector3))
          {
               var value = _context.ReadValue<Vector3>();
               return value.sqrMagnitude < 0.01f;
          }
          if (value_type == "Axis")
          {
               var value = _context.ReadValue<float>();
               return value == 0;
          }

          return false;
     }

     private void Update()
     {
          Debug.Log(current_action);
     }
}
