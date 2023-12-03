using UnityEngine;

public class Keypad : Interactible
{
    [SerializeField] private GameObject door;
    private bool doorOpen;
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }
}
