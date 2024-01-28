using UnityEngine;
using UnityEngine.AI;

namespace BlazeAISpace
{
    public class GoingToCoverBehaviour : MonoBehaviour
    {
        [Tooltip("Object layers that can take cover behind.")]
        public LayerMask coverLayers;
        [Range(-1f, 1f), Tooltip("The lower the number the better the hiding spot. From -1 (best) to 1 (worst)")]
        public float hideSensitivity = -0.25f;
        [Min(0), Tooltip("The search distance for cover. This can't be bigger than the [Distance From Enemy] property (automatically clamped if so)")]
        public float searchDistance = 5f;
        [Tooltip("Show search distance as a light blue sphere in scene view.")]
        public bool showSearchDistance;


        [Tooltip("The minimum height of cover obstacles to search for. Obstacle height is measured using collider.bounds.y. Use the GetCoverHeight script to get any obstacle height.")]
        public float minCoverHeight = 1.25f;
        [Tooltip("If height of obstacle (collider) is this or more, then it's a high cover and the high cover animation will be used. Obstacle height is measured using collider.bounds.y. Use the GetCoverHeight script to get any obstacle height.")]
        public float highCoverHeight = 2f;
        [Tooltip("The animation name to play when in high cover.")]
        public string highCoverAnim;
        [Tooltip("If height of obstacle (collider) is this or less, then it's a low cover and the low cover animation will be used. Obstacle height is measured using collider.bounds.y. Use the GetCoverHeight script to get any obstacle height.")]
        public float lowCoverHeight = 1f;
        [Tooltip("The animation name to play when in low cover.")]
        public string lowCoverAnim;
        public float coverAnimT = 0.25f;


        [Tooltip("If set to true, the AI will rotate to match the cover normal. Meaning the back of the character will be on the cover. If set to false, will take cover in the same current rotation when reached the cover.")]
        public bool rotateToCoverNormal = true;
        public float rotateToCoverSpeed = 300f;
        [Tooltip("This will make the AI refrain from attacking and only do so after taking cover.")]
        public bool onlyAttackAfterCover;

        
        #region BEHAVIOUR VARS

        BlazeAI blaze;
        CoverShooterBehaviour coverShooterBehaviour;

        public CoverProperties coverProps;
        public struct CoverProperties {
            public Transform cover;
            public Vector3 coverPoint;
            public float coverHeight;
            public BlazeAICoverManager coverManager;
        }

        public Transform lastCover { get; private set; }

        #endregion
        
        #region UNITY METHODS
        
        void Start()
        {
            blaze = GetComponent<BlazeAI>();    
            coverShooterBehaviour = GetComponent<CoverShooterBehaviour>();


            // force shut if not the same state
            if (blaze.state != BlazeAI.State.goingToCover) {
                enabled = false;
            }
        }

        void OnDrawGizmosSelected()
        {
            if (showSearchDistance) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(transform.position, searchDistance);
            }
        }

        void OnDisable()
        {
            RemoveCoverOccupation();
            blaze.tookCover = false;
        }

        void Update()
        {
            // if blaze is in friendly mode -> exit the state
            if (blaze.friendly) {
                blaze.SetState(BlazeAI.State.attack);
                return;
            }


            // call others that are not alerted yet and start the timer until attack
            coverShooterBehaviour.CallOthers();
            if (!onlyAttackAfterCover) coverShooterBehaviour.TimeUntilAttack();

            
            // if target exists
            if (blaze.enemyToAttack) {
                // if cover property is not null -> means AI has set a cover
                if (coverProps.cover) {
                    // save last cover
                    lastCover = coverProps.cover;

                    // moves agent to point and returns true when reaches destination
                    if (blaze.MoveTo(coverProps.coverPoint, coverShooterBehaviour.moveSpeed, coverShooterBehaviour.turnSpeed, coverShooterBehaviour.moveAnim, coverShooterBehaviour.idleMoveT)) {
                        TakeCover();
                    }
                }
                else {
                    if (blaze.hitWhileInCover) {
                        FindCover(lastCover);
                        return;
                    }

                    FindCover();
                }
            }
            else {
                // if there's no target return to cover shooter behaviour
                blaze.SetState(BlazeAI.State.attack);
            }
        }  

        #endregion

        #region COVER METHODS

        // search for cover
        public void FindCover(Transform coverToAvoid = null)
        {   
            blaze.hitWhileInCover = false;
            blaze.tookCover = false;

            Collider[] coverColls = new Collider[20];
            int hits = Physics.OverlapSphereNonAlloc(transform.position, searchDistance, coverColls, coverLayers);
            int hitReduction = 0;


            // eliminate bad cover options
            for (int i=0; i<hits; i++) {
                if (Vector3.Distance(blaze.ValidateYPoint(coverColls[i].transform.position), blaze.enemyToAttack.transform.position) + 2 >= coverShooterBehaviour.distanceFromEnemy || 
                    coverColls[i].bounds.size.y < minCoverHeight || coverColls[i].transform == coverToAvoid) {
                    
                    coverColls[i] = null;
                    hitReduction++;
                }
                else {
                    // check if other agents are already occupying/moving to the same cover by reading the cover manager component
                    BlazeAICoverManager coverManager = coverColls[i].transform.GetComponent<BlazeAICoverManager>();


                    // if cover manager doesn't exist -> continue
                    if (!coverManager) {
                        continue;
                    }


                    // cover manager exists and not occupied -> continue
                    if (coverManager.occupiedBy == null || coverManager.occupiedBy == transform) {
                        continue;
                    }


                    // reaching this far means cover manager exists and is occupied -> so remove as a potential cover
                    coverColls[i] = null;
                    hitReduction++;
                }
            }


            hits -= hitReduction;
            System.Array.Sort(coverColls, ColliderArraySortComparer);


            // if no covers found
            if (hits <= 0) {
                NoCoversFound();
                return;
            }


            NavMeshHit hit = new NavMeshHit();
            NavMeshHit hit2 = new NavMeshHit();
            NavMeshHit closestEdge = new NavMeshHit();
            NavMeshHit closestEdge2 = new NavMeshHit();

            
            // if found obstacles
            for (int i = 0; i < hits; i++) {
                Vector3 boundSize = coverColls[i].GetComponent<Collider>().bounds.size;
                
                if (NavMesh.SamplePosition(coverColls[i].transform.position, out hit, boundSize.x + boundSize.z, NavMesh.AllAreas)) {
                    if (!NavMesh.FindClosestEdge(hit.position, out closestEdge, NavMesh.AllAreas)) {
                        continue;
                    }


                    if (Vector3.Dot(closestEdge.normal, (blaze.enemyToAttack.transform.position - closestEdge.position).normalized) < hideSensitivity) {
                        if (!blaze.IsPathReachable(closestEdge.position)) {
                            continue;
                        }


                        if (coverShooterBehaviour.CheckIfTargetSeenFromPoint(closestEdge.position)) {
                            continue;
                        }


                        ChooseCover(closestEdge, coverColls[i]);
                        return;
                    }
                    else {
                        // Since the previous spot wasn't facing "away" enough from the target, we'll try on the other side of the object
                        if (NavMesh.SamplePosition(coverColls[i].transform.position - (blaze.enemyToAttack.transform.position - hit.position).normalized * 2, out hit2, boundSize.x + boundSize.z, NavMesh.AllAreas)) {
                            if (!NavMesh.FindClosestEdge(hit2.position, out closestEdge2, NavMesh.AllAreas)) {
                                continue;
                            }


                            if (Vector3.Dot(closestEdge2.normal, (blaze.enemyToAttack.transform.position - closestEdge2.position).normalized) < hideSensitivity) {
                                if (!blaze.IsPathReachable(closestEdge2.position)) {
                                    continue;
                                }


                                if (coverShooterBehaviour.CheckIfTargetSeenFromPoint(closestEdge2.position)) {
                                    continue;
                                }


                                ChooseCover(closestEdge2, coverColls[i]);
                                return;
                            }
                        }
                        else {
                            continue;
                        }
                    }
                }
                else {
                    continue;
                }
            }


            // if reached this point then no cover was found
            NoCoversFound();
        }

        // choose and save the passed cover
        void ChooseCover(NavMeshHit hit, Collider cover)
        {   
            BlazeAICoverManager coverMang = cover.transform.GetComponent<BlazeAICoverManager>();
            
            if (coverMang == null) {
                coverMang = cover.transform.gameObject.AddComponent<BlazeAICoverManager>() as BlazeAICoverManager;
            }
            
            coverMang.occupiedBy = transform;


            // save the cover properties
            coverProps.coverManager = coverMang;
            coverProps.cover = cover.transform;
            
            coverProps.coverPoint = hit.position;
            coverProps.coverHeight = cover.bounds.size.y;
        }

        public void RemoveCoverOccupation()
        {
            // set the current cover to null
            coverProps.cover = null;

            // if doesn't have cover manager -> return
            if (!coverProps.coverManager) {
                return;
            }

            // if cover manager shows that cover isn't occupied -> return
            if (coverProps.coverManager.occupiedBy == null) {
                return;
            }

            // if cover manager shows that cover is occupied by a different AI -> return
            if (coverProps.coverManager.occupiedBy != transform) {
                return;
            }

            // if reached this point means -> cover manager exists and this current AI occupies/occupied it
            coverProps.coverManager.occupiedBy = null;
        }

        // no covers have been found
        void NoCoversFound()
        {
            RemoveCoverOccupation();
            coverShooterBehaviour.FoundNoCover();
        }

        // taking cover
        void TakeCover()
        {
            // high cover
            if (coverProps.coverHeight >= highCoverHeight) {
                blaze.animManager.Play(highCoverAnim, coverAnimT);
            }

            
            // low cover
            if (coverProps.coverHeight <= lowCoverHeight) {
                blaze.animManager.Play(lowCoverAnim, coverAnimT);
            }


            RotateToCoverNormal();
            CheckCoverBlown();


            if (onlyAttackAfterCover) {
                coverShooterBehaviour.TimeUntilAttack();
            }
            

            blaze.tookCover = true;
        }

        // rotate to cover
        void RotateToCoverNormal()
        {
            if (!rotateToCoverNormal) {
                return;
            }

            RaycastHit hit;
            int layers = coverLayers;

            Vector3 dir = coverProps.cover.position - transform.position;
            Vector3 coverNormal = Vector3.zero;

            // get normal
            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, layers)) {
                if (coverProps.cover.IsChildOf(hit.transform) || hit.transform.IsChildOf(coverProps.cover)) {
                    coverNormal = hit.normal;
                    coverNormal.z = 0;
                }
            }
            
            // if hasn't hit the correct cover yet -> return
            if (coverNormal == Vector3.zero) {
                return;
            }

            // rotate to normal
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, coverNormal);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotateToCoverSpeed * Time.deltaTime);
        }

        // check if target has compromised AI cover
        void CheckCoverBlown()
        {
            Vector3 targetDir = blaze.enemyToAttack.transform.position - transform.position;
            RaycastHit coverBlownRayHit;

            if (Physics.Raycast(transform.position + blaze.centerPosition, targetDir, out coverBlownRayHit, Mathf.Infinity, Physics.AllLayers)) {
                if (blaze.enemyToAttack.transform.IsChildOf(coverBlownRayHit.transform)) {
                    if (coverShooterBehaviour.coverBlownDecision == CoverShooterBehaviour.CoverBlownDecision.AlwaysAttack) {
                        AttackFromCover();
                    }
                    else if (coverShooterBehaviour.coverBlownDecision == CoverShooterBehaviour.CoverBlownDecision.TakeCover) {
                        FindCover(coverProps.cover);
                    }
                    else {
                        int rand = Random.Range(0, 2);

                        if (rand == 0) {
                            FindCover(coverProps.cover);
                        }
                        else {
                            AttackFromCover();
                        }
                    }

                    RemoveCoverOccupation();
                }
            }
        }

        void AttackFromCover()
        {
            blaze.Attack();
        }

        int ColliderArraySortComparer(Collider A, Collider B)
        {
            if (A == null && B != null)
            {
                return 1;
            }
            else if (A != null && B == null)
            {
                return -1;
            }
            else if (A == null && B == null)
            {
                return 0;
            }
            else
            {
                return Vector3.Distance(transform.position, A.transform.position).CompareTo(Vector3.Distance(transform.position, B.transform.position));
            }
        }

        #endregion
    }
}
