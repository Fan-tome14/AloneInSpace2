using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    private float idleTimer = 0f;
    private bool isInJump = false;
    private float jumpTimer = 0f;
    private float jumpDuration = 1f; // Durée fixe du saut en secondes

    void Update()
    {
        // Gestion du timer de saut
        if (isInJump)
        {
            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0f)
            {
                isInJump = false; // Fin du saut
            }
            else
            {
                return; // Tant que le saut est en cours, on bloque tout le reste
            }
        }

        // Lire les axes (assure-toi d'avoir modifié l'Input Manager pour AZERTY)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        // Détecter si la touche Shift est enfoncée pour la course
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Déclenchement du saut
        if (Input.GetKeyDown(KeyCode.Space) && !isInJump)
        {
            animator.Play("Rig|jump");
            isInJump = true;
            jumpTimer = jumpDuration;
            idleTimer = 0f;
            return;
        }

        // Gestion du mouvement et de la course
        if (isMoving)
        {
            if (isRunning)
            {
                animator.Play("Rig|run"); // Animation de course
            }
            else
            {
                animator.Play("Rig|walk"); // Animation de marche
            }
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
