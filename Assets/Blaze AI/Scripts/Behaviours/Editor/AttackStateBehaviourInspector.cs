using UnityEditor;

namespace BlazeAISpace
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AttackStateBehaviour))]

    public class AttackStateBehaviourInspector : Editor
    {
        SerializedProperty moveSpeed,
        turnSpeed,

        idleAnim,
        moveAnim,
        idleMoveT,

        distanceFromEnemy,
        attackDistance,
        layersCheckOnAttacking,
        attacks,
        attackEvent,

        attackInIntervals,
        attackInIntervalsTime,

        playAttackIdleAudios,
        attackIdleAudiosTime,

        callOthers,
        callRadius,
        agentLayersToCall,
        callOthersTime,
        showCallRadius,
        receiveCallFromOthers,

        moveBackwards,
        moveBackwardsDistance,
        moveBackwardsSpeed,
        moveBackwardsAnim,
        moveBackwardsAnimT,

        turnToTarget,
        turnSensitivity,
        useTurnAnims,

        strafe,
        strafeDirection,
        strafeSpeed,
        strafeTime,
        strafeWaitTime,
        leftStrafeAnim,
        rightStrafeAnim,
        strafeAnimT,
        strafeLayersToAvoid,

        onAttackRotate,
        onAttackRotateSpeed,

        searchLocationRadius,
        timeToStartSearch,
        searchPoints,
        searchPointAnim,
        pointWaitTime,
        endSearchAnim,
        endSearchAnimTime,
        searchAnimsT,
        playAudioOnSearchStart,
        playAudioOnSearchEnd,

        returnPatrolAnim,
        returnPatrolAnimT,
        returnPatrolTime,
        playAudioOnReturnPatrol;


        bool displayAttackEvents = true;


        void OnEnable()
        {
            moveSpeed = serializedObject.FindProperty("moveSpeed");
            turnSpeed = serializedObject.FindProperty("turnSpeed");

            idleAnim = serializedObject.FindProperty("idleAnim");
            moveAnim = serializedObject.FindProperty("moveAnim");
            idleMoveT = serializedObject.FindProperty("idleMoveT");

            distanceFromEnemy = serializedObject.FindProperty("distanceFromEnemy");
            attackDistance = serializedObject.FindProperty("attackDistance");
            layersCheckOnAttacking = serializedObject.FindProperty("layersCheckOnAttacking");
            attacks = serializedObject.FindProperty("attacks");
            attackEvent = serializedObject.FindProperty("attackEvent");

            attackInIntervals = serializedObject.FindProperty("attackInIntervals");
            attackInIntervalsTime = serializedObject.FindProperty("attackInIntervalsTime");

            playAttackIdleAudios = serializedObject.FindProperty("playAttackIdleAudios");
            attackIdleAudiosTime = serializedObject.FindProperty("attackIdleAudiosTime");

            callOthers = serializedObject.FindProperty("callOthers");
            callRadius = serializedObject.FindProperty("callRadius");
            agentLayersToCall = serializedObject.FindProperty("agentLayersToCall");
            callOthersTime = serializedObject.FindProperty("callOthersTime");
            showCallRadius = serializedObject.FindProperty("showCallRadius");
            receiveCallFromOthers = serializedObject.FindProperty("receiveCallFromOthers");

            moveBackwards = serializedObject.FindProperty("moveBackwards");
            moveBackwardsDistance = serializedObject.FindProperty("moveBackwardsDistance");
            moveBackwardsSpeed = serializedObject.FindProperty("moveBackwardsSpeed");
            moveBackwardsAnim = serializedObject.FindProperty("moveBackwardsAnim");
            moveBackwardsAnimT = serializedObject.FindProperty("moveBackwardsAnimT");

            turnToTarget = serializedObject.FindProperty("turnToTarget");
            turnSensitivity = serializedObject.FindProperty("turnSensitivity");
            useTurnAnims = serializedObject.FindProperty("useTurnAnims");

            strafe = serializedObject.FindProperty("strafe");
            strafeDirection = serializedObject.FindProperty("strafeDirection");
            strafeSpeed = serializedObject.FindProperty("strafeSpeed");
            strafeTime = serializedObject.FindProperty("strafeTime");
            strafeWaitTime = serializedObject.FindProperty("strafeWaitTime");
            leftStrafeAnim = serializedObject.FindProperty("leftStrafeAnim");
            rightStrafeAnim = serializedObject.FindProperty("rightStrafeAnim");
            strafeAnimT = serializedObject.FindProperty("strafeAnimT");
            strafeLayersToAvoid = serializedObject.FindProperty("strafeLayersToAvoid");

            onAttackRotate = serializedObject.FindProperty("onAttackRotate");
            onAttackRotateSpeed = serializedObject.FindProperty("onAttackRotateSpeed");

            searchLocationRadius = serializedObject.FindProperty("searchLocationRadius");
            timeToStartSearch = serializedObject.FindProperty("timeToStartSearch");
            searchPoints = serializedObject.FindProperty("searchPoints");
            searchPointAnim = serializedObject.FindProperty("searchPointAnim");
            pointWaitTime = serializedObject.FindProperty("pointWaitTime");
            endSearchAnim = serializedObject.FindProperty("endSearchAnim");
            endSearchAnimTime = serializedObject.FindProperty("endSearchAnimTime");
            searchAnimsT = serializedObject.FindProperty("searchAnimsT");
            playAudioOnSearchStart = serializedObject.FindProperty("playAudioOnSearchStart");
            playAudioOnSearchEnd = serializedObject.FindProperty("playAudioOnSearchEnd");

            returnPatrolAnim = serializedObject.FindProperty("returnPatrolAnim");
            returnPatrolAnimT = serializedObject.FindProperty("returnPatrolAnimT");
            returnPatrolTime = serializedObject.FindProperty("returnPatrolTime");
            playAudioOnReturnPatrol = serializedObject.FindProperty("playAudioOnReturnPatrol");
        }


        public override void OnInspectorGUI () 
        {
            AttackStateBehaviour script = (AttackStateBehaviour) target;
            int spaceBetween = 20;


            EditorGUILayout.LabelField("SPEEDS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(moveSpeed);
            EditorGUILayout.PropertyField(turnSpeed);


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("ANIMATIONS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(idleAnim);
            EditorGUILayout.PropertyField(moveAnim);
            EditorGUILayout.PropertyField(idleMoveT);


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("ATTACKING & DISTANCES", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(distanceFromEnemy);
            EditorGUILayout.PropertyField(attackDistance);
            EditorGUILayout.PropertyField(layersCheckOnAttacking);
            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(attacks);
            EditorGUILayout.Space(5);
            displayAttackEvents = EditorGUILayout.Toggle("Display Attack Events", displayAttackEvents);
            if (displayAttackEvents) {
                EditorGUILayout.PropertyField(attackEvent);
            }


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("INTERVAL ATTACKS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(attackInIntervals);
            if (script.attackInIntervals) {
                EditorGUILayout.PropertyField(attackInIntervalsTime);
            }


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("ATTACK-IDLE AUDIO", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(playAttackIdleAudios);
            if (script.playAttackIdleAudios) {
                EditorGUILayout.PropertyField(attackIdleAudiosTime);
            }


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("CALLING OTHERS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(callOthers);
            if (script.callOthers) {
                EditorGUILayout.PropertyField(callRadius);
                EditorGUILayout.PropertyField(agentLayersToCall);
                EditorGUILayout.PropertyField(callOthersTime);
                EditorGUILayout.PropertyField(showCallRadius);
                EditorGUILayout.PropertyField(receiveCallFromOthers);
            }


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("MOVE BACKWARDS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(moveBackwards);
            if (script.moveBackwards) {
                EditorGUILayout.PropertyField(moveBackwardsDistance);
                EditorGUILayout.PropertyField(moveBackwardsSpeed);
                EditorGUILayout.PropertyField(moveBackwardsAnim);
                EditorGUILayout.PropertyField(moveBackwardsAnimT);
            }


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("TURNING TO TARGET", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(turnToTarget);
            if (script.turnToTarget) {
                EditorGUILayout.PropertyField(turnSensitivity);
                EditorGUILayout.PropertyField(useTurnAnims);
            }


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("STRAFING", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(strafe);
            if (script.strafe) {
                EditorGUILayout.PropertyField(strafeDirection);
                EditorGUILayout.PropertyField(strafeSpeed);
                EditorGUILayout.PropertyField(strafeTime);
                EditorGUILayout.PropertyField(strafeWaitTime);
                EditorGUILayout.PropertyField(leftStrafeAnim);
                EditorGUILayout.PropertyField(rightStrafeAnim);
                EditorGUILayout.PropertyField(strafeAnimT);
                EditorGUILayout.PropertyField(strafeLayersToAvoid);
            }


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("ATTACK ROTATE", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(onAttackRotateSpeed);
            EditorGUILayout.PropertyField(onAttackRotate);


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("SEARCHING LOCATION", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(searchLocationRadius);
            if (script.searchLocationRadius) {
                EditorGUILayout.PropertyField(timeToStartSearch);
                EditorGUILayout.PropertyField(searchPoints);
                EditorGUILayout.PropertyField(searchPointAnim);
                EditorGUILayout.PropertyField(pointWaitTime);
                EditorGUILayout.PropertyField(endSearchAnim);
                EditorGUILayout.PropertyField(endSearchAnimTime);
                EditorGUILayout.PropertyField(searchAnimsT);
                EditorGUILayout.PropertyField(playAudioOnSearchStart);
                EditorGUILayout.PropertyField(playAudioOnSearchEnd);
            }


            if (!script.searchLocationRadius) {
                EditorGUILayout.Space(spaceBetween);
                EditorGUILayout.LabelField("RETURNING TO PATROL", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(returnPatrolAnim);
                EditorGUILayout.PropertyField(returnPatrolAnimT);
                EditorGUILayout.PropertyField(returnPatrolTime);
                EditorGUILayout.PropertyField(playAudioOnReturnPatrol);
            }


            serializedObject.ApplyModifiedProperties();
        }
    }
}
