using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Localization
{
	private const string PATH = "Localization/";

	private static Localization instance;
	public static Localization Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new Localization();
			}
			return instance;
		}
	}
	

	private Dictionary<string, string> texts;

	private Localization ()
	{
		string path = OptionsManager.Instance.GetLanguage();

		texts = LoadFromText(path);
	}

	/// <summary>
	/// Loads a new Localization XML language
	/// </summary>
	/// <param name="path">Language</param>
	public void SetLanguage (string path)
	{
		texts = LoadFromText(path);
	}

	/// <summary>
	/// Gets a text identified by string ID
	/// </summary>
	/// <param name="textID">Text identifier</param>
	/// <returns>A string</returns>
	public string GetText (string textID)
	{
		string s = null;
		texts.TryGetValue(textID, out s);
		return s;
	}

	private Dictionary<string, string> LoadFromText (string path)
	{
		TextAsset text = Resources.Load(PATH + path) as TextAsset;
		Dictionary<string, string> textDic = new Dictionary<string, string>();

		XmlSerializer serializer = new XmlSerializer(typeof(LocalizatonTexts));
		LocalizatonTexts textArray = serializer.Deserialize(new StringReader(text.text)) as LocalizatonTexts;

		foreach (LocalizationText e in textArray.texts)
		{
			textDic.Add(e.ID, e.text);
		}

		return textDic;
	}
}

/// <summary>
/// Container to deserialize all LocalizationText in localization language XML
/// </summary>
[Serializable]
[XmlRoot("Root")]
public class LocalizatonTexts
{
	/// <summary>
	/// Localizatio texts in localization language XML
	/// </summary>
	[XmlArray("Texts")]
	[XmlArrayItem("Text", typeof(LocalizationText))]
	public LocalizationText[] texts;
}

/// <summary>
/// Container to deserialize a single localization text
/// </summary>
[Serializable]
[XmlRoot("Text")]
public class LocalizationText
{
	/// <summary>
	/// Identifier
	/// </summary>
	[XmlAttribute("id")]
	public string ID;

	/// <summary>
	/// Inner text
	/// </summary>
	[XmlText]
	public string text;
}
