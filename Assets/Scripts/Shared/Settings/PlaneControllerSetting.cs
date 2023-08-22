using Assets.Scripts.Shared;
using System;
using UnityEngine;

public enum ControllerType
{
    // Dieu khien bang ban phim. Phim WASD, Phim mui ten
    Keyboard,
    
    // Dieu khien bang joystick Ao
    VirtualJoystick,

    // Dieu khien bang joystick ao theo huong ngang
    VirtualJoystickHorizontal,

    // Dieu khien bang viec cham va keo tren man hinh
    TouchAndDrag
}

public class PlaneControllerSetting : SingletonMonoBehaviour<PlaneControllerSetting>
{
    [Header("Controller")]
    public ControllerType controllerType;

    #region Joystick
    [Header("------ Joystick ------")]
    public VirtualJoystick joystick;

    // Neu bien nay sai, player se nhin ve phia truoc
    public bool lookAtByDirection = true;

    // Bien nay se duoc kich hoat khi lookAtByDirection = true
    public float turnSpeed = 7.0f;
    #endregion

    #region Horizontal Drag
    [Header("------ Horizontal Drag ------")]
    public HorizontalDragArea horizontalJoystick;

    // Toc do se bi loai bo khi bien duoi day bang true
    public bool allowTeleport = false;
    #endregion

    #region Touch And Drag
    [Header("------ Touch And Drag ------")]
    public TouchDragController touchDragController;
    public bool onlyXMovement;
    #endregion

    #region Keyboard
    [Header("------ Keyboard ------")]
    public KeyboardController keyboardController;
	#endregion

	private void OnEnable()
	{
        ApplyControllerType();
	}

	private void ApplyControllerType()
	{
        // Disable all controllers
        keyboardController.gameObject.SetActive(false);
        joystick.gameObject.SetActive(false);
        horizontalJoystick.gameObject.SetActive(false);
        touchDragController.gameObject.SetActive(false);

        // Enable the specific controller
        switch (controllerType)
        {
            case ControllerType.Keyboard:
                keyboardController.gameObject.SetActive(true);
                break;
            case ControllerType.VirtualJoystick:
                joystick.gameObject.SetActive(true);
                break;
            case ControllerType.VirtualJoystickHorizontal:
                horizontalJoystick.gameObject.SetActive(true);
                break;
            case ControllerType.TouchAndDrag:
                touchDragController.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
