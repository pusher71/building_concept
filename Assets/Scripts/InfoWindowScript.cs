using UnityEngine;
using UnityEngine.UI;

public class InfoWindowScript : MonoBehaviour
{
    [SerializeField] private DescriptionBuildingScript building; //скрипт постройки

    [SerializeField] private Canvas canvas; //полотно
    [SerializeField] private Text text; //текст

    // Открыть окно с описанием
    public void OpenWindow()
    {
        canvas.enabled = true;
        InteractableManagerScript.instance.Interactable = false;

        //вывести описание
        text.text = building.GetDescription();
    }

    // Закрыть окно с описанием
    public void CloseWindow()
    {
        canvas.enabled = false;
        InteractableManagerScript.instance.Interactable = true;
    }
}
