using UnityEngine;

namespace BlazeAISpace
{
    public class AnimationManager
    {
        Animator anim;
        string currentState;
        float animSpeed = 1f;


        // constructor
        public AnimationManager (Animator animator)
        {
            anim = animator;
        }


        // actual animation playing function
        public void Play(string state, float time = 0.25f, bool overplay = false)
        {
            if (state == currentState) return;

            
            // check if passed animation name doesn't exist in the Animator
            if (!CheckAnimExists(state)) {
                if (state.Length > 0) {
                    anim.enabled = false;
                    Debug.LogWarning($"The animation name: {state} - doesn't exist and has been ignored. Please re-check your animation names.");
                }
                else {
                    anim.enabled = true;
                    Debug.LogWarning("No animation set.");
                }
                
                return;
            }

            
            anim.enabled = true;
            anim.CrossFadeInFixedTime(state, time, 0);


            if (overplay) currentState = "";
            else currentState = state;
        }


        // check whether the passed animation name exists or not
        public bool CheckAnimExists(string animName)
        {
            if (animName.Length <= 0 || animName == null) {
                return false;
            }

            return anim.HasState(0, Animator.StringToHash(animName));
        }
    }
}
