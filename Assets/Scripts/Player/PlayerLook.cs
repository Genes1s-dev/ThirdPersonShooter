using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public new Camera camera;
    private float xRotation = 0f;
    private float yRotation = 0f;

    [SerializeField] float xSensitivity = 30f;
    [SerializeField] float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
    
        float mouseX = input.x;
        float mouseY = input.y;

        //реализуем вращение камеры вниз/вверх
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);


        camera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);


        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);  //реализуем вращение игрока налево и направо
    }
}
