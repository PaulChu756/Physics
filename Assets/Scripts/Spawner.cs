using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public List<Particle> particles = new List<Particle>();
    public List<Spring> springs = new List<Spring>();
    public List<AeroForce> triangles = new List<AeroForce>();
    public Particle particle;
    public Spring spring;
    public AeroForce triangle;
    public int width, height;
    public float vLim;
    public Slider k, b, l, air, width_, height_;
    public Button exit, spawn;
    GameObject particles_;
    GameObject springs_;
    GameObject triangle_;

    void FixedUpdate()
    {
        foreach (Particle p in particles)
        {
            p.force = new Vector3(0, -9.8f, 0) * p.mass;
            if(p.velocity.magnitude > vLim)
            {
               p.velocity = p.velocity.normalized * vLim;
            }
        }

        foreach (Spring s in springs)
        {
            if (s != null)
            {
                s.springConstant = k.value;
                s.dampingFactor = b.value;
                s.RestLength = l.value;
                s.GetComponent<Spring>().ComputeForce();
            }
        }

        foreach (AeroForce t in triangles)
        {
            if(t != null)
            {
                t.velAir.z = air.value;
                t.GetComponent<AeroForce>().AeroMath();
            }
        }

        foreach (Particle p in particles) // Particle Euler Intergration
        {
            if(p != null)
            {
                p.GetComponent<Particle>().ParticleMath();
            }
        }
    }

    // Loops through spring list to draw lines to each particle
    void Update()
    {
        int i = 0;
        foreach (Spring s in springs)
        {
            if (s != null)
            {
                // Credit: Matthew Williamson
                LineRenderer l = s.GetComponent<LineRenderer>();
                l.SetPosition(0, springs[i].p1.transform.position);
                l.SetPosition(1, springs[i].p2.transform.position);
                i++;
            }
        }

        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P))
        {
            Exit();
        }
    }

    public void ResetCloth()
    {
        foreach(Particle p in particles)
        {
            Destroy(p.gameObject);
        }

        foreach (Spring s in springs)
        {
            Destroy(s.gameObject);
        }

        foreach(AeroForce t in triangles)
        {
            Destroy(t.gameObject);
        }

        Destroy(particles_);
        Destroy(springs_);
        Destroy(triangle_);

        k.value = 1;
        b.value = 1;
        l.value = 1;
        air.value = 0.1f;

        particles = new List<Particle>();
        springs = new List<Spring>();
        triangles = new List<AeroForce>();

        width = (int)width_.value;
        height = (int)height_.value;
        Spawn();
        isPinned();
    }

    public void isPinned()
    {
        // If Width and Height are the same numbers
        particles[width - 1].isPinned = true; // Bottom right
        particles[height * width - 1].isPinned = true; // Top right
        particles[height * width - width].isPinned = true; // Top left




        particles[0].isPinned = true;// Bottom Left

        //
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Spawn()
    {
        particles_ = new GameObject();
        springs_ = new GameObject();
        triangle_ = new GameObject();

        particles_.name = "Particles";
        particles_.transform.SetParent(transform);

        springs_.name = "Springs";
        springs_.transform.SetParent(transform);

        triangle_.name = "Triangles";
        triangle_.transform.SetParent(transform);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Particle newParticle = Instantiate(particle);
                newParticle.transform.position = new Vector3(x * width, y * height, 0);
                newParticle.name = x.ToString() + " " + y.ToString(); // Set all particle clone names into numbers
                newParticle.transform.SetParent(particles_.transform);
                particles.Add(newParticle);

                /*
                
                6 <-- 7 <-- 8
                | \   |   / |
                3 <-- 4 <-- 5
                |  \  |   / |
                0 <-- 1 <-- 2

                Left Springs = Springs are created and link to the previous one made
                1 to 0
                2 to 1

                Above Springs = Springs are connected to the bottom one
                3 to 0
                4 to 1
                5 to 2

                DownLeftDiagonal = Springs connected from top to bottom left
                4 to 0
                5 to 1
                7 to 3
                8 to 4

                DownRightDiagonal = Springs connected from top to bottom right
                3 to 1
                4 to 2
                6 to 4
                7 to 5
                */

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
                    sideSpring.name = "SideSpring: " + x.ToString() + " " + y.ToString();
                    sideSpring.makeSpring(newParticle, particles[y * width + (x - 1)]);
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
                    aboveSpring.name = "AboveSpring: " + x.ToString() + " " + y.ToString();
                    aboveSpring.makeSpring(newParticle, particles[(y - 1) * width + x]);
                    springs.Add(aboveSpring);
                    aboveSpring.transform.SetParent(springs_.transform);
                }

                if (x > 0 && y > 0) // DownLeftDiagonal spring y-1, x-1
                {
                    Spring downLeftSpring = Instantiate(spring);
                    LineRenderer l = downLeftSpring.GetComponent<LineRenderer>();
                    l.SetPosition(0, newParticle.transform.position);
                    l.SetPosition(1, particles[(y - 1) * width + (x - 1)].transform.position);
                    downLeftSpring.name = "UpLeftSpring: " + x.ToString() + " " + y.ToString();
                    downLeftSpring.makeSpring(newParticle, particles[(y - 1) * width + (x - 1)]);
                    springs.Add(downLeftSpring);
                    downLeftSpring.transform.SetParent(springs_.transform);
                }

                if (x < width - 1 && y > 0) // DownRightDiagonal, spring y-1, x+1
                {
                    Spring downRightSpring = Instantiate(spring);
                    LineRenderer l = downRightSpring.GetComponent<LineRenderer>();
                    l.SetPosition(0, newParticle.transform.position);
                    l.SetPosition(1, particles[(y - 1) * width + (x + 1)].transform.position);
                    downRightSpring.name = "DownRightSpring: " + x.ToString() + " " + y.ToString();
                    downRightSpring.makeSpring(newParticle, particles[(y - 1) * width + (x + 1)]);
                    springs.Add(downRightSpring);
                    downRightSpring.transform.SetParent(springs_.transform);
                }
            }
        }

        // Credit: Matthew Williamson
        for(int x = 0; x < particles.Count; x++)
        {
            if (x + 1 < width * height && x + width < width * height && x + width + 1 < width * height)
            {
                AeroForce firstTri = Instantiate(triangle);
                firstTri.name = "FirstTriangle: " + width.ToString() + " " + height.ToString();
                firstTri.makeTriangle(particles[x], particles[x + 1], particles[x + width]);
                triangles.Add(firstTri);
                firstTri.transform.SetParent(triangle_.transform);

                AeroForce secondTri = Instantiate(triangle);
                secondTri.name = "SecondTriangle: " + width.ToString() + " " + height.ToString();
                secondTri.makeTriangle(particles[x], particles[x + 1], particles[x + width + 1]);
                triangles.Add(secondTri);
                secondTri.transform.SetParent(triangle_.transform);

                AeroForce thirdTri = Instantiate(triangle);
                thirdTri.name = "ThirdTriangle: " + width.ToString() + " " + height.ToString();
                thirdTri.makeTriangle(particles[x + 1], particles[x + width], particles[x + width + 1]);
                triangles.Add(thirdTri);
                thirdTri.transform.SetParent(triangle_.transform);

                AeroForce fourthTri = Instantiate(triangle);
                fourthTri.name = "FourthTriangle: " + width.ToString() + " " + height.ToString();
                fourthTri.makeTriangle(particles[x], particles[x + width], particles[x + width + 1]);
                triangles.Add(fourthTri);
                fourthTri.transform.SetParent(triangle_.transform);
            }
        }
    }
}