using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private float zMaxOffset = 2.4f;
    [SerializeField] private float xMaxOffset = 1.2f;
    
    [Header("GameObjectRefs")]
    [SerializeField] private GameObject aimingScreen;
     private Transform player;
    private Vector3 cameraPosition;
    private LayerMask allButPlayer;
    private void Start()
    {
        cameraPosition = transform.localPosition;
        allButPlayer = LayerMask.GetMask("Default"); //исключаем игрока из расчёта
    }

    public void ResetCamera()
    {
        transform.localPosition = cameraPosition;
        aimingScreen.SetActive(false);
    }

    //Здесь должна была быть обработка движения камеры..
}
