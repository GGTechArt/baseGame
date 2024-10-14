using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUIHandler : MonoBehaviour
{
    CanvasGroup group;

    [SerializeField] TextMeshProUGUI text;
    float textAlpha;
    float groupAlpha;
    [SerializeField] float speed;
    string newText;

    bool disable = false;
    bool textChanging = false;  // Controla el cambio de texto

    // Start is called before the first frame update
    void Start()
    {
        group = gameObject.GetComponent<CanvasGroup>();

        groupAlpha = group.alpha;
        textAlpha = text.alpha;
    }

    // Update is called once per frame
    void Update()
    {
        if (group.alpha != groupAlpha)
        {
            group.alpha = Mathf.MoveTowards(group.alpha, groupAlpha, speed * Time.deltaTime);

            if (group.alpha == groupAlpha)
            {
                if (disable)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        if (text.alpha != textAlpha)
        {
            text.alpha = Mathf.MoveTowards(text.alpha, textAlpha, speed * Time.deltaTime);

            // Si el texto se ha desvanecido completamente, cambiarlo
            if (text.alpha == 0 && textChanging)
            {
                text.text = newText;   // Cambia el texto una vez que el alpha es 0
                textAlpha = 1;         // Ahora comienza a aumentar el alpha
                textChanging = false;  // Indica que ya no estamos cambiando el texto
            }
        }
    }

    public void SetText(string newText)
    {
        this.newText = newText;
        textAlpha = 0;           // Primero desvanecemos el texto
        textChanging = true;     // Señalamos que estamos en el proceso de cambiar el texto
    }

    public void Show()
    {
        groupAlpha = 1;
    }

    public void Hide()
    {
        groupAlpha = 0;
        disable = true;
    }
}
