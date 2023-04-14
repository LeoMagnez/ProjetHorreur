using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondOrderDynamics
{
    // Syst�me qui r�sout num�riquement des �quations diff�rentielles de mouvement
    // Si �a s'appelle x, c'est l� o� je devrais �tre
    // Si �a s'appelle y, c'est l� o� je suis vraiment
    // On peut faire varier les param�tres f,z,r pour avoir des comportements vari�s

    private Vector3 xp; // Previous x
    private Quaternion qp;
    private Vector3 y, yd; // y est la solution de l'�quation diff�rentielle, et yd sa d�riv�e
    private float k1, k2, k3; // Coefficients de l'�quation diff�rentielle du mouvement 

    // f est la fr�quence naturelle du syst�me en Hertz
    // z est le coefficient de damping --> Vitesse � laquelle le mouvement meurt
    // r est le coefficient de r�ponse initiale 

    public SecondOrderDynamics(float f, float z, float r, Vector3 x0)
    {
        // Calcul des coefficients � partir des constantes de comportement
        k1 = z / (Mathf.PI * f);
        k2 = 1f / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);

        // Initialisation des variables
        xp = x0;
        y = x0;
        yd = Vector3.zero;
    }

    public SecondOrderDynamics()
    {
        SetCoeffs(1, 1, 0);
        xp = Vector3.zero;
        y = Vector3.zero;
        yd = Vector3.zero;
    }

    public void SetCoeffs(float f, float z, float r)
    {
        // Calcul des coefficients � partir des constantes de comportement
        k1 = z / (Mathf.PI * f);
        k2 = 1f / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);
    }
    // Ca s'appelle Pos mais �a p�ut concerner n'importe quel vector3 comme un scale par exemple TODO rename
    public Vector3 GetUpdatedPos(Vector3 targetPos)
    {
        Vector3 xd = (targetPos - xp) / Time.deltaTime;
        xp = targetPos;

        y = y + Time.deltaTime * yd;
        yd = yd + Time.deltaTime * (targetPos + k3 * xd - y - k1 * yd) / k2;
        return y;
    }

    // Offre la possibilit� de changer les coefficients en direct
    public Vector3 GetUpdatedPos(float f, float z, float r, Vector3 targetPos)
    {
        // Calcul des coefficients � partir des constantes de comportement
        k1 = z / (Mathf.PI * f);
        k2 = 1f / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);

        Vector3 xd = (targetPos - xp) / Time.deltaTime;
        xp = targetPos;

        float _k2Stable = Mathf.Max(k2, 1.1f * (Time.deltaTime * Time.deltaTime) / 4 + Time.deltaTime * k1 / 2);

        y = y + Time.deltaTime * yd;
        yd = yd + Time.deltaTime * (targetPos + k3 * xd - y - k1 * yd) / _k2Stable;
        return y;
    }

    /*public Vector3 GetUpdatedRot(float f, float z, float r, Quaternion targetRot)
    {
        // Calcul des coefficients � partir des constantes de comportement
        k1 = z / (Mathf.PI * f);
        k2 = 1f / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);

        Quaternion qd = (targetRot - xp) / Time.deltaTime;
        qp = targetRot;

        float _k2Stable = Mathf.Max(k2, 1.1f * (Time.deltaTime * Time.deltaTime) / 4 + Time.deltaTime * k1 / 2);

        y = y + Time.deltaTime * yd;
        yd = yd + Time.deltaTime * (Quaternion.Pow(targetRot,k3) * qd - y - k1 * yd) / _k2Stable;
        return y;
    }*/

    public Vector3 GetUpdatedRot(float f, float z, float r, Vector3 targetPos)
    {
        // Calcul des coefficients � partir des constantes de comportement
        k1 = z / (Mathf.PI * f);
        k2 = 1f / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);

        Vector3 xd = (targetPos - xp) / Time.deltaTime;
        xp = targetPos;

        float _k2Stable = Mathf.Max(k2, 1.1f * (Time.deltaTime * Time.deltaTime) / 4 + Time.deltaTime * k1 / 2);

        y = y + Time.deltaTime * yd;
        yd = yd + Time.deltaTime * (targetPos + k3 * xd - y - k1 * yd) / _k2Stable;
        return y;
    }



    // T�l�porte l'onjet � l'endroit cibl�, en effa�ant son pass� pour �viter les mouvement ind�sirables
    public void Snap(Vector3 _target)
    {
        xp = _target;
        y = _target;
        yd = Vector3.zero;
    }
}