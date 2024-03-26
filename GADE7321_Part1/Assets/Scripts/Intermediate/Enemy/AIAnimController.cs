using UnityEngine;

namespace Intermediate.Enemy
{
    public class AIAnimController : MonoBehaviour
    {
        public Animator animController;

        public void PlayRunningAnim(float speed) //Play run anim
        {
            animController.SetFloat("Speed", speed);
        }

        public void PlayAttackingAnim(bool isPunching) //Play punch anim
        {
            animController.SetBool("IsPunching", isPunching);
        }

        public void PlayEvadeAnim(bool isSprinting) //Play sprint anim
        {
            animController.SetBool("IsSprinting", isSprinting);
        }
    }
}