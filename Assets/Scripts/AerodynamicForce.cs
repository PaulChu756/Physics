using UnityEngine;
using System.Collections;

public class AerodynamicForce : MonoBehaviour
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
        //float totalArea = 0.5f * crossProduct.magnitude;
        //float effectiveArea = totalArea * Vector3.Dot(velTriangle, normalTri) / velTriangle.magnitude;

        // Aerodyamic Force equation
        // AeroForce = -0.5 * density * drag * area * |V|^2 * normalTri

        //Vector3 aeroForce = -0.5f * drag * density * effectiveArea * velTriangle.sqrMagnitude * normalTri;
        Vector3 aeroForce = -0.5f * drag * density * ((0.5f * Vector3.Dot(velTriangle, normalTri) * velTriangle.magnitude) / crossProduct.magnitude) * crossProduct;

        aeroForce /= 3.0f;

        p1.force += aeroForce;
        p2.force += aeroForce;
        p3.force += aeroForce;
    }
}