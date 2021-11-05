using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    [SerializeField] private BuildMenuScript buildMenuScript; //скрипт меню постройки

    [SerializeField] private GameObject[] buildingPrefabs; //заготовки разных типов построек
    private Vector3 selectedPosition; //выбранная позиция новой постройки

    private void OnMouseDown()
    {
        if (InteractableManagerScript.instance.Interactable)
        {
            //получить позицию будущей постройки
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit);

            if (hit.point.y == -0.5f) //если нажатие было произведено именно по верхней грани коллайдера поля
            {
                selectedPosition = new Vector3(Mathf.Round(hit.point.x), 0, Mathf.Round(hit.point.z));
                buildMenuScript.OpenMenu(); //открыть меню постройки
            }
        }
    }

    // Построить
    public void Build(int buildType)
    {
        GameObject currentBuilding = Instantiate(buildingPrefabs[buildType], transform);
        currentBuilding.transform.position = selectedPosition;
    }

    // Загрузить поле
    public void LoadField()
    {
        //предварительно очистить поле
        ClearField();

        //считать данные из файла
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Application.persistentDataPath + "/Field.dat", FileMode.Open);
        List<BuildingData> list = (List<BuildingData>)bf.Deserialize(fs);
        fs.Close();

        //отстроить поле по считанным данным
        for (int i = 0; i < list.Count; i++) //для каждой постройки
        {
            BuildingData currentData = list[i]; //хранимые данные о текущей постройке
            GameObject currentBuilding = null; //что поместим на поле
            switch (currentData.TypeName)
            {
                case "DecorativeBuilding":
                    currentBuilding = Instantiate(buildingPrefabs[0], transform);
                    break;

                case "InteractableBuilding":
                    currentBuilding = Instantiate(buildingPrefabs[1], transform);
                    currentBuilding.GetComponent<InteractableBuildingScript>().SetLevel(currentData.Level);
                    break;

                case "DescriptionBuilding":
                    currentBuilding = Instantiate(buildingPrefabs[2], transform);
                    currentBuilding.GetComponent<DescriptionBuildingScript>().SetDescription(currentData.Description);
                    break;
            }

            currentBuilding.transform.position = new Vector3(currentData.X, 0, currentData.Y);
        }
    }

    // Сохранить поле
    public void SaveField()
    {
        //сформировать данные для сохранения
        List<BuildingData> list = new List<BuildingData>();
        for (int i = 0; i < transform.childCount; i++) //для каждой постройки
        {
            GameObject currentBuilding = transform.GetChild(i).gameObject; //текущая постройка
            BuildingData currentData = new BuildingData(); //хранимые данные о текущей постройке

            //взять хранимые данные с постройки
            currentData.X = (int)currentBuilding.transform.position.x;
            currentData.Y = (int)currentBuilding.transform.position.z;
            if (currentBuilding.GetComponent<InteractableBuildingScript>() != null) //если постройка интерактивная
            {
                currentData.TypeName = "InteractableBuilding";
                currentData.Level = currentBuilding.GetComponent<InteractableBuildingScript>().GetLevel(); //взять её уровень
            }
            else if (currentBuilding.GetComponent<DescriptionBuildingScript>() != null) //если постройка с описанием
            {
                currentData.TypeName = "DescriptionBuilding";
                currentData.Description = currentBuilding.GetComponent<DescriptionBuildingScript>().GetDescription(); //взять её описание
            }
            else //иначе постройка декоративная
            {
                currentData.TypeName = "DecorativeBuilding";
            }

            list.Add(currentData);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/Field.dat");
        bf.Serialize(fs, list);
        fs.Close();
    }

    // Очистить поле
    public void ClearField()
    {
        while (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
    }

    // Выйти из игры
    public void Quit()
    {
        Application.Quit();
    }
}
