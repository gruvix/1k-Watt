using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Client-side representation of a floor switch.
/// </summary>
[RequireComponent(typeof(NetworkFloorSwitchState))]
public class ClientFloorSwitchVisualization : NetworkBehaviour
{
    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private string m_AnimatorPressedDownBoolVarName = "IsPressed";

    private NetworkFloorSwitchState m_FloorSwitchState;

    private void Awake()
    {
        m_FloorSwitchState = GetComponent<NetworkFloorSwitchState>();
    }

    public override void OnNetworkSpawn()
    {
        m_FloorSwitchState.IsSwitchedOn.OnValueChanged += OnFloorSwitchStateChanged;
    }

    public override void OnNetworkDespawn()
    {
        if (m_FloorSwitchState)
        {
            m_FloorSwitchState.IsSwitchedOn.OnValueChanged -= OnFloorSwitchStateChanged;
        }
    }

    private void OnFloorSwitchStateChanged(bool wasPressed, bool isPressed)
    {
        m_Animator.SetBool(m_AnimatorPressedDownBoolVarName, isPressed);
    }

}
