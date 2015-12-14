# Physics
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

Name: partricles
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
Parameters: 


