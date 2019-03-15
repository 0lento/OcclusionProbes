#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(OcclusionProbes))]
[CanEditMultipleObjects]
public class OcclusionProbesEditor : Editor
{
	override public void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (targets.Length > 1)
			return;

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		if(GUILayout.Button("Add a detail occlusion probe set"))
			AddOcclusionProbesDetail();

		EditorGUILayout.Space();

		if(GUILayout.Button("Bake ambient probe"))
			BakeAmbientProbe();

		EditorGUILayout.Space();
	}

	void AddOcclusionProbesDetail()
	{
		OcclusionProbes occlusionProbes = (OcclusionProbes)target;
			
		GameObject go = new GameObject("OcclusionProbesDetail");
		OcclusionProbesDetail occlusionProbesDetail = go.AddComponent<OcclusionProbesDetail>();

		go.transform.parent = occlusionProbes.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;
		occlusionProbes.m_OcclusionProbesDetail.Add(occlusionProbesDetail);
		EditorUtility.SetDirty(occlusionProbes);
	}

	void BakeAmbientProbe()
	{
		OcclusionProbes occlusionProbes = (OcclusionProbes)target;
		occlusionProbes.BakeAmbientProbe();
		AssetDatabase.SaveAssets();
	}

}
#endif