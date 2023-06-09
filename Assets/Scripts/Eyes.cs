using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP.AI
{
    public class Eyes : MonoBehaviour
    {
        [SerializeField] private Legs legs;
        [SerializeField] private Vector3[] directions;
        [SerializeField] private float updateT = 1f;


        private Transform _transform;

        private int directionCount;

        private float[] seek;
        [SerializeField] private float seekMultip = 1f;
        [SerializeField] private float seekSphere = 10f;
        [SerializeField] private LayerMask seekMask;
        [SerializeField] private LayerMask returnMask;
        [SerializeField] private float seekMax = 1f;
        private float[] avoid;
        [SerializeField] private float avoidMultip = 2f;
        [SerializeField] private float avoidSphere = 10f;
        [SerializeField] private LayerMask avoidMask;
        //[SerializeField] private int avoidMax = 20;

        private int returnPointsSpawned = 0;
        private List<Transform> returnPoints = new List<Transform>();
        [SerializeField] private GameObject returnpoint;
        private float distanceTraveled = 0;
        private Vector3 lastPosition = Vector3.zero;

        
        [SerializeField] private GameObject patrolLoop;
        [SerializeField] private GameObject chaseLoop;

        [SerializeField] private Transform point1;
        [SerializeField] private Transform point2;
        [SerializeField] private bool patrolPoint = false;

        [SerializeField] public Animator anim;

        public enum EAIState
        {
            Patrol,
            Chase,
            Return
        }

        public EAIState aIState = EAIState.Patrol;


        private Vector3 aim = Vector3.zero;

        private void OnEnable()
        {
            directionCount = directions.Length;
            seek = new float[directionCount];
            avoid = new float[directionCount];
            _transform = transform;

            StartCoroutine(SightUpdate());
        }

        private bool whistle = false;
        private IEnumerator SightUpdate()
        {
            yield return new WaitForEndOfFrame();

            while (true)
            {
                SightCheck();
                if(!whistle && Random.Range(0,10000) < 2 && aIState != EAIState.Chase)
                {
                    whistle = true;
                    AudioManager.PlayStereoSound(ESound.GuardWhistle, transform.position, _transform);
                    StartCoroutine(WhistleReset());
                }
                yield return new WaitForSeconds(updateT);
            }
        }

        private IEnumerator WhistleReset()
        {
            yield return new WaitForSeconds(100);
            whistle = false;;
        }

        private void SightCheck()
        {
            
            Vector3[] selfDirections = new Vector3[directionCount];
            for (int i = 0; i < directionCount; i++)
            {
                selfDirections[i] = _transform.forward * directions[i].z + _transform.right * directions[i].x;
                selfDirections[i].y = 0;
                selfDirections[i].Normalize();
            }

            Collider[] seekColliders = Physics.OverlapSphere(_transform.position, seekSphere, seekMask);
            seek = new float[directionCount];
            avoid = new float[directionCount];
            if (seekColliders.Length != 0)
            {
                float seekNew = 0;
                foreach (Collider collider in seekColliders)
                {
                    if(returnPointsSpawned > 0)
                        returnPoints[returnPoints.Count-1].GetComponent<ReturnPoint>().SetSeek(false);
                    if(collider.CompareTag("Player"))
                    {
                        if(aIState != EAIState.Chase)
                            AudioManager.PlayStereoSound(ESound.GuardAgroStart, transform.position, _transform);
                        aIState = EAIState.Chase;
                        anim.SetBool("IsRunning", true);
                        patrolLoop.SetActive(false);
                        chaseLoop.SetActive(true);
                        Music._instance.TriggerMusic(4);
                        if(returnPointsSpawned == 0)
                        {
                            returnPoints.Add(Instantiate(returnpoint, _transform.position - _transform.forward, Quaternion.identity).transform);
                            distanceTraveled = 0;
                            returnPointsSpawned++; 
                        }
                    }
                    Vector3 dir = (collider.ClosestPoint(_transform.position) - _transform.position);
                    if (dir.magnitude <= seekMax)
                        continue;
                    for (int i = 0; i < directionCount; i++)
                    {
                        dir.Normalize();
                        dir.y = 0;
                        seekNew = seekMultip * Vector3.Dot(selfDirections[i], dir); //.normalized * (seekSphere - dir.magnitude) / seekSphere);
                        if (seekNew > seek[i])
                            seek[i] = seekNew;
                    }
                    break;
                }

                //for (int i = 0; i < directionCount; i++)
                    //Debug.DrawRay(_transform.position + Vector3.up * 0.2f, seek[i] * selfDirections[i], Color.blue, updateT);
            }
            else if(returnPointsSpawned > 0)
            {
                if(aIState != EAIState.Return)
                    AudioManager.PlayStereoSound(ESound.GuardAgroStop, transform.position, _transform);
                aIState = EAIState.Return;
                anim.SetBool("IsRunning", false);
                patrolLoop.SetActive(true);
                chaseLoop.SetActive(false);
                if(returnPoints[returnPoints.Count-1] != null)
                {
                    float seekNew = 0;
                    Vector3 dir = (returnPoints[returnPoints.Count-1].position - _transform.position);
                    returnPoints[returnPoints.Count-1].GetComponent<ReturnPoint>().SetSeek(true);

                    for (int i = 0; i < directionCount; i++)
                    {
                        dir.Normalize();
                        dir.y = 0;
                        seekNew = seekMultip * Vector3.Dot(selfDirections[i], dir); //.normalized * (seekSphere - dir.magnitude) / seekSphere);
                        if (seekNew > seek[i])
                            seek[i] = seekNew;
                    }
                }
            }
            else
            {
                aim = Vector3.zero;
                legs.SetDirection(aim);
            }

            Collider[] avoidColliders = Physics.OverlapSphere(_transform.position, avoidSphere, avoidMask);
            if (avoidColliders.Length != 0)
            {
                float avoidNew = 0;
                foreach (Collider collider in avoidColliders)
                {
                    for (int i = 0; i < directionCount; i++)
                    {
                        Vector3 dir = (collider.ClosestPoint(_transform.position) - _transform.position).normalized;
                        dir.y = 0;
                        avoidNew = avoidMultip * Vector3.Dot(selfDirections[i], dir);//dir.normalized * (avoidSphere - dir.magnitude) / avoidSphere);
                        if (avoidNew > avoid[i])
                            avoid[i] = avoidNew;
                    }
                }

                //for (int i = 0; i < directionCount; i++)
                    //Debug.DrawRay(_transform.position, avoid[i] * selfDirections[i], Color.yellow, updateT);
            }

            Vector3 aimNew = Vector3.zero;
            for (int i = 0; i < directionCount; i++)
            {
                float seekAvoidNew = 0;
                seekAvoidNew = seek[i] - avoid[i];
                if (seekAvoidNew < 0)
                    seekAvoidNew = 0;
                //Debug.DrawRay(_transform.position + Vector3.up, seekAvoidNew * selfDirections[i], Color.red, updateT);
                aimNew += seekAvoidNew * selfDirections[i];
                aimNew.y = 0;
            }
            aim = aimNew.normalized;
            aim.Normalize();
            //Debug.DrawRay(_transform.position + Vector3.up, aim * 3, Color.magenta, updateT);
            legs.SetDirection(aim);

            if(aIState == EAIState.Chase)
            {
                distanceTraveled += (_transform.position - lastPosition).magnitude;
                lastPosition = _transform.position;
                if(distanceTraveled >= 2)
                {
                    returnPoints.Add(Instantiate(returnpoint, _transform.position - _transform.forward, Quaternion.identity).transform);
                    distanceTraveled -= 2;
                    returnPointsSpawned++;
                }
            }
            if(aIState == EAIState.Return && returnPointsSpawned <= 0)
            {
                aIState = EAIState.Patrol;
            }

            if(aIState == EAIState.Patrol)
            {
                Vector3 dirPat = (patrolPoint?point1.position:point2.position) -_transform.position;
                dirPat.y = 0;
                if(dirPat.magnitude < 0.2f)
                {
                    patrolPoint = !patrolPoint;
                    dirPat = (patrolPoint?point1.position:point2.position) -_transform.position;
                    dirPat.y = 0;
                }
                dirPat.Normalize();
                legs.SetDirection(dirPat);
            }
        }

        public void OnEndpointCollision()
        {
            returnPoints.Remove(returnPoints[returnPoints.Count-1]);
            returnPointsSpawned--;
        }
        
    }
}
