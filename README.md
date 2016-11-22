A collection of small Unity scripts and shaders.

Currently included:

# Debug Shapes
![My image](https://github.com/TPen/Unity_Misc/blob/master/DebugShapes.png)

1x Gizmo and 1x Debug class to extend on Unitys limited amount of different base shapes for debugging purposes.
The DebugPlus methods can be called from everywhere during runtime while the Gizmo ones should be called from inside "OnDrawGizmos".

The included methods are: (-d debug -g gizmo) 
- DrawCircle [-d] Draws a simple circle
- DrawSphere [-d] Draws a sphere
- DrawPath [-d -g] Draw a path between provided points
- DrawBox [-d -g] Draws a box (rotatable)
- DrawCross [-d -g] Draws a cross
- DrawCone [-d -g] Draw a cone from a starting point towards a direction
- DrawCylinder [-d -g] Draws a cylinder (The debug one has a origin and a direction)
- DrawArrow [-d -g] Draw a small arrow in the set direction
- DrawCapsule [-g] Draws a capsule
- DrawDottedBox [-g] Draws a box with a dotted outline
- DrawTorus [-g] Draws a torus

(Quick note: some of the provided options differ between the two classes. Look at the method summarys for more details)

# RepeatMesh.cs
![My image](https://github.com/TPen/Unity_Misc/blob/master/RepeatMesh.png)

A script for repeating a referenced mesh on the x y and z axis.
Makes a single mesh out of all the repeated ones.
Can automaticly add a box collider and Also allows it to save the generated mesh by hitting the "SaveMesh" boolean once.

A bit slower then CombineMeshes but also copies submeshes so multi material support will still work after.
When finished either activate the "Lock" boolean or remove the script to stop the script from recreating the mesh.

# ShaderHelperFunctions.cginc

Small shader file to provide quick shortcuts for creating different kinds of gradients from the uvs.


# UI_Extra.unitypackage
![My image](https://github.com/TPen/Unity_Misc/blob/master/UI_Extra.png)

A package for providing prefabs and assets of additional UI elements compatible with the Unity 5 UI canvas.
Includes:
- Self stretching bar (healthbar or similar)
- Radial bar
- Textbox with slowly appearing letters
- A color wheel (This one is currently read only: can get the selected color / can't automaticly switch to a provided color atm)
- A few test textures and shaders to procedualy generate gradients instead of using textures
