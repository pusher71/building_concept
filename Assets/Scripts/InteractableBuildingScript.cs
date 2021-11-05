using UnityEngine;

public class InteractableBuildingScript : MonoBehaviour
{
    [SerializeField] private UpgradeMenuScript upgradeMenuScript; //скрипт меню обновления

    private int level = 1; //уровень постройки (изначально 1)
    [SerializeField] private TextMesh textLevel; //текст для отображения уровня
    [SerializeField] private Material[] levelMaterials; //материалы для визуализации уровней

    // Получить уровень
    public int GetLevel()
    {
        return level;
    }

    // Задать уровень
    public void SetLevel(int value)
    {
        level = value;
        VisualizeLevel();
    }

    // Повысить уровень
    public void Upgrade()
    {
        level++;
        VisualizeLevel();
    }

    // Отобразить уровень
    public void VisualizeLevel()
    {
        textLevel.text = level.ToString();
        GetComponent<Renderer>().material = levelMaterials[level];
    }

    void Start()
    {
        ShuffleMaterials();
        VisualizeLevel();
    }

    private void OnMouseDown()
    {
        if (InteractableManagerScript.instance.Interactable)
        {
            //открыть меню обновления
            upgradeMenuScript.OpenMenu();
        }
    }

    // Перемешать порядок цветов уровней
    private void ShuffleMaterials()
    {
        for (int i = 0; i < levelMaterials.Length; i++)
        {
            Material key = levelMaterials[i];
            int rnd = Random.Range(i, levelMaterials.Length);
            levelMaterials[i] = levelMaterials[rnd];
            levelMaterials[rnd] = key;
        }
    }
}
