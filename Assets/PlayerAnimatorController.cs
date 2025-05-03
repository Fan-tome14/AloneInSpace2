using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    private float idleTimer = 0f;
    private bool isInJump = false;

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Si on est en train de sauter
        if (isInJump)
        {
            // Si l'animation de saut est finie, on arrête le saut
            if (stateInfo.IsName("Rig|jump") && stateInfo.normalizedTime >= 1f)
            {
                isInJump = false;
            }
            else
            {
                return; // Tant que le saut est en cours, ne rien faire d'autre
            }
        }

        // Lire les axes (assure-toi d'avoir modifié l'Input Manager pour AZERTY)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        // Gestion du saut
        if (Input.GetKeyDown(KeyCode.Space) && !isInJump)
        {
            animator.Play("Rig|jump");
            isInJump = true;
            idleTimer = 0f;
            return;
        }

        // Gestion du mouvement
        if (isMoving)
        {
            animator.Play("Rig|walk");
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= 5f)
                animator.Play("Rig|idle");
            else
                animator.Play("Rig|Static_Pose");
        }
    }
}
