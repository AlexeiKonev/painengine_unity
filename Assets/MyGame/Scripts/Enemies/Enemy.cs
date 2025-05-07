using UnityEngine;
using MyGame.Core;
using MyGame.SaveSystem;

namespace MyGame.Enemies
{
    public class Enemy : MonoBehaviour, IHealth, ISaveable
    {
        public int maxHealth = 100;
        public int Health { get; set; }

        void Start()
        {
            Health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Debug.Log("Враг получил урон: " + damage);

            if (Health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Debug.Log("Враг уничтожен!");
            Destroy(gameObject);
        }

        public SaveData GetSaveData()
        {
            SaveData data = new SaveData();
            data.position = transform.position;
            data.health = Health;
            return data;
        }

        public void LoadSaveData(SaveData data)
        {
            transform.position = data.position;
            Health = data.health;
        }
    }
}