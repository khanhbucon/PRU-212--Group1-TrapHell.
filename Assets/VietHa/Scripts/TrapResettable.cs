
using UnityEngine;

public class TrapResettable : MonoBehaviour
{
    private TrapController trapController;

    private void Awake()
    {
        trapController = GetComponent<TrapController>();
    }

    private void OnEnable()
    {
        PlayerControllerr.OnPlayerDeath += ResetTrap;
    }

    private void OnDisable()
    {
        PlayerControllerr.OnPlayerDeath -= ResetTrap;
    }

    private void ResetTrap()
    {
        if (trapController != null)
        {
            trapController.ResetTrap();
        }
    }
}
