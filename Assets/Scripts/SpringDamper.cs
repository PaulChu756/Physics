using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour 
{
    public float SpringConstant, DampingFactor;
    public Vector3 RestLength;
	Particle p1, p2;
	
	// r = pos
	// v = velocity
	// a = acceleration
	// m = mass
	// p = momentum
	// f = force
	
	// force spring = k_s = spring constant
	// force damp = k_d = damping factor
	// Spring Damper = l_o = rest length
	
	// Force Spring = Spring Constant( Length - Length)
	// Force Damp = Damping Factor = Damping Factor(velocity1 - velocity2);
	// Spring-Damper = by adding Spring Constant & Damping Factor together. = (-Spring Constant( Length - Length)) +  (-Damping Factor(velocity1 - velocity2));

    // spring force = -k * d || k = force but number is scaler. d = displacement, number is vector
	
	public void ComputeForce()
	{
		Vector3 forceSpring = -SpringConstant * (RestLength - RestLength);
		//float dampingForce = -DampingFactor * (p1.velocity - p2.velocity);
		//float springDamper = 
	}
}
