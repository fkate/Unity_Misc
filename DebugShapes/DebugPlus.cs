/// <summary>
/// Extending Unitys Debug class by a few shapes.
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEditor;

public static class DebugPlus {

	/// <summary>
	/// Draws a circle.
	/// </summary>
	/// <param name="center">Center of the circle.</param>
	/// <param name="radius">Radius of the circle.</param>
	/// <param name="normal">Normal pointing up.</param>
	/// <param name="color">Color of the lines.</param>
	/// <param name="duration">How long the lines should be visible for.</param>
	public static void DrawCircle(Vector3 center, float radius, Vector3 normal, Color color, float duration){
		Quaternion rotation = Quaternion.LookRotation(normal.normalized);

		Vector3 x = rotation * new Vector3(radius, 0, 0);
		Vector3 y = rotation * new Vector3(0, radius, 0);

		Vector3[] points = new Vector3[]{
			center + x,
			center + (x + y).normalized * radius,
			center + y,
			center + (y - x).normalized * radius,
			center - x,
			center - (x + y).normalized * radius,
			center - y,
			center - (y - x).normalized * radius
		};

		DrawPath(points, true, color, duration);
	}

	/// <summary>
	/// Draws a circle.
	/// </summary>
	/// <param name="center">Center of the circle.</param>
	/// <param name="radius">Radius of the circle.</param>
	/// <param name="normal">Normal pointing up.</param>
	/// <param name="color">Color of the lines.</param>
	public static void DrawCircle(Vector3 center, float radius, Vector3 normal, Color color){
		DrawCircle(center, radius, normal, color, 0);
	}

	/// <summary>
	/// Draws a circle.
	/// </summary>
	/// <param name="center">Center of the circle.</param>
	/// <param name="radius">Radius of the circle.</param>
	/// <param name="normal">Normal pointing up.</param>
	public static void DrawCircle(Vector3 center, float radius, Vector3 normal){
		DrawCircle(center, radius, normal, Color.white, 0);
	}



	/// <summary>
	/// Draws a sphere.
	/// </summary>
	/// <param name="center">Center of the sphere.</param>
	/// <param name="radius">Radius of the sphere.</param>
	/// <param name="color">Color of the lines.</param>
	/// <param name="duration">How long the lines should be visible for.</param>
	public static void DrawSphere(Vector3 center, float radius, Color color, float duration){
		DrawCircle(center, radius, Vector3.up, color, duration);
		DrawCircle(center, radius, Vector3.right, color, duration);
		DrawCircle(center, radius, Vector3.forward, color, duration);
	}

	/// <summary>
	/// Draws a sphere.
	/// </summary>
	/// <param name="center">Center of the sphere.</param>
	/// <param name="radius">Radius of the sphere.</param>
	/// <param name="color">Color of the lines.</param>
	public static void DrawSphere(Vector3 center, float radius, Color color){
		DrawSphere(center, radius, color, 0);
	}

	/// <summary>
	/// Draws a sphere.
	/// </summary>
	/// <param name="center">Center of the sphere.</param>
	/// <param name="radius">Radius of the sphere.</param>
	public static void DrawSphere(Vector3 center, float radius){
		DrawSphere(center, radius, Color.white, 0);
	}



	/// <summary>
	/// Draws a path from multiple input points.
	/// </summary>
	/// <param name="points">Points to draw.</param>
	/// <param name="closedLoop">Should the last and first point be connected?</param>
	/// <param name="color">Color of the lines.</param>
	/// <param name="duration">How long the lines should be visible for.</param>
	public static void DrawPath(Vector3[] points, bool closedLoop, Color color, float duration){
		if(points.Length < 2) return;

		// Draw the path
		for(int i = 0; i < points.Length; i++){
			if(i + 1 < points.Length) Debug.DrawLine(points[i], points[i + 1], color, duration);
		}

		// Connect to the beginning
		if(closedLoop) Debug.DrawLine(points[points.Length - 1], points[0], color, duration);
	}

	/// <summary>
	/// Draws a path from multiple input points.
	/// </summary>
	/// <param name="points">Points to draw.</param>
	/// <param name="closedLoop">Should the last and first point be connected?</param>
	/// <param name="color">Color of the lines.</param>
	public static void DrawPath(Vector3[] points, bool closedLoop, Color color){
		DrawPath(points, closedLoop, color, 0);
	}

	/// <summary>
	/// Draws a path from multiple input points.
	/// </summary>
	/// <param name="points">Points to draw.</param>
	/// <param name="closedLoop">Should the last and first point be connected?</param>
	public static void DrawPath(Vector3[] points, bool closedLoop){
		DrawPath(points, closedLoop, Color.white, 0);
	}



	/// <summary>
	/// Draws a box.
	/// </summary>
	/// <param name="center">Center of the box.</param>
	/// <param name="size">Size of the box.</param>
	/// <param name="rotation">Rotation of the box.(Base rotation faces forward).</param>
	/// <param name="color">Color of the lines.</param>
	/// <param name="duration">How long the lines should be visible for.</param>
	public static void DrawBox (Vector3 center, Vector3 size, Quaternion rotation, Color color, float duration){
		Vector3 hSize = size * 0.5f;

		// Calculate max and min point
		Vector3 min = center + rotation * -hSize;
		Vector3 max = center + rotation * hSize;

		Vector3 x = rotation * new Vector3(size.x, 0, 0);
		Vector3 y = rotation * new Vector3(0, size.y, 0);
		Vector3 z = rotation * new Vector3(0, 0, size.z);

		// Bottom
		Debug.DrawLine(min, min + x, color, duration);
		Debug.DrawLine(min + x, min + x + z, color, duration);
		Debug.DrawLine(min + x + z, min + z, color, duration);
		Debug.DrawLine(min + z, min, color, duration);

		// Top
		Debug.DrawLine(max, max - x, color, duration);
		Debug.DrawLine(max - x, max - x - z, color, duration);
		Debug.DrawLine(max - x - z, max - z, color, duration);
		Debug.DrawLine(max - z, max, color, duration);

		// Side
		Debug.DrawLine(min, min + y, color, duration);
		Debug.DrawLine(min + x, min + x + y, color, duration);
		Debug.DrawLine(min + x + z, min + x + z + y, color, duration);
		Debug.DrawLine(min + z, min + z + y, color, duration);
	}

	/// <summary>
	/// Draws a box.
	/// </summary>
	/// <param name="center">Center of the box.</param>
	/// <param name="size">Size of the box.</param>
	/// <param name="rotation">Rotation of the box.(Base rotation faces forward).</param>
	/// <param name="color">Color of the lines.</param>
	public static void DrawBox (Vector3 center, Vector3 size, Quaternion rotation, Color color){
		DrawBox(center, size, rotation, color, 0);
	}

	/// <summary>
	/// Draws a box.
	/// </summary>
	/// <param name="center">Center of the box.</param>
	/// <param name="size">Size of the box.</param>
	/// <param name="rotation">Rotation of the box.(Base rotation faces forward).</param>
	public static void DrawBox (Vector3 center, Vector3 size, Quaternion rotation){
		DrawBox(center, size, rotation, Color.white, 0);
	}



	/// <summary>
	/// Draws a cross.
	/// </summary>
	/// <param name="center">Center of the cross.</param>
	/// <param name="size">Size of the cross.</param>
	/// <param name="color">Color of the lines.</param>
	/// <param name="duration">How long the lines should be visible for.</param>
	public static void DrawCross (Vector3 center, Vector3 size, Color color, float duration){
		Vector3 x = new Vector3(size.x, 0, 0);
		Vector3 y = new Vector3(0, size.y, 0);
		Vector3 z = new Vector3(0, 0, size.z);

		Debug.DrawLine(center - x * 0.5f, center + x * 0.5f, color, duration);
		Debug.DrawLine(center - y * 0.5f, center + y * 0.5f, color, duration);
		Debug.DrawLine(center - z * 0.5f, center + z * 0.5f, color, duration);
	}

	/// <summary>
	/// Draws a cross.
	/// </summary>
	/// <param name="center">Center of the cross.</param>
	/// <param name="size">Size of the cross.</param>
	/// <param name="color">Color of the lines.</param>
	public static void DrawCross (Vector3 center, Vector3 size, Color color){
		DrawCross(center, size, color, 0);
	}

	/// <summary>
	/// Draws a cross.
	/// </summary>
	/// <param name="center">Center of the cross.</param>
	/// <param name="size">Size of the cross.</param>
	public static void DrawCross (Vector3 center, Vector3 size){
		DrawCross(center, size, Color.white, 0);
	}



	/// <summary>
	/// Draws a cone along a direction.
	/// </summary>
	/// <param name="origin">Origin of the cone.</param>
	/// <param name="direction">Direction and length of the cone.</param>
	/// <param name="radius">Radius at the cones end position.</param>
	/// <param name="color">Color of the lines.</param>
	/// <param name="duration">How long the lines should be visible for.</param>
	public static void DrawCone(Vector3 origin, Vector3 direction, float radius, Color color, float duration){
		Quaternion rotation = Quaternion.LookRotation(direction);
		Vector3 target = origin + direction;

		// Draw radius
		DrawCircle (target, radius, direction, color, duration);

		// Normal axis
		Debug.DrawRay(origin, direction + rotation * Vector3.up * radius, color, duration);
		Debug.DrawRay(origin, direction + rotation * -Vector3.up * radius, color, duration);
		Debug.DrawRay(origin, direction + rotation * Vector3.right * radius, color, duration);
		Debug.DrawRay(origin, direction + rotation * -Vector3.right * radius, color, duration);

		// Diagonal axis
		Debug.DrawRay(origin, direction + rotation * new Vector3(1, 1, 0).normalized * radius, color, duration);
		Debug.DrawRay(origin, direction + rotation * new Vector3(1, -1, 0).normalized * radius, color, duration);
		Debug.DrawRay(origin, direction + rotation * new Vector3(-1, 1, 0).normalized * radius, color, duration);
		Debug.DrawRay(origin, direction + rotation * new Vector3(-1, -1, 0).normalized * radius, color, duration);
	}

	/// <summary>
	/// Draws a cone along a direction.
	/// </summary>
	/// <param name="origin">Origin of the cone.</param>
	/// <param name="direction">Direction and length of the cone.</param>
	/// <param name="radius">Radius at the cones end position.</param>
	/// <param name="color">Color of the lines.</param>
	public static void DrawCone(Vector3 origin, Vector3 direction, float radius, Color color){
		DrawCone(origin, direction, radius, color, 0);
	}

	/// <summary>
	/// Draws a cone along a direction.
	/// </summary>
	/// <param name="origin">Origin of the cone.</param>
	/// <param name="direction">Direction and length of the cone.</param>
	/// <param name="radius">Radius at the cones end position.</param>
	public static void DrawCone(Vector3 origin, Vector3 direction, float radius){
		DrawCone(origin, direction, radius, Color.white, 0);
	}



	/// <summary>
	/// Draws a cylinder along a direction.
	/// </summary>
	/// <param name="origin">Origin of the cylinder.</param>
	/// <param name="direction">Direction of the cylinder.</param>
	/// <param name="radius">Radius of the cylinder.</param>
	/// <param name="color">Color of the lines.</param>
	/// <param name="duration">How long the lines should be visible for.</param>
	public static void DrawCylinder(Vector3 origin, Vector3 direction, float radius, Color color, float duration){
		Quaternion rotation = Quaternion.LookRotation(direction);
		Vector3 target = origin + direction;

		// Draw radius
		DrawCircle (origin, radius, direction, color, duration);
		DrawCircle (target, radius, direction, color, duration);

		Vector3 localUp = rotation * Vector3.up * radius;
		Vector3 localRight = rotation * Vector3.right * radius;

		// Normal axis
		Debug.DrawRay(origin + localUp, direction, color, duration);
		Debug.DrawRay(origin - localUp, direction, color, duration);
		Debug.DrawRay(origin + localRight, direction, color, duration);
		Debug.DrawRay(origin - localRight, direction, color, duration);
	}

	/// <summary>
	/// Draws a cylinder along a direction.
	/// </summary>
	/// <param name="origin">Origin of the cylinder.</param>
	/// <param name="direction">Direction of the cylinder.</param>
	/// <param name="radius">Radius of the cylinder.</param>
	/// <param name="color">Color of the lines.</param>
	public static void DrawCylinder(Vector3 origin, Vector3 direction, float radius, Color color){
		DrawCylinder(origin, direction, radius, color, 0);
	}

	/// <summary>
	/// Draws a cylinder along a direction.
	/// </summary>
	/// <param name="origin">Origin of the cylinder.</param>
	/// <param name="direction">Direction of the cylinder.</param>
	/// <param name="radius">Radius of the cylinder.</param>
	public static void DrawCylinder(Vector3 origin, Vector3 direction, float radius){
		DrawCylinder(origin, direction, radius, Color.white, 0);
	}



	/// <summary>
	/// Draws an arrow.
	/// </summary>
	/// <param name="origin">Origin of the arrow.</param>
	/// <param name="direction">Direction and length of the arrow.</param>
	/// <param name="color">Color of the lines.</param>
	/// <param name="duration">How long the lines should be visible for.</param>
	public static void DrawArrow(Vector3 origin, Vector3 direction, Color color, float duration){
		float dist = direction.magnitude * 0.0625f;
		Vector3 arrowRing = origin + direction * 0.75f;
		Vector3 arrowHead = origin + direction;

		Quaternion rotation = Quaternion.LookRotation(direction);

		// Draw line
		Debug.DrawLine(origin, arrowHead, color, duration);

		// Draw arrow ring
		Vector3 x = rotation * new Vector3(dist, 0, 0);
		Vector3 y = rotation * new Vector3(0, dist, 0);

		Vector3[] points = new Vector3[]{
			arrowRing + (x + y),
			arrowRing + (y - x),
			arrowRing - (x + y),
			arrowRing - (y - x)
		};

		DrawPath(points, true, color, duration);

		// Draw arrow  lines
		Debug.DrawLine(arrowHead, points[0], color, duration);
		Debug.DrawLine(arrowHead, points[1], color, duration);
		Debug.DrawLine(arrowHead, points[2], color, duration);
		Debug.DrawLine(arrowHead, points[3], color, duration);
	}

	/// <summary>
	/// Draws an arrow.
	/// </summary>
	/// <param name="origin">Origin of the arrow.</param>
	/// <param name="direction">Direction and length of the arrow.</param>
	/// <param name="color">Color of the lines.</param>
	public static void DrawArrow(Vector3 origin, Vector3 direction, Color color){
		DrawArrow(origin, direction, color, 0);
	}

	/// <summary>
	/// Draws an arrow.
	/// </summary>
	/// <param name="origin">Origin of the arrow.</param>
	/// <param name="direction">Direction and length of the arrow.</param>
	public static void DrawArrow(Vector3 origin, Vector3 direction){
		DrawArrow(origin, direction, Color.white, 0);
	}

}
