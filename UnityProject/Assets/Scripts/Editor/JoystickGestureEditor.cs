using TouchScript.Gestures;
using TouchScript.Editor.Gestures;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JoystickGesture), true)]
internal sealed class JoystickGestureEditor : GestureEditor
{
    public static readonly GUIContent BORDER = new GUIContent("Border image.");
    public static readonly GUIContent PIVOT = new GUIContent("Pivot image.");
    public static readonly GUIContent DIST_THRESHOLD = new GUIContent(
        "Max. distance between the joystick center and the pivot center"
        );

    public static readonly GUIContent TEXT_HELP = new GUIContent(
        "Enables a joystick centred on the firsttouched screen position."
        );

    private SerializedProperty border;
    private SerializedProperty pivot;
    private SerializedProperty distThreshold;

    protected override void OnEnable ()
    {
        base.OnEnable();
        border = serializedObject.FindProperty("border");
        pivot = serializedObject.FindProperty("pivot");
        distThreshold = serializedObject.FindProperty("distThreshold");
    }

    protected override void drawBasic ()
    {

    }

    protected override void drawGeneral ()
    {
        EditorGUIUtility.labelWidth = 180;
        EditorGUILayout.PropertyField(border, BORDER);
        EditorGUILayout.PropertyField(pivot, PIVOT);
        EditorGUILayout.PropertyField(distThreshold, DIST_THRESHOLD);
    }

    protected override GUIContent getHelpText ()
    {
        return TEXT_HELP;
    }

}
