using UnityEngine;

namespace PP.AI
{
    public class Legs : MonoBehaviour
    {

        [SerializeField] private Eyes eyes;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float strength = 10f;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float chaseForceMult = 2f;
        [SerializeField] private float chaseSpeedMult = 2f;
        [SerializeField] private float interpSpeed = 1f;
        private Vector3 aim = Vector3.zero;
        private Vector3 wantToAim = Vector3.zero;
        [SerializeField] private float gg;

        private void Update()
        {
            aim = Vector3.Slerp(aim, wantToAim, Time.deltaTime * interpSpeed);
            Move();
        }

        private void Move()
        {
            if (wantToAim == Vector3.zero)
            {
                aim = wantToAim;
                rb.velocity = wantToAim;
                return;
            }

            rb.AddForce(Time.deltaTime * strength * aim * (eyes.aIState == Eyes.EAIState.Chase? chaseForceMult:1), ForceMode.Force);

            Vector3 newV = rb.velocity;
            newV.y = 0;
            if (newV != Vector3.zero)
                rb.rotation = Quaternion.LookRotation(newV.normalized, Vector3.up);

            if (newV.magnitude > speed * (eyes.aIState == Eyes.EAIState.Chase? chaseSpeedMult:1))
                rb.velocity = newV.normalized * speed * (eyes.aIState == Eyes.EAIState.Chase? chaseSpeedMult:1);
        }

        public void SetDirection(Vector3 direction)
        {
            direction.y = 0;
            direction.Normalize();
            wantToAim = direction;
        }
    }
}
