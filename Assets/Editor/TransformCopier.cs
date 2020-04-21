// TransformCopier.cs v 1.2
// homepage: http://wiki.unity3d.com/index.php/CopyTransform

using UnityEngine;
using UnityEditor;
using System.Collections;

public class TransformCopier : ScriptableObject
{

    private static Vector3 position;
    private static Quaternion rotation;
    private static Vector3 scale;

    [MenuItem("CONTEXT/Transform/Copy Independent Values", false, 151)]
    static void DoRecord()
    {
        position = Selection.activeTransform.localPosition;
        rotation = Selection.activeTransform.localRotation;
        scale = Selection.activeTransform.localScale;
    }

    // PASTE POSITION:
    [MenuItem("CONTEXT/Transform/Paste Position", false, 200)]
    static void DoApplyPositionXYZ()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Position" + selection.name);
            selection.localPosition = position;
        }
    }

    [MenuItem("CONTEXT/Transform/Paste Position X", false, 201)]
    static void DoApplyPositionX()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Position X" + selection.name);
            selection.localPosition = new Vector3(position.x, selection.localPosition.y, selection.localPosition.z);
        }
    }

    [MenuItem("CONTEXT/Transform/Paste Position Y", false, 202)]
    static void DoApplyPositionY()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Position Y" + selection.name);
            selection.localPosition = new Vector3(selection.localPosition.x, position.y, selection.localPosition.z);
        }
    }

    [MenuItem("CONTEXT/Transform/Paste Position Z", false, 203)]
    static void DoApplyPositionZ()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Position Z" + selection.name);
            selection.localPosition = new Vector3(selection.localPosition.x, selection.localPosition.y, position.z);
        }
    }

    // PASTE ROTATION:
    [MenuItem("CONTEXT/Transform/Paste Rotation", false, 250)]
    static void DoApplyRotationXYZ()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Rotation" + selection.name);
            selection.localRotation = rotation;
        }
    }

    [MenuItem("CONTEXT/Transform/Paste Rotation X", false, 251)]
    static void DoApplyRotationX()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Rotation X" + selection.name);
            selection.localRotation = Quaternion.Euler(rotation.eulerAngles.x, selection.localRotation.eulerAngles.y, selection.localRotation.eulerAngles.z);
        }
    }

    [MenuItem("CONTEXT/Transform/Paste Rotation Y", false, 252)]
    static void DoApplyRotationY()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Rotation Y" + selection.name);
            selection.localRotation = Quaternion.Euler(selection.localRotation.eulerAngles.x, rotation.eulerAngles.y, selection.localRotation.eulerAngles.z);
        }
    }

    [MenuItem("CONTEXT/Transform/Paste Rotation Z", false, 253)]
    static void DoApplyRotationZ()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Rotation Z" + selection.name);
            selection.localRotation = Quaternion.Euler(selection.localRotation.eulerAngles.x, selection.localRotation.eulerAngles.y, rotation.eulerAngles.z);
        }
    }

    // PASTE SCALE:
    [MenuItem("CONTEXT/Transform/Paste Scale", false, 300)]
    static void DoApplyScaleXYZ()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Scale" + selection.name);
            selection.localScale = scale;
        }
    }

    [MenuItem("CONTEXT/Transform/Paste Scale X", false, 301)]
    static void DoApplyScaleX()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Scale X" + selection.name);
            selection.localScale = new Vector3(scale.x, selection.localScale.y, selection.localScale.z);
        }
    }

    [MenuItem("CONTEXT/Transform/Paste Scale Y", false, 302)]
    static void DoApplyScaleY()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Scale Y" + selection.name);
            selection.localScale = new Vector3(selection.localScale.x, scale.y, selection.localScale.z);
        }
    }

    [MenuItem("CONTEXT/Transform/Paste Scale Z", false, 303)]
    static void DoApplyScaleZ()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Paste Scale Z" + selection.name);
            selection.localScale = new Vector3(selection.localScale.x, selection.localScale.y, scale.z);
        }
    }

    // CHANGE LOCAL ROTATION :
    [MenuItem("CONTEXT/Transform/localRotation.x + 90", false, 350)]
    static void localRotateX90()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "localRotation.x + 90" + selection.name);
            selection.localRotation = selection.localRotation * Quaternion.Euler(90f, 0f, 0f);
        }
    }

    [MenuItem("CONTEXT/Transform/localRotation.y + 90", false, 351)]
    static void localRotateY90()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "localRotation.y + 90" + selection.name);
            selection.localRotation = selection.localRotation * Quaternion.Euler(0f, 90f, 0f);
        }
    }

    [MenuItem("CONTEXT/Transform/localRotation.z + 90", false, 352)]
    static void localRotateZ90()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "localRotation.z + 90" + selection.name);
            selection.localRotation = selection.localRotation * Quaternion.Euler(0f, 0f, 90f);
        }
    }

    // SWAP:
    [MenuItem("CONTEXT/Transform/Swap Y and Z Scale", false, 401)]
    static void SwapYZScale()
    {
        Transform[] selections = Selection.transforms;
        foreach (Transform selection in selections)
        {
            Undo.RecordObject(selection, "Swap Y and Z Scale" + selection.name);
            selection.localScale = new Vector3(selection.localScale.x, selection.localScale.z, selection.localScale.y);
        }
    }
}