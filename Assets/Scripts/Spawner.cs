using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<Particle> particles = new List<Particle>();
    public List<Spring> springs = new List<Spring>();

    public Particle particle;
    public Spring spring;

    public int width, height;

    void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Particle newParticle = Instantiate(particle);
                particle.transform.position = new Vector3(x * width, y * height, 0);
                newParticle = particle;
                particles.Add(newParticle);

                if (x > 0) // Make sure this isn't the first particle in the row, if there is, there won't be anything to it's left and we can't connect. Left side
                {
                    //connects to the part we just created with the particle at the index.
                    // particle at the same Y value.
                    // x - 1 we're creating all the particles one by one, but storing.
                    //Spring leftSpring = (newParticle, particles[y * width + (x - 1)]);
                    //leftSpring = Instantiate(spring);
                    //springs.Add(leftSpring);
                    Spring leftSpring = Instantiate(spring);
                    leftSpring.makeSpring(newParticle, particles[y * width + (x - 1)]);
                    leftSpring.transform.position = new Vector3(x * width, y * height, 0);
                    springs.Add(leftSpring);
                }
            }
        }
    }
    //public void ApplyGravity()
    //{
    //    foreach(GameObject o in particles)
    //    {
    //        Vector3 g = new Vector3(0, -9.8f, 0) * GetComponent<Particle>().mass;
    //        GetComponent<Particle>().force += g;
    //    }
    //}
}