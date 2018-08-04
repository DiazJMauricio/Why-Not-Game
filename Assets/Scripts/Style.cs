using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Style {

    public const int NUMBER = 4;
    public const string color1              = "#696969";
    public const string color2              = "#2d2d2d";
    public const string vidaFull            = "#43e95a";
    public const string vidaEmpty           = "#a7255c";
    public const string barraEnergiaEmpty   = "#29303b";
    public const string barraEnergiaFull    = "#2cbd96";
    public const string bullet1             = "#9fd25c";
    public const string bullet2             = "#bd512c";
    public const string bullet3             = "#2fdaf3";
    public const string bullet4             = "#612ff3";

    public static Color GetColor(string colorH) {

        Color colorReturn = new Color();
        ColorUtility.TryParseHtmlString(colorH, out colorReturn);
        return colorReturn;
    }
}

