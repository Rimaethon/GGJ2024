using UnityEngine;

namespace BlazeAISpace
{
    public class HitStateBehaviour : MonoBehaviour
    {   
        [Header("ANIMATIONS"), Tooltip("Hit animation name.")]
        public string hitAnim;
        [Min(0), Tooltip("The animation transition from current animation to the hit animation.")]
        public float hitAnimT = 0.2f;
        [Min(0), Tooltip("The gap time between replaying the hit animations to avoid having the animation play on every single hit which may look bad on very fast and repitive attacks such as a machine gun.")]
        public float hitAnimGap = 0.3f;


        [Header("DURATION"), Min(0), Tooltip("The duration of the hit state.")]
        public float hitDuration = 1;
        
        
        [Header("ATTACK OPTION"), Tooltip("If set to true will cancel the attack if got hit.")]
        public bool cancelAttackOnHit;
        
        
        [Header("AUDIO"), Tooltip("Play audio when hit. Set your audios in the audio scriptable in the General Tab in Blaze AI.")]
        public bool playAudio;

        
        [Header("CALL OTHERS"), Min(0), Tooltip("The radius to call other AIs when hit. You use this by calling blaze.Hit(player, true).")]
        public float callOthersRadius = 5;
        [Tooltip("The layers of the agents to call. You use this by calling blaze.Hit(player, true).")]
        public LayerMask agentLayersToCall;
        [Tooltip("Shows the call radius as a cyan wire sphere in the scene view.")]
        public bool showCallRadius;


        BlazeAI blaze;
        float _duration = 0;
        float _gapTimer = 0;
        bool playedAudio;


        #region UNITY METHODS

        void Start()
        {
            blaze = GetComponent<BlazeAI>();    

            // force shut if not the same state
            if (blaze.state != BlazeAI.State.hit) {
                enabled = false;
            }
        }


        void OnDisable()
        {
            ResetTimers();
            blaze.hitEnemy = null;
            playedAudio = false;
        }


        // validate properties
        void OnValidate()
        {
            // hit duration can't be smaller than the hit animation gap
            if (hitDuration < hitAnimGap) {
                hitAnimGap = hitDuration;
            }
        }


        // show the call others radius
        void OnDrawGizmosSelected() 
        {
            if (!showCallRadius) {
                return;
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, callOthersRadius);
        }


        // hit behaviour here
        void Update()
        {
            // check if a hit was registered
            if (blaze.hitRegistered) {
                blaze.hitRegistered = false;

                if (_duration == 0) {
                    blaze.animManager.Play(hitAnim, hitAnimT, true);
                }
                else {
                    if (_gapTimer >= hitAnimGap) {
                        blaze.animManager.Play(hitAnim, hitAnimT, true);
                        _gapTimer = 0;
                    }
                }
                
                _duration = 0;

                
                // call others
                if (blaze.callOthersOnHit) {
                    CallOthers();
                }
            }


            // play hit audio
            PlayAudio();


            // cancel attacks
            if (cancelAttackOnHit) {
                blaze.StopAttack();
            }


            _gapTimer += Time.deltaTime;


            // hit duration timer
            _duration += Time.deltaTime;

            if (_duration >= hitDuration) {
                FinishHitState();
            }
        }

        #endregion

        #region BEHAVIOUR METHODS

        // exit hit state and turn to either alert or attack state
        void FinishHitState()   
        {
            ResetTimers();

            
            // if AI was in cover -> return to cover state
            if (blaze.hitWhileInCover && blaze.coverShooterMode) {
                blaze.SetState(BlazeAI.State.goingToCover);
                return;
            }


            // if the enemy that did the hit is passed -> set AI to go to enemy location
            if (blaze.hitEnemy) {
                // check the passed enemy isn't the same AI
                if (blaze.hitEnemy.transform.IsChildOf(transform)) {
                    blaze.ChangeState("alert");
                    return;
                }
                
                blaze.SetEnemy(blaze.hitEnemy);
                return;
            }


            // if not -> turn alert
            blaze.ChangeState("alert");
        }


        // play the hit audio
        void PlayAudio()
        {
            if (!playAudio) {
                return;
            }
            
            if (playedAudio) {
                return;
            }

            if (blaze.IsAudioScriptableEmpty()) {
                return;
            }

            if (blaze.PlayAudio(blaze.audioScriptable.GetAudio(AudioScriptable.AudioType.Hit))) {
                playedAudio = true;
            }
        }


        // call others
        void CallOthers()
        {
            Collider[] agentsColl = new Collider[5];
            int agentsCollNum = Physics.OverlapSphereNonAlloc(transform.position + blaze.centerPosition, callOthersRadius, agentsColl, agentLayersToCall);
        
            for (int i=0; i<agentsCollNum; i++) {
                BlazeAI script = agentsColl[i].GetComponent<BlazeAI>();

                // if caught collider is that of the same AI -> skip
                if (agentsColl[i].transform.IsChildOf(transform)) {
                    continue;
                }

                
                // if script doesn't exist -> skip
                if (script == null) {
                    continue;
                }


                // reaching this point means current item is a valid AI

                if (blaze.hitEnemy) {
                    script.SetEnemy(blaze.hitEnemy, true, true);
                    continue;
                }
                

                // if no enemy has been passed
                // make it a random point within the destination
                Vector3 randomPoint = script.RandomSpherePoint(transform.position);
                
                script.ChangeState("alert");
                script.MoveToLocation(randomPoint);
            }
        }


        // reset the timers of hit duration
        void ResetTimers()
        {
            _duration = 0;
            _gapTimer = 0;
        }

        #endregion
    }
}
