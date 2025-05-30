using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTriggerEnter : MonoBehaviour
{
    [Header("Настройки смены сцены")]
    [SerializeField] private string sceneName; // Имя сцены для загрузки
    [SerializeField] private float delayBeforeLoad = 0f; // Задержка перед загрузкой
    [SerializeField] private bool usePlayerTag = true; // Проверять тег игрока
    [SerializeField] private string playerTag = "Player"; // Тег игрока
    [SerializeField] private bool showDebugMessages = true; // Показывать сообщения в консоли

    [Header("Эффекты перехода")]
    [SerializeField] private GameObject transitionEffect; // Эффект перехода (опционально)
    [SerializeField] private float effectDuration = 1f; // Длительность эффекта

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, нужно ли проверять тег и соответствует ли он
        if (!usePlayerTag || other.CompareTag(playerTag))
        {
            if (showDebugMessages)
                Debug.Log($"Игрок вошел в триггер, загрузка сцены: {sceneName}");

            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        // Активируем эффект перехода, если он есть
        if (transitionEffect != null)
        {
            transitionEffect.SetActive(true);
            yield return new WaitForSeconds(effectDuration);
        }

        // Ждем указанную задержку
        yield return new WaitForSeconds(delayBeforeLoad);

        // Загружаем сцену
        SceneManager.LoadScene(sceneName);
    }

    // Метод для вызова смены сцены из других скриптов
    public void ChangeScene(string newSceneName)
    {
        sceneName = newSceneName;
        StartCoroutine(LoadSceneWithDelay());
    }
}