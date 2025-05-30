using UnityEngine;
using System.Collections;

public class LiftObjectOnInteraction : MonoBehaviour
{
    [Header("Настройки подъема")]
    [SerializeField] private float liftHeight = 2f; // Высота подъема
    [SerializeField] private float liftSpeed = 1f; // Скорость подъема
    [SerializeField] private float returnDelay = 3f; // Задержка перед возвратом
    [SerializeField] private bool returnsToOriginalPosition = true; // Возвращается ли обратно

    [Header("Настройки взаимодействия")]
    [SerializeField] private KeyCode interactionKey = KeyCode.E; // Клавиша взаимодействия
    [SerializeField] private string playerTag = "Player"; // Тег игрока
    [SerializeField] private GameObject interactionPrompt; // Подсказка для взаимодействия

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isLifting = false;
    private bool isPlayerInTrigger = false;
    private bool isLifted = false;

    private void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition + Vector3.up * liftHeight;

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        // Проверка нажатия кнопки взаимодействия, когда игрок в триггере
        if (isPlayerInTrigger && Input.GetKeyDown(interactionKey))
        {
            if (!isLifted)
            {
                StartLifting();
            }
        }

        // Логика подъема
        if (isLifting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, liftSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isLifting = false;
                isLifted = true;

                if (returnsToOriginalPosition)
                {
                    StartCoroutine(ReturnToOriginalPosition());
                }
            }
        }
    }

    private void StartLifting()
    {
        isLifting = true;
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(returnDelay);

        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, liftSpeed * Time.deltaTime);
            yield return null;
        }

        isLifted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !isLifted)
        {
            isPlayerInTrigger = true;
            if (interactionPrompt != null)
                interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInTrigger = false;
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }

    // Для отладки - сброс положения
    public void ResetPosition()
    {
        StopAllCoroutines();
        transform.position = originalPosition;
        isLifting = false;
        isLifted = false;
    }
}