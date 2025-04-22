using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackSystem : MonoBehaviour
{
    [Serializable]
    public class Attack
    {
        public Vector2 direction = Vector2.right; // Направление удара
        public float length = 1f; // Длина луча
        public LayerMask hitLayers; // Слои, по которым будет проверяться попадание
        public Action<RaycastHit2D[]> onHit; // Метод, вызываемый при попадании
    }

    [Serializable]
    public class Combo
    {
        public string name; // Название комбо (для удобства)
        public List<KeyCode> inputSequence = new List<KeyCode>(); // Последовательность кнопок
        public List<Attack> attacks = new List<Attack>(); // Атаки в этом комбо
    }

    public List<Combo> combos = new List<Combo>(); // Все возможные комбо
    public float inputDelay = 0.5f; // Время на ввод следующей атаки

    private List<KeyCode> currentInput = new List<KeyCode>(); // Введенная игроком последовательность
    private Combo currentCombo = null;
    private int currentAttackIndex = 0;
    private bool isAttacking = false;
    private bool facingRight = true; // Направление игрока
    private Coroutine comboResetCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) RegisterInput(KeyCode.Mouse0); // ЛКМ
        if (Input.GetKeyDown(KeyCode.Mouse1)) RegisterInput(KeyCode.Mouse1); // ПКМ
    }

    private void RegisterInput(KeyCode key)
    {
        if (isAttacking) return; // Не регистрировать ввод, если идет атака

        currentInput.Add(key);
        Combo matchedCombo = FindMatchingCombo();

        if (matchedCombo != null)
        {
            currentCombo = matchedCombo;
            StartCoroutine(ExecuteCombo());
        }

        if (comboResetCoroutine != null) StopCoroutine(comboResetCoroutine);
        comboResetCoroutine = StartCoroutine(ResetInputAfterDelay());
    }

    private Combo FindMatchingCombo()
    {
        foreach (var combo in combos)
        {
            if (combo.inputSequence.Count < currentInput.Count) continue;

            bool match = true;
            for (int i = 0; i < currentInput.Count; i++)
            {
                if (combo.inputSequence[i] != currentInput[i])
                {
                    match = false;
                    break;
                }
            }

            if (match) return combo;
        }

        return null;
    }

    private IEnumerator ExecuteCombo()
    {
        isAttacking = true;
        currentAttackIndex = 0;

        while (currentCombo != null && currentAttackIndex < currentCombo.attacks.Count)
        {
            ExecuteAttack(currentCombo.attacks[currentAttackIndex]);
            currentAttackIndex++;
            yield return new WaitForSeconds(0.2f);
        }

        ResetComboState();
    }

    private void ExecuteAttack(Attack attack)
    {
        Vector2 attackDir = facingRight ? attack.direction : new Vector2(-attack.direction.x, attack.direction.y);
        Vector2 origin = transform.position;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, attackDir, attack.length, attack.hitLayers);
        if (hits.Length > 0 && attack.onHit != null)
        {
            attack.onHit.Invoke(hits);
        }
    }

    private IEnumerator ResetInputAfterDelay()
    {
        yield return new WaitForSeconds(inputDelay);
        currentInput.Clear();
        currentCombo = null;
    }

    private void ResetComboState()
    {
        isAttacking = false;
        currentInput.Clear();
        currentCombo = null;
    }

    public void Flip(bool faceRight)
    {
        facingRight = faceRight;
    }

    private void OnDrawGizmos()
    {
        if (currentCombo == null || currentCombo.attacks.Count == 0) return;

        Gizmos.color = Color.red;
        Vector2 origin = transform.position;

        foreach (var attack in currentCombo.attacks)
        {
            Vector2 attackDir = facingRight ? attack.direction : new Vector2(-attack.direction.x, attack.direction.y);
            Gizmos.DrawLine(origin, origin + attackDir * attack.length);
        }
    }
}
