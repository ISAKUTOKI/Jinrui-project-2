using com;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Script.SceneObject
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public GameObject explosionPrefab;
        public int dmg = 40;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                SoundSystem.instance.Play("FireCannon");
                var exp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(exp.gameObject, 2);

                PlayerBehaviour.instance.health.TakeDamage(dmg);
            }
        }

        void Start()
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * speed;
            Destroy(gameObject, 10);
        }
    }
}