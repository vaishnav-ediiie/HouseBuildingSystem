using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
   [MenuItem("Options/Clear Player Prefs")]
   public static void ClearePlayerPrefs() => PlayerPrefs.DeleteAll();
}
