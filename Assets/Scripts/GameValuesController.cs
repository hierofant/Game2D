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
        public string parameterName;         // ��� ��������� (��������, "Health")
        public float currentValue;           // ������� �������� ���������
        public float minValue;               // ����������� ��������
        public float maxValue;               // ������������ ��������
        public float defaultValue;           // �������� �� ���������

        // ����� �����������
        public bool enableDisplay = false;   // �������� �����������
        public DisplayType displayType;      // ��� �����������
        public Text text;

        // ���������� ���������� �������� �� �������
        public bool enableTimeBasedChange = false;  // �������� ��������� �� ��������
        public float changeRate;             // �������� ��������� �������� (��������, +1 � �������)

        // ����� ��� ������ ��������� � �������� �� ���������
        public void ResetToDefault()
        {
            currentValue = defaultValue;
        }

        // ����� ��� ��������� �������� � ��������� �� �������
        public void ModifyValue(float amount)
        {
            currentValue = Mathf.Clamp(currentValue + amount, minValue, maxValue);
        }

        // ����� ��� ��������������� ��������� �������� �� ��������
        public void UpdateOverTime(float deltaTime)
        {
            if (enableTimeBasedChange)
            {
                ModifyValue(changeRate * deltaTime);
            }
        }
    }

    // ������������ ��� ������ ���� �����������
    public enum DisplayType
    {
        None,
        Disk,
        Bar,
        Number,
        Objects
    }

    public GameParameter[] parameters;  // ������ ����������

    void Update()
    {
        foreach (var parameter in parameters)
        {
            parameter.UpdateOverTime(Time.deltaTime);  // ��������� �������� �� ��������, ���� ��������

            if (parameter.enableDisplay)
            {
                DisplayParameter(parameter);  // ���������� ��������, ���� �������� �����������
            }
        }
    }

    // ����� ��� ������ ��������� �� ����� � ��������� ��� ��������
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

    // ����� ��� ������ ��������� �� �����
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

    // ����� ��� ����������� ���������
    private void DisplayParameter(GameParameter parameter)
    {
        switch (parameter.displayType)
        {
            case DisplayType.Disk:
                // ������ ����������� � ���� �����
                DisplayAsDisk(parameter);
                break;
            case DisplayType.Bar:
                // ������ ����������� � ���� �����
                DisplayAsBar(parameter);
                break;
            case DisplayType.Number:
                // ������ ����������� � ���� �����
                DisplayAsNumber(parameter);
                break;
            case DisplayType.Objects:
                // ������ ����������� � ������� �������� (��������, ������)
                DisplayAsObjects(parameter);
                break;
        }
    }

    // ������ ��� ��������� ����� ����������� (����� ������ ��������)
    private void DisplayAsDisk(GameParameter parameter)
    {
        // ������: ���������� �������� ��������� �� UI
        Debug.Log($"Display {parameter.parameterName} as Disk with value {parameter.currentValue}");
    }

    private void DisplayAsBar(GameParameter parameter)
    {
        // ������: ���������� ����� (��������, ������� ��������)
        Debug.Log($"Display {parameter.parameterName} as Bar with value {parameter.currentValue}");
    }

    private void DisplayAsNumber(GameParameter parameter)
    {
        // ������: ���������� �������� �������� (��������, ���������� �����)
        Debug.Log($"Display {parameter.parameterName} as Number with value {parameter.currentValue}");
    }

    private void DisplayAsObjects(GameParameter parameter)
    {
        // ������: ���������� ������� (��������, ���������� ��������)
        Debug.Log($"Display {parameter.parameterName} as Objects with value {parameter.currentValue}");
    }

    // ����� ��� ������ ���� ���������� � ��������� �� ���������
    public void ResetAllToDefault()
    {
        foreach (var parameter in parameters)
        {
            parameter.ResetToDefault();
        }
    }
}