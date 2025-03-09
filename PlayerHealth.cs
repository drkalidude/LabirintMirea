using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHits = 3;
    private int currentHits = 0;

    public void TakeDamage()
    {
        currentHits++;
        Debug.Log("Игрок получил урон! Попаданий: " + currentHits);

        if (currentHits >= maxHits)
        {
            RestartLevel(); 
        }
    }

    private void RestartLevel()
    {
        Debug.Log("Игрок проиграл! Перезапуск...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
