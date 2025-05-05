using UnityEngine;

public class PlayerAnimationController2 : MonoBehaviour
{
    public Animator animator;

    
    private bool isInJump = false;
    private float jumpTimer = 0f;
    private float jumpDuration = 1f; // Dur�e fixe du saut en secondes

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

        // Lire les axes (assure-toi d'avoir modifi� l'Input Manager pour AZERTY)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        // D�tecter si la touche Shift est enfonc�e pour la course
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // D�clenchement du saut
        if (Input.GetKeyDown(KeyCode.Space) && !isInJump)
        {
            animator.Play("Root|jump_up_root_motion");
            isInJump = true;
            jumpTimer = jumpDuration;
            
            return;
        }

        // Gestion du mouvement et de la course
        if (isMoving)
        {
            if (isRunning)
            {
                animator.Play("Root|run"); // Animation de course
            }
            else
            {
                animator.Play("Root|walk"); // Animation de marche
            }
            
        }

    }
}
