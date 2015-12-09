using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
    public float springConstant; // springConstant, a constant for how "strong" the spring is.
    public float dampingFactor; // dampingConstant, a constant for slowing down motion of the spring.
    public float RestLength; // length between two particles
    public Particle p1, p2;
    
    public void makeSpring(Particle P1, Particle P2)
    {
        p1 = P1;
        p2 = P2;
    }

    // Spring constant = Ks
    // Damping Factor = Kd
    // Rest Length = l
    // Hook's Law = spring force = -k * d || k = force but number is scaler. d = displacement, number is vector

    public void ComputeForce()
    {
        // Spring Force
        // Get the distance between two particles
        // So first we use  p2 Pos - p1 pos
        // Then we normalized e
        // Then use Vector.Distance to find out the distance between particles, in order to get the length
        // After getting the length, use in equation to get Spring Force.
        Vector3 e = p2.position - p1.position;
        Vector3 l = e.normalized;
        float distanceBetween = Vector3.Distance(p1.position, p2.position);
        float forceSpring = -springConstant * (RestLength - distanceBetween);
  
        // Linear Damping Force
        // Kd = -dampingFactor (v1 - v2)
        // Turn 3D distances & velocities to 1D
        // Compute spring force in 1D
        // turn 1D force back into 3D force
        float v1 = Vector3.Dot(l, p1.velocity);
        float v2 = Vector3.Dot(l, p2.velocity);
        float dampingForce = -dampingFactor * (v1 - v2);

        // Spring Damper by just adding the two together
        // Find 1D force and map it back to 3D
        float springDamper = forceSpring + dampingForce; //-50 + 0
        Vector3 force1 = springDamper * l; //<0,0,0> * -50 = 0
        Vector3 force2 = -force1;

        p1.force += force1; 
        p2.force += force2;
    }
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

