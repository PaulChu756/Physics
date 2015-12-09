using UnityEngine;
using System.Collections;

public class AerodynamicForce : MonoBehaviour
{

}

//Vector3 F_aero;
//Vector3 p;
//Vector3 c;
//Vector3 a;
//Vector3 e;

// Aerodynamic drag force
// p = density of air or water
// c = coefficient of drag for the object
// a = cross sectional area of the object
// e = unity vector in the opposite direction of the velocity // Changed to n for some reason.
// What the hell is V... Velocity?!?!?

// Using triangles for surface area
// find velocity, normal, and area of triangle.