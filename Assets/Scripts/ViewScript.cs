using UnityEngine;

public class ViewScript : MonoBehaviour
{
    [SerializeField] private Rigidbody cameraAnchor;
    [SerializeField] private Camera camera;
    [SerializeField] private Light light;
    private bool is2D = false; //текущий вид - 2D

    // Переключить вид камеры
    public void ToggleView()
    {
        if (!is2D) Set2D();
        else Set3D();
    }

    // Установить вид 2D
    public void Set2D()
    {
        is2D = true;

        //изменить параметры камеры
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(90, 0, 0);
        camera.orthographic = true;

        //отключить тени
        light.shadows = LightShadows.None;
    }

    // Установить вид 3D
    public void Set3D()
    {
        is2D = false;

        //изменить параметры камеры
        transform.localPosition = new Vector3(0, 0, -5.14f);
        transform.localRotation = Quaternion.Euler(63.94f, 0, 0);
        camera.orthographic = false;

        //включить тени
        light.shadows = LightShadows.Soft;
    }

    void Update()
    {
        //вращение камеры вокруг поля
        float speed_h = Input.GetAxis("Horizontal");
        cameraAnchor.angularVelocity = new Vector3(0, -speed_h, 0);

        //приближение/отдаление камеры
        float speed_mw = Input.GetAxis("Mouse ScrollWheel");
        camera.fieldOfView -= speed_mw * 10;
        camera.orthographicSize -= speed_mw;

        //установка ограничений на параметры камеры
        if (camera.fieldOfView < 60)
            camera.fieldOfView = 60;
        if (camera.orthographicSize < 8)
            camera.orthographicSize = 8;
    }
}
