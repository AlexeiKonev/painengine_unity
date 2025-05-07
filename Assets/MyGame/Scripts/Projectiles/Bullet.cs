using UnityEngine;
using MyGame.MyGame.Core;

namespace MyGame.MyGame.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20f;
        public int damage = 10;
        public float lifeTime = 2f;
        public GameObject shooter;

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.linearVelocity = transform.forward * speed;
            Destroy(gameObject, lifeTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == shooter) return;

            IHealth health = other.GetComponent<IHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}