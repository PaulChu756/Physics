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

    // Loops through spring list 
    void Update()
    {
        int i = 0;
        foreach (Spring s in springs)
        {
            LineRenderer l = s.GetComponent<LineRenderer>();
            l.SetPosition(0, springs[i].p1.transform.position);
            l.SetPosition(1, springs[i].p2.transform.position);
            i++;
        }
    }

    public void Spawn()
    {
        GameObject particles_ = new GameObject();
        particles_.name = "Particles";
        particles_.transform.SetParent(transform);
        GameObject springs_ = new GameObject();
        springs_.name = "Springs";
        springs_.transform.SetParent(transform);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //GameObject newParticleGO = Instantiate(particleGO);
                //Particle newParticle = newParticleGO.GetComponent<Particle>();
                Particle newParticle = Instantiate(particle);

                newParticle.transform.position = new Vector3(x * width, y * height, 0);
                newParticle.name = (x * width).ToString() + " " + (y * height).ToString();
                newParticle.transform.SetParent(particles_.transform);
                particles.Add(newParticle);

                if (x > 0) // Left spring
                {
                    // Make sure this isn't the first particle in the row, if there is, there won't be anything to it's left and we can't connect.
                    //connects to the part we just created with the particle at the index.
                    // particle at the same Y value.
                    // x - 1 we're creating all the particles one by one, but storing.
                    Spring sideSpring = Instantiate(spring);
                    LineRenderer l = sideSpring.GetComponent<LineRenderer>();
                    l.SetPosition(0, newParticle.transform.position);
                    l.SetPosition(1, particles[y * width + (x - 1)].transform.position);
                    sideSpring.name = (x - .5f).ToString() + " " + (y * height).ToString();
                    sideSpring.makeSpring(newParticle, particles[y * width + (x - 1)]);
                    sideSpring.transform.position = new Vector3((x - .5f) * width, y * height, 0);
                    sideSpring.transform.parent = gameObject.transform;
                    springs.Add(sideSpring);
                    sideSpring.transform.SetParent(springs_.transform);
                }

                if (y > 0) // Up Spring
                {
                    //// Connecting ontop, but going downwards.
                    Spring aboveSpring = Instantiate(spring);
                    LineRenderer l = aboveSpring.GetComponent<LineRenderer>();
                    l.SetPosition(0, newParticle.transform.position);
                    l.SetPosition(1, particles[(y - 1) * width + x].transform.position);
                    aboveSpring.makeSpring(newParticle, particles[(y - 1) * width + x]);
                    aboveSpring.transform.position = new Vector3(x * width, (y - .5f) * height, 0);
                    aboveSpring.transform.parent = gameObject.transform;
                    springs.Add(aboveSpring);
                    aboveSpring.transform.SetParent(springs_.transform);
                }

                //if (x > 0 && y > 0)
                //{
                //    Spring upLeftSpring = Instantiate(leftDiagonalSpring);
                //    upLeftSpring.makeSpring(newParticle, particles[(y - 1) * width + (x - 1)]);
                //    upLeftSpring.transform.position = new Vector3(x * width, (y - .5f) * height, 0);
                //    upLeftSpring.transform.parent = gameObject.transform;
                //    springs.Add(upLeftSpring);
                //}

                //foreach(Particle p in particles)
                //{
                //    p.GetComponent<Spring>().Simulate();
                //}

                //foreach(Spring s in springs)
                //{
                //    s.GetComponent<Spring>().Simulate();
                //}
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