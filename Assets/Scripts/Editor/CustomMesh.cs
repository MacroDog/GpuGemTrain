using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CustomMesh : EditorWindow {

	public int size = 1;

	public int segment = 5;

	public Vector3[] vertices;

	public Vector2[] uv;

	public int[] triangles;

	public Material mat;
	private static EditorWindow window;

	[MenuItem ("Tools/MeshCreate")]
	static void Init () {
		window = GetWindow (typeof (CustomMesh));
		window.Show ();
	}

	void OnGUI () {
		EditorGUILayout.BeginVertical ();
		size = EditorGUILayout.IntField ("大小", size);
		segment = EditorGUILayout.IntField ("分段", segment);
		mat = EditorGUILayout.ObjectField ("材质", mat, typeof (Material), true) as Material;
		if (GUILayout.Button ("生成")) {
			CreateMesh ();
		}
		EditorGUILayout.EndVertical ();
	}

	public void CreateMesh () {
		Mesh mesh = new Mesh ();
		mesh.name = "Custom";
		Generate ();
		mesh.Clear ();
		mesh.vertices = this.vertices;
		mesh.uv = this.uv;
		mesh.triangles = this.triangles;
		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();
		mesh.RecalculateTangents ();
		GameObject go_mesh = new GameObject ();
		go_mesh.AddComponent<MeshFilter> ().mesh = mesh;
		var mrd = go_mesh.AddComponent<MeshRenderer> ();
		if (mat != null) {
			mrd.material = mat;
		}
	}

	public void Generate () {
		vertices = new Vector3[(segment + 1) * (segment + 1)];
		uv = new Vector2[vertices.Length];
		float m = (float) size / segment;
		int num = 0;
		for (int i = 0; i <= segment; i++) {
			for (int j = 0; j <= segment; j++) {
				vertices[num] = new Vector3 (j * m, 0, i * m);
				uv[num] = new Vector2 ((float) j / segment, (float) i / segment);
				num++;
			}
		}
		/// <summary>
		/// 设置索引
		/// </summary>

		triangles = new int[segment * segment * 6];
		int index = 0; //用来给三角形索引计数

		for (int i = 0; i < segment; i++) {
			for (int j = 0; j < segment; j++) {
				int line = segment + 1;
				int self = j + (i * line);

				triangles[index] = self;
				triangles[index + 1] = self + line + 1;
				triangles[index + 2] = self + 1;
				triangles[index + 3] = self;
				triangles[index + 4] = self + line;
				triangles[index + 5] = self + line + 1;
				index += 6;
			}
		}

	}
}