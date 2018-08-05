using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Style {

    public enum StyleColor {
        Color1,
        Color2,
        Color3,
        Color4,
        Color5,
        Color6,
        vidaFull,
        VidaEmpty,
        BarraEnergiaEmpty,
        BarraEnergia,
        Bullet1,
        Bullet2,
        bullet3,
        bullet4 };


    public const string color1              = "#696969";
    public const string color2              = "#2d2d2d";
    public const string color3              = "#2d2d2d";
    public const string color4              = "#2d2d2d";
    public const string color5              = "#2d2d2d";
    public const string color6              = "#2d2d2d";
    public const string vidaFull            = "#3dcb6c";
    public const string vidaEmpty           = "#003f1f";
    public const string barraEnergiaEmpty   = "#222A68";
    public const string barraEnergiaFull    = "#6254FF";
    public const string bullet1             = "#3de900";//  7FFF52
    public const string bullet2             = "#FF6F89";
    public const string bullet3             = "#28C4FF";
    public const string bullet4             = "#612ff3";

    public static Color GetColor(string colorH) {

        Color colorReturn = new Color();
        ColorUtility.TryParseHtmlString(colorH, out colorReturn);
        return colorReturn;
    }
    public static Color GetColor(StyleColor colorH)
    {
        Color colorReturn = new Color();
        string hexa = "";

        switch (colorH)
        {
            case StyleColor.Color1:             hexa = color1;              break;
            case StyleColor.Color2:             hexa = color2;              break;
            case StyleColor.Color3:             hexa = color3;              break;
            case StyleColor.Color4:             hexa = color4;              break;
            case StyleColor.Color5:             hexa = color5;              break;
            case StyleColor.Color6:             hexa = color6;              break;
            case StyleColor.vidaFull:           hexa = vidaFull;            break;
            case StyleColor.VidaEmpty:          hexa = vidaEmpty;           break;
            case StyleColor.BarraEnergiaEmpty:  hexa = barraEnergiaEmpty;   break;
            case StyleColor.BarraEnergia:       hexa = barraEnergiaFull;    break;
            case StyleColor.Bullet1:            hexa = bullet1;             break;
            case StyleColor.Bullet2:            hexa = bullet2;             break;
            case StyleColor.bullet3:            hexa = bullet3;             break;
            case StyleColor.bullet4:            hexa = bullet4;             break;
            default: Debug.LogErrorFormat("StyleColor no Encontrado en 'Style.GetColor(StyleColor colorh);'."); break;
        }
        
        ColorUtility.TryParseHtmlString(hexa, out colorReturn);
        return colorReturn;
    }
}

