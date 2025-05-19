using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerUIInput : MonoBehaviour, PlayerControls.IUIActions
{
    public PlayerControls PlayerControls { get; private set; }

    public bool ConfirmInput { get; private set; }

    #region Startup

    private void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.UI.Enable();
        PlayerControls.UI.SetCallbacks(this);
    }

    private void OnDisable()
    {
        PlayerControls.UI.Disable();
        PlayerControls.UI.RemoveCallbacks(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    #endregion

    #region Update Logic

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        ConfirmInput = false;
    }

    #endregion

    #region Input Callbacks
    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        ConfirmInput = !ConfirmInput;
    }
    #endregion
}
