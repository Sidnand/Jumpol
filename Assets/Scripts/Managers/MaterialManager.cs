using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialManager {

    public static List<string> materials = new List<string>();

    /// <summary>
    /// Saves data
    /// </summary>
    /// <param name="data">Data to save</param>
    public static void SaveData (string data) {

        if (!PlayerPrefs.HasKey ("Materials")) PlayerPrefs.SetString ("Materials", "");

        string materialsStr = PlayerPrefs.GetString ("Materials");
        materialsStr += data + ",";
        PlayerPrefs.SetString ("Materials", materialsStr);

    }

    /// <summary>
    /// Loads data
    /// </summary>
    public static void LoadData () {

        if (PlayerPrefs.HasKey ("Materials")) {

            string materialsStr = PlayerPrefs.GetString ("Materials");
            materials = materialsStr.Split (',').ToList ();

            ShopManager.Instance.Setup ();

        }

    }

}
