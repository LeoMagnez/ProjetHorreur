using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondOrderDynamics
{
    // Système qui résout numériquement des équations différentielles de mouvement
    // Si ça s'appelle x, c'est là où je devrais être
    // Si ça s'appelle y, c'est là où je suis vraiment
    // On peut faire varier les paramètres f,z,r pour avoir des comportements variés

    private Vector3 xp; // Previous x
    private Quaternion qp;
    private Vector3 y, yd; // y est la solution de l'équation différentielle, et yd sa dérivée
    private float k1, k2, k3; // Coefficients de l'équation différentielle du mouvement 

    // f est la fréquence naturelle du système en Hertz
    // z est le coefficient de damping --> Vitesse à laquelle le mouvement meurt
    // r est le coefficient de réponse initiale 

    public SecondOrderDynamics(float f, float z, float r, Vector3 x0)
    {
        // Calcul des coefficients à partir des constantes de comportement
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
        // Calcul des coefficients à partir des constantes de comportement
        k1 = z / (Mathf.PI * f);
        k2 = 1f / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);
    }
    // Ca s'appelle Pos mais ça pêut concerner n'importe quel vector3 comme un scale par exemple TODO rename
    public Vector3 GetUpdatedPos(Vector3 targetPos)
    {
        Vector3 xd = (targetPos - xp) / Time.deltaTime;
        xp = targetPos;

        y = y + Time.deltaTime * yd;
        yd = yd + Time.deltaTime * (targetPos + k3 * xd - y - k1 * yd) / k2;
        return y;
    }

    // Offre la possibilité de changer les coefficients en direct
    public Vector3 GetUpdatedPos(float f, float z, float r, Vector3 targetPos)
    {
        // Calcul des coefficients à partir des constantes de comportement
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
        // Calcul des coefficients à partir des constantes de comportement
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
        // Calcul des coefficients à partir des constantes de comportement
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



    // Téléporte l'onjet à l'endroit ciblé, en effaçant son passé pour éviter les mouvement indésirables
    public void Snap(Vector3 _target)
    {
        xp = _target;
        y = _target;
        yd = Vector3.zero;
    }
}