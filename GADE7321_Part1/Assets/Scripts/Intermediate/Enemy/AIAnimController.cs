using UnityEngine;

namespace Intermediate.Enemy
{
    public class AIAnimController : MonoBehaviour
    {
        public Animator animController;

        public void PlayRunningAnim(float speed)
        {
            animController.SetFloat("Speed", speed);
        }

        public void PlayAttackingAnim(bool isPunching)
        {
            animController.SetBool("IsPunching", isPunching);
        }

        public void PlayEvadeAnim(bool isSprinting)
        {
            animController.SetBool("IsSprinting", isSprinting);
        }
    }
}