using UnityEngine.UI;
using UnityEngine;

public class MovBackground : MonoBehaviour
{
    public Material _material; // El material cuyo offset de textura quieres animar
    public float _x; // Velocidad de desplazamiento en el eje X
    public float _y; // Velocidad de desplazamiento en el eje Y

    private Vector2 _offset; // Offset actual de las coordenadas UV

    private void Update()
    {
        // Calculamos el nuevo offset en función de las velocidades y el tiempo
        _offset += new Vector2(_x, _y) * Time.deltaTime;

        // Aplicamos el nuevo offset a la textura principal del material
        _material.SetTextureOffset("_MainTex", _offset);
    }
}
