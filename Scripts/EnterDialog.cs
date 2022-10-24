using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player")
        {
            enterDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Player")
        {
            enterDialog.SetActive(false);
        }
    }
}
