using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private new Camera camera;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    private void Start()
    {
        camera = GetComponent<PlayerLook>().camera;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        playerUI.UpdateText(string.Empty);
        
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);  
        RaycastHit hitInfo;  
        if (Physics.Raycast(ray, out hitInfo, distance, mask)) //возвращаем bool 
        {
            if (hitInfo.collider.GetComponent<Interactible>() != null)
            {
                Interactible interactable = hitInfo.collider.GetComponent<Interactible>();
                playerUI.UpdateText(interactable.promptMessage);  
                if (inputManager.inputActions.Interact.triggered) //аналог Input.GetKeyDown в старой версии     
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
