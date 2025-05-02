using Unity.Cinemachine;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed;

    public Vector3 Direction {  get; set; }
    public float Damage { get; set; }

    private void Update()
    {
        transform.Translate(Direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IDamageable>()?.TakeDamage(Damage);

        Destroy(gameObject);
    }

}
