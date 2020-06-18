using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PickupWeights))]
public class PickupWeightUIE : PropertyDrawer
{
    //public override VisualElement CreatePropertyGUI(SerializedProperty property)
    //{
    //    // Create property container element.
    //    var container = new VisualElement();

    //    // Create property fields.
    //    var amountField = new PropertyField(property.FindPropertyRelative("Type"));
    //    var unitField = new PropertyField(property.FindPropertyRelative("Weight"));
    //    //var nameField = new PropertyField(property.FindPropertyRelative("name"), "Fancy Name");

    //    // Add fields to the container.
    //    container.Add(amountField);
    //    container.Add(unitField);
    //    //container.Add(nameField);

    //    return container;
    //}

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x, position.y, 30, position.height);
        var unitRect = new Rect(position.x + 35, position.y, 150, position.height);
        var typeRect = new Rect(position.x + 155, position.y, 150, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels

        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("Weight"), GUIContent.none);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("Prefab"), GUIContent.none);

        //var pickupType = property.FindPropertyRelative("Prefab.PickupType");
        //var pickupName = pickupType.enumNames[pickupType.enumValueIndex];
        //EditorGUI.LabelField(typeRect, pickupName);
        //EditorGUI.LabelField(typeRect, property.FindPropertyRelative("Pickup.PickupType"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
