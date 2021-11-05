using UnityEngine;
using UnityEngine.UI;

public class BuildMenuScript : MonoBehaviour
{
    [SerializeField] private FieldScript field; //скрипт поля

    [SerializeField] private Canvas canvas; //полотно
    [SerializeField] private Button[] buttonsOfType; //список кнопок, отвечающих за тип постройки
    [SerializeField] private Button buttonBuild; //кнопка постройки
    private int buildType; //выбранный тип постройки

    // Открыть меню постройки
    public void OpenMenu()
    {
        canvas.enabled = true;
        InteractableManagerScript.instance.Interactable = false;

        //переключить доступность кнопок
        for (int i = 0; i < buttonsOfType.Length; i++)
            buttonsOfType[i].interactable = true;
        buttonBuild.interactable = false;
    }

    // Выбрать тип постройки
    public void BuildTypeSelected(int selected)
    {
        buildType = selected;

        //переключить доступность кнопок
        for (int i = 0; i < buttonsOfType.Length; i++)
            buttonsOfType[i].interactable = buildType != i;
        buttonBuild.interactable = true;
    }

    // Нажата кнопка постройки
    public void BuildButtonClicked()
    {
        field.Build(buildType);
        CloseMenu();
    }

    // Закрыть меню постройки
    public void CloseMenu()
    {
        canvas.enabled = false;
        InteractableManagerScript.instance.Interactable = true;
    }
}
