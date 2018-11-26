using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaveSystem))]
public class WaveSystemEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		WaveSystem waveSys = (WaveSystem)target;

		//EditorGUILayout.
	}
}
