using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
[TestFixture]
public class PlayerMovementTests : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    [SetUp]
    public void SetUp()
    {
        // Создаем объект игрока
        playerObject = new GameObject("Player");

        // Добавляем компоненты
        playerMovement = playerObject.AddComponent<PlayerMovement>();
        rb = playerObject.AddComponent<Rigidbody2D>();

        // Инициализируем компоненты, которые должны быть заданы вручную


        // Другие компоненты, которые могут понадобиться, можно инициализировать здесь
    }

    [UnityTest]
    public IEnumerator PlayerMovesRight()
    {
        // Запускаем движение вправо
        playerMovement.Move(1f);

        // Ждем немного, чтобы физика обновилась
        yield return new WaitForSeconds(0.1f);

        // Проверяем, что скорость больше 0 (игрок двигается вправо)
        Assert.Greater(rb.velocity.x, 0, "Игрок должен двигаться вправо.");
    }

    // Другие тесты можно добавить здесь...

    [TearDown]
    public void TearDown()
    {
        // Уничтожаем объект игрока после каждого теста
        GameObject.Destroy(playerObject);
    }
}
