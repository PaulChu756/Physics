using UnityEngine;
using System.Collections;

public class AerodynamicForce : MonoBehaviour
{
    Particle p1, p2, p3;
    public Vector3 velAir = new Vector3 (0, 0, 1);

    public void velSurface()
    {
        Vector3 velSur = (p1.velocity + p2.velocity + p3.velocity) / 3;
        Vector3 velRelative = velSur - velAir;
    }

    public Vector3 normalTriangle()
    {
        Vector3 side1 = p2.position - p1.position;
        Vector3 side2 = p3.position - p1.position;
        return Vector3.Cross(side1, side2).normalized;
    }

}

// Aerodynamic drag force
// p = density of air or water
// c = coefficient of drag for the object
// a = cross sectional area of the object
// n = normal of the surface of triangle
// v = velocity;s
// Using triangles for surface area
// find velocity, normal, and area of triangle.
// aeroForce = (-1/2) * p * v.Abs.Value ^2 * c * a * n 