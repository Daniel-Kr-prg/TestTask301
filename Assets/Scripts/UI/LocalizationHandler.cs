using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalizationHandler : MonoBehaviour
{
}

public class Localization
{
    public string languageName;
    public Sprite preview;

    public Dictionary<string, string> localization;

    public string FromLocalizationString(string localizationString)
    {
        if (localization == null)
        {
            return localizationString;
        }

        if (localizationString.First() == '%' && localizationString.Last() == '%')
        {
            string stringName = localizationString.Substring(1, localizationString.Length - 2);

            if (localization.ContainsKey(stringName))
            {
                return localization[stringName];
            }
            else
            {
                return localizationString;
            }
        }

        return localizationString;
    }
}
