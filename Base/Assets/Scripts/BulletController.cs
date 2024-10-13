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
        ServiceLocator.GetService<AudioManager>().PlayMainSfx("Laser Impact");
        damager.SetDamage(target.GetComponent<CharacterConfig>().Damageable);
        Destroy(gameObject);
    }

    IEnumerator MoveTowardsPointB(Transform target)
    {
        // Si el target no es null
        if (target == null) yield break;

        // Posición inicial (A)
        Vector3 startPosition = transform.position;

        float tiempo = 0.0f;
        float duracion = 1.0f; // Tiempo total para alcanzar el objetivo (ajústalo según tu necesidad)

        float rangoImpacto = 0.5f; // Tolerancia para el impacto

        Vector3 direccionLateral = new Vector3(Random.Range(1, 3) == 1 ? 1 : -1, 0, 0);

        while (target != null && tiempo < 1.0f)
        {
            tiempo += Time.deltaTime / duracion;

            // Posición final del target (se actualiza constantemente)
            Vector3 endPosition = target.position;

            // Definir un punto de control a un lado (en el eje X o Z para curvatura lateral)
            //Vector3 direccionLateral = Vector3.right; // Puedes usar Vector3.left para la otra dirección
            Vector3 puntoControl = (startPosition + endPosition) / 2 + direccionLateral * 4; // Ajusta la magnitud de la curva

            // Interpolamos primero entre el punto A (inicio) y el punto de control
            Vector3 punto1 = Vector3.Lerp(startPosition, puntoControl, tiempo);
            // Luego entre el punto de control y el punto B (target) que se actualiza
            Vector3 punto2 = Vector3.Lerp(puntoControl, endPosition, tiempo);
            // Finalmente, interpolamos entre punto1 y punto2 para obtener la curva lateral
            transform.position = Vector3.Lerp(punto1, punto2, tiempo);

            // Calcula la distancia para saber si se alcanza el target
            float distance = Vector3.Distance(transform.position, endPosition);

            // Si alcanzamos el objetivo o estamos dentro del rango de impacto
            if (target != null && distance <= rangoImpacto)
            {
                HitTarget();
                Destroy(gameObject); // Destruye el proyectil si se alcanza el target
                yield break; // Detenemos la coroutina
            }

            // Espera hasta el siguiente frame
            yield return null;
        }

        // Si el target desaparece durante la ejecución
        Destroy(gameObject);
    }






}
