using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OptionData
{
    // static變數 選項變數
    private static float volume = 1;

    private static float m_sensitivityX = 2f;
    private static float m_sensitivityY = 2f;

    public static float VOLUME
    {
        get { return volume; }
        set { volume = value; }
    }
    public static float M_SensitivityX
    {
        get { return m_sensitivityX; }
        set { m_sensitivityX = value; }
    }
    public static float M_SensitivityY
    {
        get { return m_sensitivityY; }
        set { m_sensitivityY = value; }
    }
}
