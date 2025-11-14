using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanVas : MonoBehaviour
{
    [SerializeField] private Text m_text;

    private static DebugCanVas ms_instance;

    private List<string> m_printTexts = new List<string>();

    private void Awake()
    {
        ms_instance = this;
    }

    public static void Print(string format,params object[] args)
    {
        if (ms_instance == null) return;
        ms_instance.m_printTexts.Add(string.Format(format, args));
    }

    private void LateUpdate()
    {
        string text = "";
        foreach(string str in m_printTexts)
        {
            text += str + "\n";
        }
        m_text.text = text;

        m_printTexts.Clear();
    }
}
