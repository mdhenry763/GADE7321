using UnityEngine;

namespace Player
{
    public class PAnimController : MonoBehaviour
    {
        [Header("Animations: ")] 
        public Animator anim;

        public string fightingAnim;
        public string runAnim;

        public void RunAnim(float speed) //Play Run ANim
        {
            anim.SetFloat(runAnim, speed);
        }

        public void PunchAnim() //Play Punch Anim
        {
            anim.SetTrigger(fightingAnim);
        }
        
    }
}