/// <summary>
/// Extending Unitys Gizmos class by a few shapes.
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEditor;

public static class GizmosPlus {

	/// <summary>
	/// Draws a capsule.
	/// </summary>
	/// <param name="center">Center of the capsule.</param>
	/// <param name="radius">Radius of the capsule.</param>
	/// <param name="height">Height of the capsule.</param>
	/// <param name="rotation">Rotation of the capsule.</param>
	public static void DrawCapsule(Vector3 center, float radius, float height, Quaternion rotation){
		Handles.color = Gizmos.color;

		Vector3 localRight = rotation * Vector3.right;
		Vector3 localUp = rotation * Vector3.up;
		Vector3 localForward = rotation * Vector3.forward;

		Vector3 upper = center + localUp * height * 0.5f;
		Vector3 lower = center - localUp * height * 0.5f;

		// Draw lower
		Handles.DrawWireDisc (lower, localUp, radius);
		Handles.DrawWireArc (lower, localRight, localForward, 180, radius);
		Handles.DrawWireArc (lower, localForward, -localRight, 180, radius);

		// Draw upper
		Handles.DrawWireDisc (upper, localUp, radius);
		Handles.DrawWireArc (upper, localRight, -localForward, 180, radius);
		Handles.DrawWireArc (upper, localForward, localRight, 180, radius);

		// Scale to radius
		localRight *= radius;
		localForward *= radius;

		// Draw side lines
		Gizmos.DrawLine(lower + localRight, upper + localRight);
		Gizmos.DrawLine(lower - localRight, upper - localRight);
		Gizmos.DrawLine(lower + localForward, upper + localForward);
		Gizmos.DrawLine(lower - localForward, upper - localForward);
	}

	/// <summary>
	/// Draws a capsule.
	/// </summary>
	/// <param name="center">Center of the capsule.</param>
	/// <param name="radius">Radius of the capsule.</param>
	/// <param name="height">Height of the capsule.</param>
	public static void DrawCapsule(Vector3 center, float radius, float height){
		DrawCapsule(center, radius, height, Quaternion.identity);
	}



	/// <summary>
	/// Draws a dotted path from multiple input points.
	/// </summary>
	/// <param name="points">Points to draw.</param>
	/// <param name="closedLoop">Should the last and first point be connected?</param>
	/// <param name="dotRadius">Radius of the dots at the points (0 will not render any dots).</param>
	/// <param name="lineWidth">Screenspace width of the dotted lines.</param>
	public static void DrawPath(Vector3[] points, bool closedLoop, float dotRadius, float lineWidth){
		if(points.Length < 2) return;

		Handles.color = Gizmos.color;

		// Draw the path
		for(int i = 0; i < points.Length; i++){
			if(dotRadius > 0) Gizmos.DrawWireSphere(points[i], dotRadius);

			if(i + 1 < points.Length) Handles.DrawDottedLine(points[i], points[i + 1], lineWidth);
		}

		// Connect to the beginning
		if(closedLoop) Handles.DrawDottedLine(points[points.Length - 1], points[0], lineWidth);
	}

	/// <summary>
	/// Draws a dotted path from multiple input points.
	/// </summary>
	/// <param name="points">Points to draw.</param>
	/// <param name="closedLoop">Should the last and first point be connected?</param>
	/// <param name="dotRadius">Radius of the dots at the points (0 will not render any dots).</param>
	public static void DrawPath(Vector3[] points, bool closedLoop, float dotRadius){
		DrawPath(points, closedLoop, dotRadius, 10);
	}

	/// <summary>
	/// Draws a dotted path from multiple input points.
	/// </summary>
	/// <param name="points">Points to draw.</param>
	/// <param name="closedLoop">Should the last and first point be connected?</param>
	public static void DrawPath(Vector3[] points, bool closedLoop){
		DrawPath(points, closedLoop, 0.1f, 10);
	}

	/// <summary>
	/// Draws a dotted path from multiple input points.
	/// </summary>
	/// <param name="points">Points to draw.</param>
	public static void DrawPath(Vector3[] points){
		DrawPath(points, false, 0.1f, 10);
	}

	/// <summary>
	/// Draws a dotted path from multiple input points.
	/// </summary>
	/// <param name="points">Points to draw.</param>
	/// <param name="dotRadius">Radius of the dots at the points (0 will not render any dots).</param>
	/// <param name="lineWidth">Screenspace width of the dotted lines.</param>
	public static void DrawPath(Vector3[] points, float dotRadius, float lineWidth){
		DrawPath(points, false, dotRadius, lineWidth);
	}

	/// <summary>
	/// Draws a dotted path from multiple input points.
	/// </summary>
	/// <param name="points">Points to draw.</param>
	/// <param name="dotRadius">Radius of the dots at the points (0 will not render any dots).</param>
	public static void DrawPath(Vector3[] points, float dotRadius){
		DrawPath(points, false, dotRadius, 10);
	}



	/// <summary>
	/// Draws a box (Similar to Gizmos.DrawCube but with rotation).
	/// </summary>
	/// <param name="center">Center of the box.</param>
	/// <param name="size">Size of the box.</param>
	/// <param name="rotation">Rotation of the box.</param>
	public static void DrawBox (Vector3 center, Vector3 size, Quaternion rotation){
		Vector3 hSize = size * 0.5f;

		// Calculate max and min point
		Vector3 min = center + rotation * -hSize;
		Vector3 max = center + rotation * hSize;

		Vector3 x = rotation * new Vector3(size.x, 0, 0);
		Vector3 y = rotation * new Vector3(0, size.y, 0);
		Vector3 z = rotation * new Vector3(0, 0, size.z);

		// Bottom
		Gizmos.DrawLine(min, min + x);
		Gizmos.DrawLine(min + x, min + x + z);
		Gizmos.DrawLine(min + x + z, min + z);
		Gizmos.DrawLine(min + z, min);

		// Top
		Gizmos.DrawLine(max, max - x);
		Gizmos.DrawLine(max - x, max - x - z);
		Gizmos.DrawLine(max - x - z, max - z);
		Gizmos.DrawLine(max - z, max);

		// Side
		Gizmos.DrawLine(min, min + y);
		Gizmos.DrawLine(min + x, min + x + y);
		Gizmos.DrawLine(min + x + z, min + x + z + y);
		Gizmos.DrawLine(min + z, min + z + y);
	}



	/// <summary>
	/// Draws a dotted box.
	/// </summary>
	/// <param name="center">Center of the box.</param>
	/// <param name="size">Size of the box.</param>
	/// <param name="rotation">Rotation of the box.</param>
	/// <param name="lineWidth">Screenspace width of the dotted lines.</param>
	public static void DrawDottedBox (Vector3 center, Vector3 size, Quaternion rotation, float lineWidth){
		Handles.color = Gizmos.color;

		Vector3 hSize = size * 0.5f;

		// Calculate max and min point
		Vector3 min = center + rotation * -hSize;
		Vector3 max = center + rotation * hSize;

		Vector3 x = rotation * new Vector3(size.x, 0, 0);
		Vector3 y = rotation * new Vector3(0, size.y, 0);
		Vector3 z = rotation * new Vector3(0, 0, size.z);

		// Bottom
		Handles.DrawDottedLine(min, min + x, lineWidth);
		Handles.DrawDottedLine(min + x, min + x + z, lineWidth);
		Handles.DrawDottedLine(min + x + z, min + z, lineWidth);
		Handles.DrawDottedLine(min + z, min, lineWidth);

		// Top
		Handles.DrawDottedLine(max, max - x, lineWidth);
		Handles.DrawDottedLine(max - x, max - x - z, lineWidth);
		Handles.DrawDottedLine(max - x - z, max - z, lineWidth);
		Handles.DrawDottedLine(max - z, max, lineWidth);

		// Side
		Handles.DrawDottedLine(min, min + y, lineWidth);
		Handles.DrawDottedLine(min + x, min + x + y, lineWidth);
		Handles.DrawDottedLine(min + x + z, min + x + z + y, lineWidth);
		Handles.DrawDottedLine(min + z, min + z + y, lineWidth);
	}

	/// <summary>
	/// Draws a dotted box.
	/// </summary>
	/// <param name="center">Center of the box.</param>
	/// <param name="size">Size of the box.</param>
	/// <param name="rotation">Rotation of the box.</param>
	public static void DrawDottedBox (Vector3 center, Vector3 size, Quaternion rotation){
		DrawDottedBox (center, size, rotation, 10);
	}

	/// <summary>
	/// Draws a dotted box.
	/// </summary>
	/// <param name="center">Center of the box.</param>
	/// <param name="size">Size of the box.</param>
	/// <param name="lineWidth">Screenspace width of the dotted lines.</param>
	public static void DrawDottedBox (Vector3 center, Vector3 size, float lineWidth){
		DrawDottedBox (center, size, Quaternion.identity, lineWidth);
	}

	/// <summary>
	/// Draws a dotted box.
	/// </summary>
	/// <param name="center">Center of the box.</param>
	/// <param name="size">Size of the box.</param>
	public static void DrawDottedBox (Vector3 center, Vector3 size){
		DrawDottedBox (center, size, Quaternion.identity, 10);
	}



	/// <summary>
	/// Draws a cross.
	/// </summary>
	/// <param name="center">Center of the cross.</param>
	/// <param name="size">Size of the cross.</param>
	/// <param name="rotation">Rotation of the cross.</param>
	public static void DrawCross (Vector3 center, Vector3 size, Quaternion rotation){
		Vector3 x = rotation * new Vector3(size.x, 0, 0);
		Vector3 y = rotation * new Vector3(0, size.y, 0);
		Vector3 z = rotation * new Vector3(0, 0, size.z);

		Gizmos.DrawLine(center - x * 0.5f, center + x * 0.5f);
		Gizmos.DrawLine(center - y * 0.5f, center + y * 0.5f);
		Gizmos.DrawLine(center - z * 0.5f, center + z * 0.5f);
	}

	/// <summary>
	/// Draws a cross.
	/// </summary>
	/// <param name="center">Center of the cross.</param>
	/// <param name="size">Size of the cross.</param>
	public static void DrawCross (Vector3 center, Vector3 size){
		DrawCross (center, size, Quaternion.identity);
	}

	/// <summary>
	/// Draws a cross.
	/// </summary>
	/// <param name="center">Center of the cross.</param>
	public static void DrawCross (Vector3 center){
		DrawCross (center, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity);
	}



	/// <summary>
	/// Draws a cone.
	/// </summary>
	/// <param name="origin">Origin of the cone.</param>
	/// <param name="direction">Direction and length of the cone.</param>
	/// <param name="radius">Radius at the cones end position.</param>
	public static void DrawCone(Vector3 origin, Vector3 direction, float radius){
		Handles.color = Gizmos.color;

		Quaternion rotation = Quaternion.LookRotation(direction);

		// Draw radius
		Handles.DrawWireDisc (origin + direction, direction.normalized, radius);

		// Normal axis
		Gizmos.DrawRay(origin, direction + rotation * Vector3.up * radius);
		Gizmos.DrawRay(origin, direction + rotation * -Vector3.up * radius);
		Gizmos.DrawRay(origin, direction + rotation * Vector3.right * radius);
		Gizmos.DrawRay(origin, direction + rotation * -Vector3.right * radius);

		// Diagonal axis
		Gizmos.DrawRay(origin, direction + rotation * new Vector3(1, 1, 0).normalized * radius);
		Gizmos.DrawRay(origin, direction + rotation * new Vector3(1, -1, 0).normalized * radius);
		Gizmos.DrawRay(origin, direction + rotation * new Vector3(-1, 1, 0).normalized * radius);
		Gizmos.DrawRay(origin, direction + rotation * new Vector3(-1, -1, 0).normalized * radius);
	}



	/// <summary>
	/// Draws a torus.
	/// </summary>
	/// <param name="center">Center of the torus.</param>
	/// <param name="radius">Radius of the torus.</param>
	/// <param name="thickness">Thickness radius of the torus.</param>
	/// <param name="rotation">Rotation of the torus.</param>
	public static void DrawTorus(Vector3 center, float radius, float thickness, Quaternion rotation){
		Handles.color = Gizmos.color;

		Vector3 localRight = rotation * Vector3.right;
		Vector3 localUp = rotation * Vector3.up;
		Vector3 localForward = rotation * Vector3.forward;

		// Draw radius
		Handles.DrawWireDisc (center, localUp, radius + thickness);
		Handles.DrawWireDisc (center, localUp, radius - thickness);
		Handles.DrawWireDisc (center + localUp * thickness, localUp, radius);
		Handles.DrawWireDisc (center - localUp * thickness, localUp, radius);

		// Draw thickness
		Handles.DrawWireDisc (center + localRight * radius, localForward, thickness);
		Handles.DrawWireDisc (center - localRight * radius, localForward, thickness);
		Handles.DrawWireDisc (center + localForward * radius, localRight, thickness);
		Handles.DrawWireDisc (center - localForward * radius, localRight, thickness);
	}

	/// <summary>
	/// Draws a torus.
	/// </summary>
	/// <param name="center">Center of the torus.</param>
	/// <param name="radius">Radius of the torus.</param>
	/// <param name="thickness">Thickness radius of the torus.</param>
	public static void DrawTorus(Vector3 center, float radius, float thickness){
		DrawTorus(center, radius, thickness, Quaternion.identity);
	}



	/// <summary>
	/// Draws a cylinder.
	/// </summary>
	/// <param name="center">Center of the cylinder.</param>
	/// <param name="radius">Radius of the cylinder.</param>
	/// <param name="height">Height of the cylinder.</param>
	/// <param name="rotation">Rotation of the cylinder.</param>
	public static void DrawCylinder(Vector3 center, float radius, float height, Quaternion rotation){
		Handles.color = Gizmos.color;

		Vector3 localRight = rotation * Vector3.right;
		Vector3 localUp = rotation * Vector3.up;
		Vector3 localForward = rotation * Vector3.forward;

		Vector3 upper = center + localUp * height * 0.5f;
		Vector3 lower = center - localUp * height * 0.5f;

		// Draw lower
		Handles.DrawWireDisc (lower, localUp, radius);

		// Draw upper
		Handles.DrawWireDisc (upper, localUp, radius);

		// Scale to radius
		localRight *= radius;
		localForward *= radius;

		// Draw side lines
		Gizmos.DrawLine(lower + localRight, upper + localRight);
		Gizmos.DrawLine(lower - localRight, upper - localRight);
		Gizmos.DrawLine(lower + localForward, upper + localForward);
		Gizmos.DrawLine(lower - localForward, upper - localForward);
	}

	/// <summary>
	/// Draws a cylinder.
	/// </summary>
	/// <param name="center">Center of the cylinder.</param>
	/// <param name="radius">Radius of the cylinder.</param>
	/// <param name="height">Height of the cylinder.</param>
	public static void DrawCylinder(Vector3 center, float radius, float height){
		DrawCylinder(center, radius, height, Quaternion.identity);
	}



	/// <summary>
	/// Draws an arrow.
	/// </summary>
	/// <param name="origin">Origin of the arrow.</param>
	/// <param name="direction">Direction and length of the arrow.</param>
	public static void DrawArrow(Vector3 origin, Vector3 direction){
		Handles.color = Gizmos.color;

		float radius = direction.magnitude * 0.125f;
		Vector3 arrowRing = origin + direction * 0.75f;
		Vector3 arrowHead = origin + direction;

		Quaternion rotation = Quaternion.LookRotation(direction);

		// Draw line
		Gizmos.DrawLine(origin, arrowHead);

		// Draw radius
		Handles.DrawWireDisc (arrowRing, direction.normalized, radius);

		// Draw arrow  lines
		Gizmos.DrawLine(arrowHead, arrowRing + rotation * Vector3.up * radius);
		Gizmos.DrawLine(arrowHead, arrowRing + rotation * -Vector3.up * radius);
		Gizmos.DrawLine(arrowHead, arrowRing + rotation * Vector3.right * radius);
		Gizmos.DrawLine(arrowHead, arrowRing + rotation * -Vector3.right * radius);
	}

}
