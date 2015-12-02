using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour 
{
	public float SpringConstant, DampingFactor, RestLength;
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
	// Force Damp = Damping Factor = Damping Factor(
	
	public void ComputeForce()
	{
		float forceSpring = SpringConstant * (RestLength - RestLength);
		//float dampingForce = DampingFactor * (p1.velocity - p2.velocity);
		//float springDamper = 
	}
}
