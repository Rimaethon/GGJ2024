using UnityEditor;

namespace BlazeAISpace 
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CoverShooterBehaviour))]
    public class CoverShooterBehaviourInspector : Editor
    {
        SerializedProperty moveSpeed,
        turnSpeed,
        idleAnim,
        moveAnim,
        idleMoveT,

        distanceFromEnemy,
        attackDistance,
        layersCheckOnAttacking,
        shootingAnim,
        shootingAnimT,
        shootEvent,
        shootEvery,
        singleShotDuration,
        delayBetweenEachShot,
        totalShootTime,

        firstSightDecision,
        coverBlownDecision,
        attackEnemyCover,

        braveMeter,

        callOthers,
        callRadius,
        showCallRadius,
        agentLayersToCall,
        callOthersTime,
        receiveCallFromOthers,

        moveBackwards,
        moveBackwardsDistance,
        moveBackwardsSpeed,
        moveBackwardsAnim,
        moveBackwardsAnimT,
        moveBackwardsAttack,

        strafe,
        strafeSpeed,
        strafeTime,
        strafeWaitTime,
        leftStrafeAnim,
        rightStrafeAnim,
        strafeAnimT,
        strafeLayersToAvoid,

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


        bool displayshootEvents = true;


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
            shootingAnim = serializedObject.FindProperty("shootingAnim");
            shootingAnimT = serializedObject.FindProperty("shootingAnimT");
            shootEvent = serializedObject.FindProperty("shootEvent");
            shootEvery = serializedObject.FindProperty("shootEvery");
            singleShotDuration = serializedObject.FindProperty("singleShotDuration");
            delayBetweenEachShot = serializedObject.FindProperty("delayBetweenEachShot");
            totalShootTime = serializedObject.FindProperty("totalShootTime");


            firstSightDecision = serializedObject.FindProperty("firstSightDecision");
            coverBlownDecision = serializedObject.FindProperty("coverBlownDecision");
            attackEnemyCover = serializedObject.FindProperty("attackEnemyCover");


            braveMeter = serializedObject.FindProperty("braveMeter");


            callOthers = serializedObject.FindProperty("callOthers");
            callRadius = serializedObject.FindProperty("callRadius");
            showCallRadius = serializedObject.FindProperty("showCallRadius");
            agentLayersToCall = serializedObject.FindProperty("agentLayersToCall");
            callOthersTime = serializedObject.FindProperty("callOthersTime");
            receiveCallFromOthers = serializedObject.FindProperty("receiveCallFromOthers");


            moveBackwards = serializedObject.FindProperty("moveBackwards");
            moveBackwardsDistance = serializedObject.FindProperty("moveBackwardsDistance");
            moveBackwardsSpeed = serializedObject.FindProperty("moveBackwardsSpeed");
            moveBackwardsAnim = serializedObject.FindProperty("moveBackwardsAnim");
            moveBackwardsAnimT = serializedObject.FindProperty("moveBackwardsAnimT");
            moveBackwardsAttack = serializedObject.FindProperty("moveBackwardsAttack");


            strafe = serializedObject.FindProperty("strafe");
            strafeSpeed = serializedObject.FindProperty("strafeSpeed");
            strafeTime = serializedObject.FindProperty("strafeTime");
            strafeWaitTime = serializedObject.FindProperty("strafeWaitTime");
            leftStrafeAnim = serializedObject.FindProperty("leftStrafeAnim");
            rightStrafeAnim = serializedObject.FindProperty("rightStrafeAnim");
            strafeAnimT = serializedObject.FindProperty("strafeAnimT");
            strafeLayersToAvoid = serializedObject.FindProperty("strafeLayersToAvoid");


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
            CoverShooterBehaviour script = (CoverShooterBehaviour) target;
            int spaceBetween = 20;
            

            EditorGUILayout.LabelField("MOVEMENT", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(moveSpeed);
            EditorGUILayout.PropertyField(turnSpeed);
            EditorGUILayout.PropertyField(idleAnim);
            EditorGUILayout.PropertyField(moveAnim);
            EditorGUILayout.PropertyField(idleMoveT);


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("ATTACKING", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(distanceFromEnemy);
            EditorGUILayout.PropertyField(attackDistance);
            EditorGUILayout.PropertyField(layersCheckOnAttacking);
            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(shootingAnim);
            EditorGUILayout.PropertyField(shootingAnimT);
            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(shootEvery);
            EditorGUILayout.PropertyField(singleShotDuration);
            EditorGUILayout.PropertyField(delayBetweenEachShot);
            EditorGUILayout.PropertyField(totalShootTime);
            EditorGUILayout.Space(5);
            displayshootEvents = EditorGUILayout.Toggle("Display Attack Events", displayshootEvents);
            if (displayshootEvents) {
                EditorGUILayout.PropertyField(shootEvent);
            }


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("DECISIONS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(firstSightDecision);
            EditorGUILayout.PropertyField(coverBlownDecision);
            EditorGUILayout.PropertyField(attackEnemyCover);


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("BRAVENESS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(braveMeter);


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("CALL OTHERS", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(callOthers);
            if (script.callOthers) {
                EditorGUILayout.PropertyField(callRadius);
                EditorGUILayout.PropertyField(showCallRadius);
                EditorGUILayout.PropertyField(agentLayersToCall);
                EditorGUILayout.PropertyField(callOthersTime);
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
                EditorGUILayout.PropertyField(moveBackwardsAttack);
            }


            EditorGUILayout.Space(spaceBetween);
            EditorGUILayout.LabelField("STRAFING", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(strafe);
            if (script.strafe) {
                EditorGUILayout.PropertyField(strafeSpeed);
                EditorGUILayout.PropertyField(strafeTime);
                EditorGUILayout.PropertyField(strafeWaitTime);
                EditorGUILayout.PropertyField(leftStrafeAnim);
                EditorGUILayout.PropertyField(rightStrafeAnim);
                EditorGUILayout.PropertyField(strafeAnimT);
                EditorGUILayout.PropertyField(strafeLayersToAvoid);
            }


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
