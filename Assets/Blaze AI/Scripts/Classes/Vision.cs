using UnityEngine;
using System.Collections.Generic;

namespace BlazeAISpace
{
    [System.Serializable]
    public class Vision
    {
        [System.Serializable] public struct normalVision {
            [Range(0f, 360f)]
            public float coneAngle;
            [Min(0f)]
            public float sightRange;
            
            //constructor
            public normalVision (float angle, float range) {
                coneAngle = angle;
                sightRange = range;
            }
        }

        [System.Serializable] public struct alertVision {
            [Range(0f, 360f)]
            public float coneAngle;
            [Min(0f)]
            public float sightRange;

            //constructor
            public alertVision (float angle, float range) {
                coneAngle = angle;
                sightRange = range;
            }
        }

        [System.Serializable] public struct attackVision {
            [Range(0f, 360f)]
            [Tooltip("Always better to have this at 360 in order for the AI to have 360 view when in attack state.")]
            public float coneAngle;
            [Min(0f), Tooltip("Will be automatically set if cover shooter enabled based on Distance From Enemy property.")]
            public float sightRange;

            //constructor
            public attackVision (float angle, float range) {
                coneAngle = angle;
                sightRange = range;
            }
        }

        [System.Serializable] public struct AlertTags {
            [Tooltip("The tag name you want to react to.")]
            public string alertTag;
            [Tooltip("The behaviour script to enable when seeing this alert tag.")]
            public MonoBehaviour behaviourScript;
            [Tooltip("When the AI sees an object with an alert tag it'll immediately change it to this value. In order not to get alerted by it again. If this value is empty it'll fall back to 'Untagged'.")]
            public string fallBackTag;
        }
        

        [Header("ENEMY/ALERT LAYERS & TAGS")]
        [Tooltip("Add all the layers you want to detect around the world. Any layer not added will be seen through. Recommended not to add other Blaze agents layers in order for them not to block the view from your target. Also no need to set the enemy layers here.")]
        public LayerMask layersToDetect = Physics.AllLayers;
        [Tooltip("Set the layers of the hostiles and alerts. Hostiles are the enemies you want to attack. Alerts are objects when seen will turn the AI to alert state.")]
        public LayerMask hostileAndAlertLayers;

        [Space(5)]
        [Tooltip("The tag names of hostile gameobjects (player) that this agent should attack. This needs to be set in order to identify enemies.")]
        public string[] hostileTags;
        [Tooltip("Tags that will make the agent become in alert state such as tags of dead bodies or an open door. This is optional.")]
        public AlertTags[] alertTags;


        [Header("SET THE VISION RANGE AND CONE ANGLE FOR EACH STATE"), Space(7)]
        public normalVision visionDuringNormalState = new normalVision(90f, 10f);
        public alertVision visionDuringAlertState = new alertVision(100f, 15f);
        public attackVision visionDuringAttackState = new attackVision(360f, 15f);


        [Header("SIGHT HEIGHT"), Space(7)]
        [Min(0f), Tooltip("The level of sight (eyes) of the agent. Anything below this vision cone will be seen, anything above it won't. Enable Show Normal Vision, etc... in the DEBUG section to see how the vision cone of different states offsets with this property.")]
        public float sightLevel = 1f;
        [Min(0f), Tooltip("The maximum level of sight of the agent to detect objects, shown as a purple rectangle in the scene view. Any object above the max level won't be seen but anything between the rectangle and below the actual vision cone will be seen.")]
        public float maxSightLevel = 2f;
        [Tooltip("OPTIONAL: add the head object, this will be used for updating both the rotation of the vision according to the head and the sight level automatically. If empty, the rotation will be according to the body, projecting forwards.")]
        public Transform head;

        [Header("VISION CYCLE"), Range(1, 30), Space(7), Tooltip("Vision systems normally run once every certain amount of frames to improve performance. Here you can set the amount of frames to pass before running vision on each cycle. The lower the number, the more accurate but expensive. The higher the number, the less accurate but better for performance. Remember the amount of frames passing is basically neglibile, so the accuracy isn't that big of a measure but performance will be better.")]
        public int pulseRate = 10;
        
        [Header("DEBUG"), Space(7)]
        [Tooltip("Show the vision cone of normal state in scene view for easier debugging.")]
        public bool showNormalVision = true;
        [Tooltip("Show the vision cone of alert state in scene view for easier debugging.")]
        public bool showAlertVision = false;
        [Tooltip("Show the vision cone of attack state in scene view for easier debugging.")]
        public bool showAttackVision = false;
        [Tooltip("Shows the maximum sight level as a purple rectangle.")]
        public bool showMaxSightLevel = false;


        // show the vision spheres in level-editor screen
        public void ShowVisionSpheres(Transform transform) 
        {
            if (showNormalVision) {
                //normal state vision
                DrawVisionCone(transform, visionDuringNormalState.coneAngle, visionDuringNormalState.sightRange, Color.white);
            }
            
            if (showAlertVision) {
                //alert state vision
                DrawVisionCone(transform, visionDuringAlertState.coneAngle, visionDuringAlertState.sightRange, Color.white);
            }

            if (showAttackVision) {
                //attack state vision
                DrawVisionCone(transform, visionDuringAttackState.coneAngle, visionDuringAttackState.sightRange, Color.red);
            }

            if (showMaxSightLevel) {
                //all the passed arguments are being ignored
                DrawVisionCone(transform, visionDuringAttackState.coneAngle, visionDuringAttackState.sightRange, Color.red, true);
            }
        }


        // draw vision cone
        void DrawVisionCone(Transform transform, float angle, float rayRange, Color color, bool ignore = false)
        {
            if (transform == null) return;


            if (ignore) 
            {
                if (showMaxSightLevel && maxSightLevel > 0f) {
                    Gizmos.color = new Color(139,0,139);
                    Gizmos.DrawCube(transform.position + new Vector3(0f, sightLevel + maxSightLevel, 0.5f), new Vector3(0.5f, 0.05f, 0.5f));
                }
                return;
            }


            if (angle >= 360f)
            {   
                Gizmos.color = color;
                Gizmos.DrawWireSphere(transform.position + new Vector3(0f, sightLevel + maxSightLevel, 0f), rayRange);
                return;
            }
            

            float halfFOV = angle / 2.0f;

            Quaternion leftRayRotation1 = Quaternion.AngleAxis(-halfFOV, Vector3.up);
            Quaternion rightRayRotation1 = Quaternion.AngleAxis(halfFOV, Vector3.up);

            leftRayRotation1.eulerAngles = new Vector3(0f, leftRayRotation1.eulerAngles.y, 0f);
            rightRayRotation1.eulerAngles = new Vector3(0f, rightRayRotation1.eulerAngles.y, 0f);

            Vector3 leftRayDirection1 = leftRayRotation1 * transform.forward;
            Vector3 rightRayDirection1 = rightRayRotation1 * transform.forward;

            Vector3 npcSight = new Vector3(transform.position.x, transform.position.y + sightLevel, transform.position.z);

            Gizmos.color = Color.white;
            Gizmos.DrawRay(npcSight, leftRayDirection1 * rayRange);
            Gizmos.DrawRay(npcSight, rightRayDirection1 * rayRange);

            Gizmos.DrawLine(npcSight + rightRayDirection1 * rayRange, npcSight + leftRayDirection1 * rayRange);
        }


        // return the index of the passed alert tag -> if exists
        public int GetAlertTagIndex(string alertTag)
        {
            for (int i=0; i<alertTags.Length; i++) {
                // check if alert tag is empty
                if (alertTags[i].alertTag.Length <= 0) {
                    continue;
                }

                // check if alert tag equals the paramater
                if (alertTags[i].alertTag != alertTag) {
                    continue;
                }
                
                return i;
            }

            return -1;
        }


        // disable all the behaviour scripts of alert tags
        public void DisableAllAlertBehaviours()
        {
            for (int i=0; i<alertTags.Length; i++) {
                if (alertTags[i].behaviourScript != null) {
                    alertTags[i].behaviourScript.enabled = false;
                }
            }
        }


        // check if any tag in hostile and alert are equal
        public void CheckHostileAndAlertItemEqual(bool dialogue=false)
        {
            for (int i=0; i<hostileTags.Length; i++) {
                for (int x=0; x<alertTags.Length; x++) {
                    if (hostileTags[i].Length > 0 && hostileTags[i] == alertTags[x].alertTag) {
                        #if UNITY_EDITOR
                        if (UnityEditor.EditorUtility.DisplayDialog("Same tag in Hostile and Alert",
                            "You can't have the same tag name in both Hostile and Alert. The tag name in Alert Tags will be removed when out of focus or you can continue typing by double clicking the text.", "Ok")) {
                        }
                        #endif 

                        alertTags[x].alertTag = "";
                    }
                }
            }
        }
    }
}

