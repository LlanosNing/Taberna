using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSelector : MonoBehaviour
{
    public Dropdown languageDropdown;

    void Start()
    {
        // Llenar el Dropdown con los idiomas disponibles.
        languageDropdown.options.Clear();
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            languageDropdown.options.Add(new Dropdown.OptionData(locale.LocaleName));
        }

        // Escuchar cambios en el Dropdown
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

        // Configura el valor inicial del Dropdown según el idioma actual
        SetInitialLanguage();
    }

    private void SetInitialLanguage()
    {
        var currentLocale = LocalizationSettings.SelectedLocale;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            if (LocalizationSettings.AvailableLocales.Locales[i].Identifier == currentLocale.Identifier)
            {
                languageDropdown.value = i;
                break;
            }
        }
    }

    public void OnLanguageChanged(int index)
    {
        // Cambiar el idioma al seleccionar una opción del Dropdown
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        LocalizationSettings.SelectedLocale = selectedLocale;
    }
}
