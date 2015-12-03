using UnityEngine;
using System.Collections;

/* Unity has build in normailized and Mag. */

public class Particle : MonoBehaviour 
{
	public Vector3 position;
	public Vector3 velocity;
    public Vector3 nextPosition;
    public Vector3 nextVelocity;
    public Vector3 acceleration;
    public Vector3 forceGravity;
    public Vector3 force;
	public float mass;
	
	// r = pos
	// v = velocity
	// a = acceleration
	// m = mass
	// p = momentum
	// f = force
	// l = length;
	
	// Didn't do Uniform Gravity yet, using rigidbody, but thinking about it.
	
	public void Acceleration()
	{
        acceleration = (1 / mass) * force;
	}
	
	public void Force () // F = ma;
    {
		force = mass * acceleration;
	}
	
	// Euler Integration for Velocity
	public void Velocity()
	{
        nextVelocity = velocity + (acceleration * Time.deltaTime);
	}
	
	// Euler Integration for Position
	public void Position()
	{
        nextPosition = position + (nextVelocity * Time.deltaTime);
        transform.position = nextPosition;
	}

    public void Gravity()
    {
        Vector3 g = new Vector3(0, -9.8f, 0);
        forceGravity = mass * g;
    }
}
