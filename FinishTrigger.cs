using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private ARLevelController levelController;

    public void Initialize(ARLevelController controller)
    {
        levelController = controller;
        Debug.Log("FinishTrigger инициализирован!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Объект вошел в триггер: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок пересек финиш!");
            levelController.CompleteLevel();
        }
    }
}
