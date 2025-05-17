using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerGameplayInput : MonoBehaviour, PlayerControls.IGameplayActions
{
    public PlayerControls PlayerControls { get; private set; }
    public Vector2 MovementInput { get; private set; }

    public Vector2 LookInput { get; private set; }

    public bool AttackInput { get; private set; }

    #region Startup

    private void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.Gameplay.Enable();
        PlayerControls.Gameplay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        PlayerControls.Gameplay.Disable();
        PlayerControls.Gameplay.RemoveCallbacks(this);
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
        AttackInput = false;
    }

    #endregion

    #region Input Callbacks
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        AttackInput = !AttackInput;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }
    #endregion
}
