using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    private PlayerInput playerInput;

    private PlayerInput.OnGuiActions onGui;

    private UIManager uiManager;

    public bool isTutorial = true;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onGui = playerInput.OnGui;
        uiManager = GetComponent<UIManager>();
        onGui.Continue.performed += ctx => uiManager.changeGui();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && isTutorial)
        {
            InputMovement playerMotion = player.GetComponent<InputMovement>();
            if (playerMotion != null) playerMotion.enabled = false;
        }
    }
    
    private void OnEnable()
    {
        onGui.Enable();
    }

    private void OnDisable()
    {
        onGui.Disable();
    }
}
