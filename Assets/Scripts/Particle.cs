using UnityEngine;
using System.Collections;

/* Unity has build in normailized and Mag. */

public class Particle : MonoBehaviour 
{
	public Vector3 position;
	public Vector3 velocity;
	public Vector3 force;
	public Vector3 acceleration;
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
	
	public void Force ()
	{
		// F = ma;
		force = mass * acceleration;
	}
	
	// Euler Integration for Velocity
	public void Velocity()
	{
		velocity = velocity + (acceleration * Time.deltaTime);
	}
	
	// Euler Integration for Position
	public void Position()
	{
		position = position + (velocity * Time.deltaTime);
	}
}
