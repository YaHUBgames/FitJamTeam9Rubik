using UnityEngine;

namespace PP.AI
{
    public class Legs : MonoBehaviour
    {

        [SerializeField] private Rigidbody rb;
        [SerializeField] private float strength = 10f;
        [SerializeField] private float speed = 10f;
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

            rb.AddForce(Time.deltaTime * strength * aim, ForceMode.Force);

            Vector3 newV = rb.velocity;
            newV.y = 0;
            if (newV != Vector3.zero)
                rb.rotation = Quaternion.LookRotation(newV.normalized, Vector3.up);

            if (newV.magnitude > speed)
                rb.velocity = newV.normalized * speed;
        }

        public void SetDirection(Vector3 direction)
        {
            direction.y = 0;
            direction.Normalize();
            wantToAim = direction;
        }
    }
}
