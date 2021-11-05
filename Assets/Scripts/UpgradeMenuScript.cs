using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuScript : MonoBehaviour
{
    [SerializeField] private InteractableBuildingScript building; //скрипт постройки

    [SerializeField] private Canvas canvas; //полотно
    [SerializeField] private Text text; //текст
    [SerializeField] private GameObject buttonUpgrade; //кнопка обновления

    // Открыть меню обновления
    public void OpenMenu()
    {
        canvas.enabled = true;
        InteractableManagerScript.instance.Interactable = false;

        //переключить доступность обновления
        text.text = building.GetLevel() < 5 ? "Повысить уровень?" : "Невозможно повысить уровень";
        buttonUpgrade.SetActive(building.GetLevel() < 5);
    }

    // Нажата кнопка обновления
    public void UpgradeButtonClicked()
    {
        building.Upgrade();
        CloseMenu();
    }

    // Закрыть меню обновления
    public void CloseMenu()
    {
        canvas.enabled = false;
        InteractableManagerScript.instance.Interactable = true;
    }
}
