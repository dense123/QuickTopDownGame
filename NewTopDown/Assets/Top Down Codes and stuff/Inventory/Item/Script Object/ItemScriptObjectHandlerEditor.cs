using UnityEditor;
using gui = UnityEditor.EditorGUILayout;

[CustomEditor(typeof(ItemScriptObjectHandler))]
public class ItemScriptObjectHandlerEditor : Editor
{
    string rb = "rb";
    string SR = "spriteRenderer";
    string SImage = "spriteImage";
    string currentScript = "m_Script";

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

       
        EditorGUI.BeginDisabledGroup(true);
        displayGUICuzImLazy(serializedObject.FindProperty("itemID"));
        EditorGUI.EndDisabledGroup();
        displayGUICuzImLazy(serializedObject.FindProperty("itemData"), serializedObject.FindProperty("description"));


        //SerializedProperty property = serializedObject.GetIterator();
        //while (property.NextVisible(true))
        //{
        //    if (property.name == spriteRenderer.name || property.name == spriteImage.name || property.name == currentScript || property.name == isUIItem.name)
        //    {
        //        continue;
        //    }
        //    gui.PropertyField(property);
        //}
        SerializedProperty isUIItem = serializedObject.FindProperty("isUIItem");
        SerializedProperty rigidbody = serializedObject.FindProperty(rb);
        SerializedProperty spriteRenderer = serializedObject.FindProperty(SR);
        SerializedProperty spriteImage = serializedObject.FindProperty(SImage);

        gui.PropertyField(isUIItem);
        gui.PropertyField(isUIItem.boolValue ? spriteImage: spriteRenderer);

        if(!isUIItem.boolValue)
            gui.PropertyField(rigidbody);

        serializedObject.ApplyModifiedProperties();
    }

    void displayGUICuzImLazy(SerializedProperty property1)
    {
        gui.PropertyField(property1);
    }
    void displayGUICuzImLazy(SerializedProperty property1, SerializedProperty property2)
    {
        gui.PropertyField(property1);
        gui.PropertyField(property2);
    }
    void displayGUICuzImLazy(SerializedProperty property1, SerializedProperty property2, SerializedProperty property3)
    {
        gui.PropertyField(property1);
        gui.PropertyField(property2);
        gui.PropertyField(property3);
    }
    void displayGUICuzImLazy(SerializedProperty property1, SerializedProperty property2, SerializedProperty property3, SerializedProperty property4)
    {
        gui.PropertyField(property1);
        gui.PropertyField(property2);
        gui.PropertyField(property3);
        gui.PropertyField(property4);
    }
    void displayGUICuzImLazy(SerializedProperty property1, SerializedProperty property2, SerializedProperty property3, SerializedProperty property4, SerializedProperty property5)
    {
        gui.PropertyField(property1);
        gui.PropertyField(property2);
        gui.PropertyField(property3);
        gui.PropertyField(property4);
        gui.PropertyField(property5);
    }
}
