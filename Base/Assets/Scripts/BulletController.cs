using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletController : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;

    [SerializeField] BaseDamager damager;
    public void Seek(Transform _target)
    {
        target = _target;
        StartCoroutine(MoveTowardsPointB(target));
    }

    void HitTarget()
    {
        damager.SetDamage(target.GetComponent<CharacterConfig>().Damageable);
        Destroy(gameObject);
    }

    IEnumerator MoveTowardsPointB(Transform target)
    {
        // Mientras el target no sea null
        while (target != null)
        {
            // Calcula la distancia solo si el target aún existe
            float distance = Vector3.Distance(transform.position, target.position);

            // Si el target desaparece o se alcanza el objetivo
            if (target == null || distance <= 0.1f)
            {
                HitTarget();
                Destroy(gameObject); // Destruye la bala si el target desaparece o se alcanza
                yield break; // Detenemos la corrutina
            }

            // Calcula la dirección desde la posición actual hasta el target
            Vector3 direction = (target.position - transform.position).normalized;

            // Mueve el objeto hacia el target a la velocidad especificada
            transform.position += direction * speed * Time.deltaTime;

            // Espera hasta el siguiente frame
            yield return null;
        }

        // Si el target desaparece durante la ejecución del bucle
        Destroy(gameObject);
    }


}
