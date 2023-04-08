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
        [SerializeField] private float seekMax = 1f;
        private float[] avoid;
        [SerializeField] private float avoidMultip = 2f;
        [SerializeField] private float avoidSphere = 10f;
        [SerializeField] private LayerMask avoidMask;
        //[SerializeField] private int avoidMax = 20;

        private Vector3 aim = Vector3.zero;

        private void OnEnable()
        {
            directionCount = directions.Length;
            seek = new float[directionCount];
            avoid = new float[directionCount];
            _transform = transform;

            StartCoroutine(SightUpdate());
        }

        private IEnumerator SightUpdate()
        {
            yield return new WaitForEndOfFrame();

            while (true)
            {
                SightCheck();
                yield return new WaitForSeconds(updateT);
            }
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
                }

                //for (int i = 0; i < directionCount; i++)
                    //Debug.DrawRay(_transform.position + Vector3.up * 0.2f, seek[i] * selfDirections[i], Color.blue, updateT);
            }
            else
            {
                aim = Vector3.zero;
                legs.SetDirection(aim);
                return;
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
        }
    }
}
