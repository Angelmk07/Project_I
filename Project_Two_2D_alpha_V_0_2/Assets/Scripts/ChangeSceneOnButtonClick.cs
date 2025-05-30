using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneOnButtonClick : MonoBehaviour
{
    [Header("Настройки смены сцены")]
    [SerializeField] private string sceneName; // Имя сцены для загрузки
    [SerializeField] private float delayBeforeLoad = 0f; // Задержка перед загрузкой
    [SerializeField] private Button targetButton; // Кнопка, которая вызывает смену сцены

    [Header("Эффекты перехода")]
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private string triggerName = "FadeOut";
    [SerializeField] private float transitionTime = 1f;

    private void Start()
    {
        // Автоматически находим кнопку, если не назначена
        if (targetButton == null)
            targetButton = GetComponent<Button>();

        // Добавляем обработчик нажатия
        if (targetButton != null)
            targetButton.onClick.AddListener(OnButtonClick);
        else
            Debug.LogError("Не найдена кнопка для ChangeSceneOnButtonClick!");
    }

    private void OnButtonClick()
    {
        StartCoroutine(LoadSceneWithEffects());
    }

    private IEnumerator LoadSceneWithEffects()
    {
        // Запускаем анимацию перехода, если есть
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger(triggerName);
            yield return new WaitForSeconds(transitionTime);
        }

        // Ждем указанную задержку
        yield return new WaitForSeconds(delayBeforeLoad);

        // Загружаем сцену
        SceneManager.LoadScene(sceneName);
    }

    // Метод для вызова из других скриптов
    public void ChangeScene(string newSceneName)
    {
        sceneName = newSceneName;
        StartCoroutine(LoadSceneWithEffects());
    }
}