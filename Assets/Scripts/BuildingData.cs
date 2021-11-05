// Данные о постройке, которые хранятся в файле
[System.Serializable]
public class BuildingData
{
    public string TypeName; //тип постройки
    public int X;
    public int Y;
    public int Level; //используется для интерактивных построек
    public string Description; //используется для построек с описанием
}
