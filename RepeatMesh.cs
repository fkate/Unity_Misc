using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent (typeof(MeshFilter))]
public class RepeatMesh : MonoBehaviour {

	[Header("Mesh")]
	[Tooltip("The base mesh beeing used")]
	public Mesh BaseMesh;

	[Header("Values")]
	[Tooltip("How often the mesh will be repeated on the X axis")]
	public int AmountX = 4;
	[Tooltip("How often the mesh will be repeated on the Y axis")]
	public int AmountY = 1;
	[Tooltip("How often the mesh will be repeated on the Z axis")]
	public int AmountZ = 1;
	[Tooltip("The distance between the repeated meshes on each axis")]
	public Vector3 Offsets = Vector3.one;

	[Header("Other")]
	[Tooltip("Create a box collider around all the resulting mesh")]
	public bool CreateBoxCollider = false;
	[Tooltip("Stops the mesh from rebuilding")]
	public bool Lock = false;

	[Space]

	[Tooltip("Save mesh as asset into the project folder")]
	public bool SaveMesh = false;



	// On value change
	void OnValidate(){
		if(Lock || Application.isPlaying) return;

		if(!BaseMesh){
			Debug.LogError(transform.name + ": The reference mesh has not been assigned.");
			return;
		}

		// Make it so that the values can't go below 1
		AmountX = Mathf.Max(1, AmountX);
		AmountY = Mathf.Max(1, AmountY);
		AmountZ = Mathf.Max(1, AmountZ);

		// Set up the lists for building our new mesh
		List <Vector3> vertices = new List<Vector3>();
		List <Vector2> uvs = new List<Vector2>();
		List<Vector3> normals = new List<Vector3>();
		List<Vector4> tangents = new List<Vector4>();
		List<Color> colors = new List<Color>();
		List<int>[] triangles = new List<int>[BaseMesh.subMeshCount];
		for(int i = 0; i < triangles.Length; i++) triangles[i] = new List<int>();

		// Index for triangle calculation
		int index = 0;

		// Repeat for x Times on the different axis (Create new instance and calculate the relative transform)
		for(int x = 0; x < AmountX; x++){
			for(int y = 0; y < AmountY; y++){
				for(int z = 0; z < AmountZ; z++){

					// Add vertices
					for(int i = 0; i < BaseMesh.vertices.Length; i++){
						vertices.Add(BaseMesh.vertices[i] + Vector3.Scale(Offsets, new Vector3(x, y, z)));
					}

					// Add uvs
					for(int i = 0; i < BaseMesh.uv.Length; i++){
						uvs.Add(BaseMesh.uv[i]);
					}

					// Add normals
					for(int i = 0; i < BaseMesh.normals.Length; i++){
						normals.Add(BaseMesh.normals[i]);
					}

					// Add tangents
					for(int i = 0; i < BaseMesh.tangents.Length; i++){
						tangents.Add(BaseMesh.tangents[i]);
					}

					// Add colors
					for(int i = 0; i < BaseMesh.colors.Length; i++){
						colors.Add(BaseMesh.colors[i]);
					}

					// Calculate the base tri
					int baseTri = index * BaseMesh.vertices.Length;

					// Go through submeshes
					for(int i = 0; i < BaseMesh.subMeshCount; i++){
						int[] tris = BaseMesh.GetTriangles(i);

						// Add triangles
						for(int j = 0; j < tris.Length; j++){
							triangles[i].Add(tris[j] + baseTri);
						}							
					}

					// Count up index
					index++;
				}
			}
		}

		// Combine and assign the mesh and also try optimize it
		Mesh mesh = new Mesh();
		mesh.name = gameObject.name + "_mesh";
		mesh.subMeshCount = BaseMesh.subMeshCount;

		// add the arrays to the new mesh
		mesh.vertices = vertices.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.normals = normals.ToArray();
		mesh.tangents = tangents.ToArray();
		mesh.colors = colors.ToArray();

		// Add sub mesh tris
		for(int i = 0; i < mesh.subMeshCount; i++){
			mesh.SetTriangles(triangles[i].ToArray(), i);
		}

		// Assign the mesh to the filter
		mesh.Optimize();
		mesh.RecalculateBounds();
		transform.GetComponent<MeshFilter>().mesh = mesh;
	
		// Create collider
		if(CreateBoxCollider){
			// Add a collider if it is missing
			if(!gameObject.GetComponent<BoxCollider>()) gameObject.AddComponent(typeof (BoxCollider));

			// Set the collider to the recalculated bounds
			gameObject.GetComponent<BoxCollider>().center = mesh.bounds.center;
			gameObject.GetComponent<BoxCollider>().size = mesh.bounds.extents * 2;
		}
	}



	// Update
	void Update(){
		// Saves mesh once and then resets
		if(SaveMesh){
			Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

			if(!AssetDatabase.IsValidFolder("Assets/RepeatMesh")) AssetDatabase.CreateFolder("Assets", "RepeatMesh");
			AssetDatabase.CreateAsset(mesh, "Assets/RepeatMesh/" + mesh.name + ".asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			Debug.Log("Created asset: Assets/RepeatMesh/" + mesh.name + ".asset");

			SaveMesh = false;
		}
	}
}
