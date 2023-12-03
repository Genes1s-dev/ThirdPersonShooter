using UnityEngine;

public abstract class Interactible : MonoBehaviour   
{ 
    public string promptMessage; //сообщение, всплывающее перед игроком, если с объектом можно взаимодействовать

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        
    }
}
