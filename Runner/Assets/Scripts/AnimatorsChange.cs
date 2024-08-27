using UnityEngine;

public class AnimatorsChange : MonoBehaviour
{
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private AnimatorsAssetMenu assetMenu;
    [SerializeField] private Animator animator;

    private void Start()
    {
        playerInputController.RunEvent += OnRun;
        playerInputController.JumpEvent += OnJump;
    }

    private void OnDestroy()
    {
        playerInputController.RunEvent -= OnRun;
        playerInputController.JumpEvent -= OnJump;
    }
    

    private void OnRun()
    {
        ChangeController(assetMenu.animatorRun);
    }

    private void OnJump()
    {
        ChangeController(assetMenu.animatorJump);
    }

    private void ChangeController(RuntimeAnimatorController newRuntimeAnimatorController)
    {
        animator.runtimeAnimatorController = newRuntimeAnimatorController;
    }
}