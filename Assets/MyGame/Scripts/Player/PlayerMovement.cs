using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using MyGame.MyGame.Core;
using MyGame.MyGame.Projectiles;
using MyGame.MyGame.SaveSystem;

namespace MyGame.MyGame.Player
{
    public class PlayerMovement : MonoBehaviour, IHealth, ISaveable
    {
        public float jumpForce = 5f;
        public float moveSpeed = 5f;
        public GameObject bulletPrefab;
        public Transform firePoint;
        public float fireRate = 0.5f;
        public int maxHealth = 100;
        public string saveFileName = "playerSave.json";

        private Rigidbody rb;
        private bool isGrounded;
        private float nextFire;

        public int Health { get; set; }

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            Health = maxHealth;
            LoadGame();
        }

        void Update()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(x, 0, z);
            moveDirection = transform.TransformDirection(moveDirection);
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);

            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveGame();
            }
        }

        void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.shooter = this.gameObject;
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Debug.Log("Игрок получил урон: " + damage);

            if (Health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Debug.Log("Игрок умер!");
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

        public void SaveGame()
        {
            SaveData data = GetSaveData();
            string json = JsonUtility.ToJson(data);
            string path = Path.Combine(Application.persistentDataPath, saveFileName);
            File.WriteAllText(path, json);
            Debug.Log("Игра сохранена в: " + path);
        }

        public void LoadGame()
        {
            string path = Path.Combine(Application.persistentDataPath, saveFileName);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                LoadSaveData(data);
                Debug.Log("Игра загружена из: " + path);
            }
            else
            {
                Debug.LogWarning("Файл сохранения не найден: " + path);
            }
        }
    }
}