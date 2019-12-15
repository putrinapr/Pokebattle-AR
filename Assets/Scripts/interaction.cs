using UnityEngine;
using System.Collections;

public class interaction : MonoBehaviour
{

    public static Color defaultColor;
    public static Color selectedColor;
    public static Material mat;

    void Start()
    {

        mat = GetComponent<Renderer>().material;

        mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;

        defaultColor = new Color32(255, 255, 255, 255);
        selectedColor = new Color32(255, 0, 0, 255);

        mat.color = defaultColor;
    }

    void touchBegan()
    {
        mat.color = selectedColor;
    }

    void touchEnded()
    {
        mat.color = defaultColor;
    }

    void touchStay()
    {
        mat.color = selectedColor;
    }

    void touchExit()
    {
        mat.color = defaultColor;
    }
}