using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using ArabicSupport;

[RequireComponent(typeof(TextAlignment))]
public class TextMeshProArGUI : TextMeshProUGUI
{
    private StringBuilder ResultText = new StringBuilder();

    private TextAlignmentOptions AlignmentOptions;
    protected override void Start()
    {
        AlignmentOptions = GetComponent<TextAlignment>().textAlignment;
    }

    [ContextMenu("Convert To Arabic")]
    public void ConvertToArabic()
    {

        text = ArabicFixer.Fix(text);


        isRightToLeftText = true;

        alignment = GetComponent<TextAlignment>() != null ? GetComponent<TextAlignment>().textAlignment : TextAlignmentOptions.Center;


        text = RevrseText(text);
    }

    public string RevrseText(string txt)
    {
        ResultText.Clear();
        char[] _text = txt.ToCharArray();


        for (int i = txt.Length - 1; i >= 0; i--)
        {
            ResultText.Append(_text[i]);
        }

        return ResultText.ToString();
    }
}
