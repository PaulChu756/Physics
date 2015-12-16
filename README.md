Paul Chu
AIE Year 2 Programming
December 16th, 2015

I.0: Requirements Documentation

I.1: Description of the Problem
Name: Cloth Simulation 
Problem Statement: The physical simulation and will be used to demonstrate advanced physics interactions.
Problem Specification:  The application uses a system of particles that interconnect with spring-dampers to make some type of cloth. In this cloth simulation we apply gravity to each particle, then apply and compute a force to each spring-damper. Then we compute and apply aerodynamic forces to each triangle upon the cloth. After all out of that, we integrate motion by applying a forward euler integration to each particle.

I.2: Input Information
Name: Physics
Description: Input of width and height of grid.
Type: Unity/Visual Studio
Range of Acceptable Values: Most acceptable values are 0 to 10, anything after would take a toll on user’s computer or tablet.

I.3: Output Information
The program will output a cloth simulating a soft body physics.

I.4: User Interface Information
A GUI will display for user after they input information. After, the user can increase/decrease values on via sliders displayed. 
The user can also spawn/reset the cloth simulation, also they can quit the application whenever they please.

II.0: Design Documentation
II.1: System Architecture Description
The program is split into five different scripts for organization. The main script in this project is Spawner.cs, this is the main part of the project, it handles line rendering for springs, spawning/resetting the cloth, pinning the corners for the grid, and exiting.
II.2: Information about the functions
File: Spawner.cs
Class:Spawner
Data Members: MonoBehaviour

Name: particles
Description: A list of class Particle

Name: springs
Description: A list of class Spring

Name: triangles
Description: A list of class AeroForce

Name: particle
Description: A class Particle gameobject

Name: spring
Description: A class Spring gameobject

Name: triangle
Description: A class Aeroforce gameobject

Name: width
Description: a public integer for user to define width of cloth

Name: height
Description: a public integer of user to define height of cloth

Name: vLim
Description: a public float to limit velocity so it doesn’t cause errors.

Name: k
Description: spring force variable to increase/decrease slider value.

Name: b
Description: damping force variable to increase/decrease slider value for spring stiffness.


Name: l
Description: rest length variable to increase/decrease slider value for distance between particles.

Name: air
Description: air variable to increase/decrease slider value to blow onto cloth.

Function: FixedUpdate
Description: Computes and applies force to particle, spring, triangle and does the euler integration. 

Function: Update
Description: Updates all line rendering to each particle 1 to particle 2, and also checks if Esc or P is pressed to exit the program.

Function: ResetCloth
Description: Destroys each particle, spring, and triangle, also the lists for all of them. Reset values on sliders and create new list of particles, spring, and triangles and call Spawn function to create Cloth, along with anchors at corners.

Function: isPinned
Description: Pins all the particles at the corners of the cloth.

Function: Exit
Description: Exit application.

Function: Spawn
Parameters: n/a
Description: It creates all particles in a 2D grid format, while we create the particles, springs connect to the previous particle that is created. So as the particles on the width are created, it connects a spring going to the left. Particles created on the height, are connected by going downward. After this, for diagonal, it goes left and right downward diagonal to connect as the grid is created. As the grid is created into a square like object, we cut those squares into triangles so we can use these triangles for Aerodynamic forces. 

File: Particle
Class: Particle
Data Members: MonoBehaviour


Name: position
Description: Getting and setting the position = transform.position 

Name: velocity
Description: current velocity of the particle

Name: force
Description: the current force applied to the particle

Name: mass
Description: the current mass the particle has.

Name: isPinned
Description: if the particle is pinned or not.

Function: Awake
Description: setting the current particle’s position to the transform.position which is the current gameobject.

Function: ParticleMath
Description: Euler Integration upon particle, does acceleration = f / m; also velocity += (acceleration * deltaTime), position += (velocity * deltaTime);

File: Spring
Class: Spring
Data Members: MonoBehaviour

Name: springConstant
Description: A a constant for how strong the spring is

Name: dampingFactor
Description: A constant for slowing down motion of the spring

Name: RestLength
Description: The distance between two particles
Name: Particle
Description: p1 and p2


Function: makeSpring
Parameters: Particle p1, Particle p2
Description: To connect the new particle that is created to the previous one

Function: ComputeForce
Description: First we get the distance between p2 pos and p1 pos. Then we normalized e (distance between p2 pos and p1), then use the distance formula between them. After getting the length between them we use an equation to get Spring force. After that we get the linear damping force, after this, we turned all the 3D distances & velocities to 1D. We compute spring back to 1D, and then turn that 1D force back into 3D force. After that we get spring damper by just adding spring force and damping force together. Then we find the 1D force and turn it back into 3D, and just add it to p1.force and p2.force.

File: AeroForce
Class: AeroForce
Data Members: MonoBehaviour

Name: Particle
Description: Particles name p1, p2 ,p3

Name: velAir
Description: A force that applies itself on the cloth to make it appear wind or water. 

Name: drag
Description: Air resistance 

Name: Density
Description: The mass per volume of the velAir.

Function: makeTriangle
Description: To connect Particle p1, p2, p3 all together to form a triangle.

Function: AeroMath
Description: First we find the velocity of the triangle. Then we subtract velTriangle by air velocity. Then we find the normal of the triangle, by doing a lot of math. After a lot of math, we use the Aerodyamic force equation which applies a force to each triangle that is on the cloth.



File:SetText
Class:SetText
Data Members: MonoBehaviour

Function: SetLabel
Parameters(Slider slider)
Description: The slider that is passed in, will turn it’s slider value to a string, to display the number it’s on.

File: DragObject
Class:DragObject
Data Members: MonoBehaviour

Name: screenPoint
Description: It grabs the particle’s position from world to screen point

Name: offset
Description: Calculate any difference between the particle world position and the mouse's Screen position converted to a world point.

Function: OnMouseDown
Description: Once the left mouse button is pressed, the user can grab the particle, and move it around.

Function: OnMouseDrag
Description: The user can now drag the particle around the screen while keeping track of it in world space as the user inputs through screen space.


III.0: Implementation Documentation
III.1 Program Code
#File: Spawner.cs
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
        particles[0].isPinned = true; // Bottom Left
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

#File: Particle.cs
class Particle: 
using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour
{
    public Vector3 position // current pos of the particle in space
    {
        get { return transform.position; }
        set { transform.position = value; } // Getting and Setting the position = transform.position
    }
    public Vector3 velocity; // current velocity of the particle
    public Vector3 force;
    public float mass;
    public bool isPinned;

    void Awake()
    {
        position = transform.position;
    }

    public void ParticleMath()
    {
        if (!isPinned)
        {
            Vector3 acceleration = force / mass; // We create acceleration right here, so it doesn't get reset
            velocity += (acceleration * Time.deltaTime);
            position += (velocity * Time.deltaTime);
        }
    }
}

#File: Spring.cs
class Spring: 
using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
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
        // Credit: Matthew Williamson.
        Vector3 e = p2.position - p1.position;
        Vector3 l = e.normalized;
        float distanceBetween = Vector3.Distance(p1.position, p2.position);
        float forceSpring = -springConstant * (RestLength - distanceBetween);
        float v1 = Vector3.Dot(l, p1.velocity);
        float v2 = Vector3.Dot(l, p2.velocity);
        float dampingForce = -dampingFactor * (v1 - v2);
        float springDamper = forceSpring + dampingForce;
        Vector3 force1 = springDamper * l;
        Vector3 force2 = -force1;

        p1.force += force1;
        p2.force += force2;
    }
}

#File: AeroForce.cs
using UnityEngine;
using System.Collections;

public class AeroForce : MonoBehaviour
{
    Particle p1, p2, p3;
    public Vector3 velAir = new Vector3 (0, 0, 0);
    public float drag = 0.5f;
    public float density = 1.0f;

    public void makeTriangle(Particle P1, Particle P2, Particle P3)
    {
        p1 = P1;
        p2 = P2;
        p3 = P3;
    }

    public void AeroMath()
    {
        // Credit: Matthew Willson && Andrew Gotow.

        //Find velocity of Triangle.
        Vector3 velTriangle = (p1.velocity + p2.velocity + p3.velocity) / 3;

        //Find relative velocity, so subtract off velocity of the air.
        velTriangle -= velAir;

        //Finding the normal of the Triangle.
        Vector3 a = p2.position - p1.position;
        Vector3 b = p3.position - p1.position;
        Vector3 crossProduct = Vector3.Cross(a,b);
        Vector3 normalTri = crossProduct / crossProduct.magnitude;
        float totalArea = 0.5f * crossProduct.magnitude;
        float effectiveArea = totalArea * Vector3.Dot(velTriangle, normalTri) / velTriangle.magnitude;

        // Aerodyamic Force equation
        // AeroForce = -0.5 * density * drag * area * |V|^2 * normalTri
        Vector3 aeroForce = -0.5f * drag * density * effectiveArea * velTriangle.sqrMagnitude * normalTri;

        aeroForce /= 3.0f;

        p1.force += aeroForce;
        p2.force += aeroForce;
        p3.force += aeroForce;
    }
}

#File: DragObject.cs
using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private void OnMouseDown()
    {
        //translate the particle position from the world to Screen Point
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //calculate any difference between the particle world position and the mouses Screen position converted to a world point  
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        //keep track of the mouse position
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //convert the screen mouse position to world point and adjust with offset
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
        //update the position of the object in the world
        transform.position = currentPosition;
    }
}

#File: SetText.cs
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    public void SetLabel(Slider slider)
    {
        gameObject.GetComponent<Text>().text = slider.value.ToString();
    }
}

IV.2: Operating Directions
To run this program, navigate through directory and click on build.exe or go to http://paulchu756.github.io/Physics/  (Please make sure you have build data folder with it).


