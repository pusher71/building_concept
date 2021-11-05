using UnityEngine;

public class DescriptionBuildingScript : MonoBehaviour
{
    [SerializeField] private InfoWindowScript infoWindowScript; //скрипт окна с описанием
    private string description = ""; //само описание

    void Start()
    {
        if (GetDescription() == "")
            GenerateDescription();
    }

    // Принять случайное описание
    public void GenerateDescription()
    {
        string[] possible = new string[5] //возможные описания
        {
            "Hospital",
            "Hotel",
            "Shop",
            "Factory",
            "Residential building"
        };

        SetDescription(possible[Random.Range(0, possible.Length)]);
    }

    // Получить описание
    public string GetDescription()
    {
        return description;
    }

    // Задать описание
    public void SetDescription(string value)
    {
        description = value;
    }

    private void OnMouseDown()
    {
        if (InteractableManagerScript.instance.Interactable)
        {
            //открыть окно с описанием
            infoWindowScript.OpenWindow();
        }
    }
}
