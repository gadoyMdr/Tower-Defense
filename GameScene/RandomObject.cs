using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using UnityEditor;


[System.Serializable]
public class Set
{
    public GameObject gameObject;

    public float percentage;
}

[System.Serializable]
public class RandomGOInSet
{
    public List<Set> set = new List<Set>();

    public GameObject GetRandomObject()
    {
        int random = Random.Range(0, 101);
        float temp = 0;
        foreach (Set s in set)
        {
            temp += s.percentage;
            if (temp >= random) return s.gameObject;
        }
        return null;
    }

    public float GetMaxmimumValue(int indexOfTheCurrentlyModifiedSet)
    {
        float total = 0;

        for (int i = 0; i < set.Count; i++)
            if (i != indexOfTheCurrentlyModifiedSet)
                total += set[i].percentage;


        return 100 - total;
    }
}


#if UNITY_EDITOR



[CustomPropertyDrawer(typeof(Set))]
public class DrawerSet : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {


        SerializedProperty percentage = property.FindPropertyRelative("percentage");
        SerializedProperty gameObject = property.FindPropertyRelative("gameObject");

        percentage.floatValue = EditorGUI.Slider(new Rect(position.x, position.y, position.width / 2 - 40, position.height), percentage.floatValue, 0, 100);

        gameObject.objectReferenceValue = EditorGUI.ObjectField(new Rect(position.width / 2 + 40, position.y, position.width / 2, position.height), gameObject.objectReferenceValue, typeof(GameObject), false);

    }
}


[CustomPropertyDrawer(typeof(RandomGOInSet))]
public class DrawerRandomGOInSet : PropertyDrawer
{

    private bool Initialize = false;
    private bool InitializeArray = false;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!Initialize)
        {
            Initialize = true;
            return;
        }
        GUIStyle redBigLabel = new GUIStyle(EditorStyles.label);
        redBigLabel.normal.textColor = Color.red;


        SerializedProperty set = property.FindPropertyRelative("set");


        List<float> valueBeforeChange = new List<float>();
        for (int i = 0; i < set.arraySize; i++)
            valueBeforeChange.Add(set.GetArrayElementAtIndex(i).FindPropertyRelative("percentage").floatValue);

        EditorGUILayout.PropertyField(set, true);

        if(set.arraySize != 0 && !InitializeArray)
        {
            InitializeArray = true;
            return;
        }

        for (int i = 0; i < set.arraySize; i++)
            if (set.GetArrayElementAtIndex(i).FindPropertyRelative("percentage").floatValue != valueBeforeChange[i])
            {
                SerializedProperty changedSlider = set.GetArrayElementAtIndex(i);


                RandomGOInSet currentRandomGOInSet = (RandomGOInSet)GetTargetObjectOfProperty(property);

                float value = currentRandomGOInSet.GetMaxmimumValue(int.Parse(changedSlider.propertyPath.Substring(changedSlider.propertyPath.Length - 2, 1)));


                if (changedSlider.FindPropertyRelative("percentage").floatValue > value)
                    changedSlider.FindPropertyRelative("percentage").floatValue = value;
            }


        if (valueBeforeChange.Sum(x => x) < 100)
            EditorGUILayout.LabelField
                (
                $"Becarful, the total of probability isn't equal to 100. Total : {(valueBeforeChange.Sum(x => x)).ToString("F2")}",
                redBigLabel
                );
        if (valueBeforeChange.Sum(x => x) > 100)
            EditorGUILayout.LabelField
                (
                "Refresh the lastly-added entry's slider to refresh the total points",
                redBigLabel
                );


    }


    //Thanks to :
    //https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBaseEditor/EditorHelper.cs

    public static object GetTargetObjectOfProperty(SerializedProperty prop)
    {
        if (prop == null) return null;

        var path = prop.propertyPath.Replace(".Array.data[", "[");
        object obj = prop.serializedObject.targetObject;
        var elements = path.Split('.');
        foreach (var element in elements)
        {
            if (element.Contains("["))
            {
                var elementName = element.Substring(0, element.IndexOf("["));
                var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue_Imp(obj, elementName, index);
            }
            else
            {
                obj = GetValue_Imp(obj, element);
            }
        }
        return obj;
    }

    private static object GetValue_Imp(object source, string name, int index)
    {
        var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
        if (enumerable == null) return null;
        var enm = enumerable.GetEnumerator();
        //while (index-- >= 0)
        //    enm.MoveNext();
        //return enm.Current;

        for (int i = 0; i <= index; i++)
        {
            if (!enm.MoveNext()) return null;
        }
        return enm.Current;
    }

    private static object GetValue_Imp(object source, string name)
    {
        if (source == null)
            return null;
        var type = source.GetType();

        while (type != null)
        {
            var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (f != null)
                return f.GetValue(source);

            var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (p != null)
                return p.GetValue(source, null);

            type = type.BaseType;
        }
        return null;
    }

}
#endif