using UnityEngine;
using System;
# if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Only draws the field only if a condition is met. Supports enum, bools, int, floats, long, double.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DvDrawIfAttribute : PropertyAttribute
{
    public string comparedPropertyName { get; private set; }
    public object comparedValue { get; private set; }
    public DisablingType disablingType { get; private set; }
    public ComparisonType comparisonType { get; private set; }

    public enum DisablingType
    {
        ReadOnly = 2,
        DontDraw = 3
    }

    public enum ComparisonType
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanEqual,
        GreaterThan,
        GreaterThanEqual
    }

    public DvDrawIfAttribute(string comparedPropertyName, object comparedValue)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue;
        this.disablingType = DisablingType.DontDraw;
        this.comparisonType = ComparisonType.Equal;
    }

    public DvDrawIfAttribute(string comparedPropertyName, object comparedValue, DisablingType disablingType = DisablingType.DontDraw)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue;
        this.disablingType = disablingType;
        this.comparisonType = ComparisonType.Equal;
    }

    public DvDrawIfAttribute(string comparedPropertyName, object comparedValue, DisablingType disablingType = DisablingType.DontDraw, ComparisonType comparisonType = ComparisonType.Equal)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue;
        this.disablingType = disablingType;
        this.comparisonType = comparisonType;
    }
}

# if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DvDrawIfAttribute))]
public class DvDrawIfPropertyDrawer : PropertyDrawer
{
    DvDrawIfAttribute DvDrawIf;
    SerializedProperty comparedField;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!ShowMe(property) && DvDrawIf.disablingType == DvDrawIfAttribute.DisablingType.DontDraw)
            return 0f;

        // The height of the property should be defaulted to the default height.
        return base.GetPropertyHeight(property, label);
    }

    bool AreEqual(SerializedProperty obj1, object obj2, string path)
    {
        switch (comparedField.type)
        { // Possible extend cases to support your own type
            case "bool":
                return comparedField.boolValue.Equals(DvDrawIf.comparedValue);
            case "Enum":
                return comparedField.enumValueIndex.Equals((int)DvDrawIf.comparedValue);
            case "float":
                return comparedField.floatValue.Equals((float)DvDrawIf.comparedValue);
            case "int":
                return comparedField.intValue.Equals((int)DvDrawIf.comparedValue);
            case "double":
                return comparedField.doubleValue.Equals((double)DvDrawIf.comparedValue);
            case "long":
                return comparedField.longValue.Equals((long)DvDrawIf.comparedValue);
            default:
                Debug.LogError("Error: " + comparedField.type + " is not supported of " + path + "(Type: " + comparedField.type + ")");
                return true;
        }
    }

    private bool ShowMe(SerializedProperty property)
    {
        DvDrawIf = attribute as DvDrawIfAttribute;
        // Replace propertyname to the value from the parameter
        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, DvDrawIf.comparedPropertyName) : DvDrawIf.comparedPropertyName;
        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)
        {
            Debug.LogWarning("Cannot find property with name: " + path);
            return true;
        }
        
        if (   DvDrawIf.comparisonType == DvDrawIfAttribute.ComparisonType.LessThan 
            || DvDrawIf.comparisonType == DvDrawIfAttribute.ComparisonType.LessThanEqual
            || DvDrawIf.comparisonType == DvDrawIfAttribute.ComparisonType.GreaterThan
            || DvDrawIf.comparisonType == DvDrawIfAttribute.ComparisonType.GreaterThanEqual
            )
        {
            if (!(DvDrawIf.comparedValue is float || DvDrawIf.comparedValue is int || DvDrawIf.comparedValue is double || DvDrawIf.comparedValue is long))
            {
                Debug.LogWarning(DvDrawIf.comparisonType + "Comparison between two no-numeric types. comparedPropertyName: \"" + DvDrawIf.comparedPropertyName + "\"");
                return true;
            }
        }


        switch (DvDrawIf.comparisonType)
        {
            case (DvDrawIfAttribute.ComparisonType.Equal):
                {
                    return AreEqual(comparedField, DvDrawIf.comparedValue, path);
                }
            case (DvDrawIfAttribute.ComparisonType.NotEqual):
                {
                    return !AreEqual(comparedField, DvDrawIf.comparedValue, path);
                }
            case (DvDrawIfAttribute.ComparisonType.LessThan):
                {
                    return comparedField.floatValue < (float)DvDrawIf.comparedValue;
                }
            case (DvDrawIfAttribute.ComparisonType.LessThanEqual):
                {
                    return comparedField.floatValue <= (float)DvDrawIf.comparedValue;
                }
            case (DvDrawIfAttribute.ComparisonType.GreaterThan):
                {
                    return comparedField.floatValue > (float)DvDrawIf.comparedValue;
                }
            case (DvDrawIfAttribute.ComparisonType.GreaterThanEqual):
                {
                    return comparedField.floatValue >= (float)DvDrawIf.comparedValue;
                }
            default:
                {
                    return true;
                }
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // If the condition is met, simply draw the field.
        if (ShowMe(property))
        {
            EditorGUI.PropertyField(position, property, new GUIContent(property.displayName));
        } //...check if the disabling type is read only. If it is, draw it disabled
        else if (DvDrawIf.disablingType == DvDrawIfAttribute.DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, new GUIContent(property.displayName));
            GUI.enabled = true;
        }
    }
}
# endif
