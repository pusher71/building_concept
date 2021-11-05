using System.Collections;
using UnityEngine;

public class InteractableManagerScript : MonoBehaviour
{
    public static InteractableManagerScript instance;

    // Глобальная переменная, отвечающая за кликабельность любых объектов на сцене
    // Когда выводится любое меню с кнопками, тогда Interactable = false,
    // а когда меню скрывается, тогда Interactable = true

    private bool interactable = true;
    public bool Interactable
    {
        get
        {
            return interactable;
        }
        set
        {
            if (!value) //если отключаем возможность кликнуть,
                interactable = false; //то отключаем сразу,
            else //а если включаем,
                StartCoroutine(EnableInteractable(0.3f)); //то только через 0.3 секунды
        }
    }

    IEnumerator EnableInteractable(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        interactable = true;
    }

    void Start()
    {
        instance = this;
    }
}
