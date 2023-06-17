using UnityEngine;

public class RemoveMissingScripts : MonoBehaviour
{
    [ContextMenu("Clean Up Missing Scripts")]
    private void CleanUpMissingScriptsFromChildren()
    {
        CleanUpMissingScriptsRecursive(transform);
    }

    private void CleanUpMissingScriptsRecursive(Transform parent)
    {
        // Перебираем все дочерние объекты
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);

            // Удаляем удаленные скрипты из текущего дочернего объекта
            CleanUpMissingScriptsFromObject(child.gameObject);

            // Рекурсивно вызываем этот метод для всех дочерних объектов
            CleanUpMissingScriptsRecursive(child);
        }
    }

    private void CleanUpMissingScriptsFromObject(GameObject gameObject)
    {
        Component[] components = gameObject.GetComponents<Component>();

        int missingScriptCount = 0;

        // Проходим по всем компонентам объекта
        for (int i = components.Length - 1; i >= 0; i--)
        {
            if (components[i] == null)
            {
#if UNITY_EDITOR
                // Удаляем удаленный скрипт из объекта в редакторе
                UnityEditor.Undo.DestroyObjectImmediate(components[i]);
#else
            // Удаляем удаленный скрипт из объекта во время выполнения
            DestroyImmediate(components[i]);
#endif
                missingScriptCount++;
            }
        }

        if (missingScriptCount > 0)
        {
            Debug.Log($"Removed {missingScriptCount} missing script(s) from {gameObject.name}.");
        }
    }


    // [ContextMenu("Remove Missing Scripts")]
    // private void RemoveMissingScriptsFromChildren()
    // {
    //     RemoveMissingScriptsRecursive(transform);
    // }
    //
    // private void RemoveMissingScriptsRecursive(Transform parent)
    // {
    //     // Перебираем все дочерние объекты
    //     for (int i = parent.childCount - 1; i >= 0; i--)
    //     {
    //         Transform child = parent.GetChild(i);
    //
    //         // Удаляем удаленные скрипты из текущего дочернего объекта
    //         RemoveMissingScriptsFromObject(child.gameObject);
    //
    //         // Рекурсивно вызываем этот метод для всех дочерних объектов
    //         RemoveMissingScriptsRecursive(child);
    //     }
    // }
    //
    // private void RemoveMissingScriptsFromObject(GameObject gameObject)
    // {
    //     Component[] components = gameObject.GetComponents<Component>();
    //
    //     // Счетчик для отслеживания удаленных скриптов
    //     int componentCount = 0;
    //
    //     for (int i = components.Length - 1; i >= 0; i--)
    //     {
    //         if (components[i] == null)
    //         {
    //             // Удаляем удаленный скрипт из объекта
    //             DestroyImmediate(components[i]);
    //             componentCount++;
    //         }
    //     }
    //
    //     if (componentCount > 0)
    //     {
    //         Debug.Log($"Removed {componentCount} missing script(s) from {gameObject.name}.");
    //     }
    // }
}