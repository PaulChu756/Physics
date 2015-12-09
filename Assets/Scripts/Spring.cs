using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
    /// <summary>
    /// springConstant, a constant for how "strong" the spring is.
    /// dampingConstant, a constant for slowing down motion of the spring.
    /// length between two particles
    /// Hook's Law = spring force = -k * d || k = force but number is scaler. d = displacement, number is vector
    /// </summary>
    public float springConstant;
    public float dampingFactor;
    public float RestLength;
    public Particle p1, p2;
    
    public void makeSpring(Particle P1, Particle P2)
    {
        p1 = P1;
        p2 = P2;
    }

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

