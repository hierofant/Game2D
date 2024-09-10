using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameValuesController : MonoBehaviour
{
    [System.Serializable]
    public class GameParameter
    {
        public string parameterName;         // Имя параметра (например, "Health")
        public float currentValue;           // Текущее значение параметра
        public float minValue;               // Минимальное значение
        public float maxValue;               // Максимальное значение
        public float defaultValue;           // Значение по умолчанию

        // Опции отображения
        public bool enableDisplay = false;   // Включить отображение
        public DisplayType displayType;      // Тип отображения
        public Text text;

        // Управление изменением значения во времени
        public bool enableTimeBasedChange = false;  // Включить изменение со временем
        public float changeRate;             // Скорость изменения значения (например, +1 в секунду)

        // Метод для сброса параметра к значению по умолчанию
        public void ResetToDefault()
        {
            currentValue = defaultValue;
        }

        // Метод для изменения значения с проверкой на границы
        public void ModifyValue(float amount)
        {
            currentValue = Mathf.Clamp(currentValue + amount, minValue, maxValue);
        }

        // Метод для автоматического изменения значения со временем
        public void UpdateOverTime(float deltaTime)
        {
            if (enableTimeBasedChange)
            {
                ModifyValue(changeRate * deltaTime);
            }
        }
    }

    // Перечисление для выбора типа отображения
    public enum DisplayType
    {
        None,
        Disk,
        Bar,
        Number,
        Objects
    }

    public GameParameter[] parameters;  // Массив параметров

    void Update()
    {
        foreach (var parameter in parameters)
        {
            parameter.UpdateOverTime(Time.deltaTime);  // Обновляем значения со временем, если включено

            if (parameter.enableDisplay)
            {
                DisplayParameter(parameter);  // Отображаем значение, если включено отображение
            }
        }
    }

    // Метод для поиска параметра по имени и изменения его значения
    public void ModifyParameter(string name, float amount)
    {
        GameParameter parameter = GetParameterByName(name);
        if (parameter != null)
        {
            parameter.ModifyValue(amount);
        }
        else
        {
            Debug.LogWarning("Parameter not found: " + name);
        }
    }

    // Метод для поиска параметра по имени
    private GameParameter GetParameterByName(string name)
    {
        foreach (var parameter in parameters)
        {
            if (parameter.parameterName == name)
            {
                return parameter;
            }
        }
        return null;
    }

    // Метод для отображения параметра
    private void DisplayParameter(GameParameter parameter)
    {
        switch (parameter.displayType)
        {
            case DisplayType.Disk:
                // Логика отображения в виде диска
                DisplayAsDisk(parameter);
                break;
            case DisplayType.Bar:
                // Логика отображения в виде шкалы
                DisplayAsBar(parameter);
                break;
            case DisplayType.Number:
                // Логика отображения в виде числа
                DisplayAsNumber(parameter);
                break;
            case DisplayType.Objects:
                // Логика отображения с помощью объектов (например, иконок)
                DisplayAsObjects(parameter);
                break;
        }
    }

    // Методы для различных типов отображения (здесь просто заглушки)
    private void DisplayAsDisk(GameParameter parameter)
    {
        // Пример: отобразить круговой индикатор на UI
        Debug.Log($"Display {parameter.parameterName} as Disk with value {parameter.currentValue}");
    }

    private void DisplayAsBar(GameParameter parameter)
    {
        // Пример: отобразить шкалу (например, полоску здоровья)
        Debug.Log($"Display {parameter.parameterName} as Bar with value {parameter.currentValue}");
    }

    private void DisplayAsNumber(GameParameter parameter)
    {
        // Пример: отобразить числовое значение (например, количество очков)
        Debug.Log($"Display {parameter.parameterName} as Number with value {parameter.currentValue}");
    }

    private void DisplayAsObjects(GameParameter parameter)
    {
        // Пример: отобразить объекты (например, количество сердечек)
        Debug.Log($"Display {parameter.parameterName} as Objects with value {parameter.currentValue}");
    }

    // Метод для сброса всех параметров к значениям по умолчанию
    public void ResetAllToDefault()
    {
        foreach (var parameter in parameters)
        {
            parameter.ResetToDefault();
        }
    }
}