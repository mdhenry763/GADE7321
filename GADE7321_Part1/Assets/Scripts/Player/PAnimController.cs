using UnityEngine;

namespace Player
{
    public class PAnimController : MonoBehaviour
    {
        [Header("Animations: ")] 
        public Animator anim;

        public string fightingAnim;
        public string runAnim;

        public void RunAnim(float speed)
        {
            anim.SetFloat(runAnim, speed);
        }

        public void PunchAnim(bool isPunching)
        {
            anim.SetBool(fightingAnim, isPunching);
        }
        
    }
}