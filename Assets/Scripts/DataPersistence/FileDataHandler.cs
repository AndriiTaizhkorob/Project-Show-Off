using UnityEngine;
using System;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured while loading " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e) 
        {
            Debug.LogError("Error occured while saving " + fullPath + "\n" + e);
        }
    }

    public void Delete()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            if (File.Exists(fullPath))
            {
                //Directory.Delete(Path.GetDirectoryName(fullPath), true);
                File.Delete(fullPath);
            }
            else
            {
                Debug.Log("No file to delete.");
                return;
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Failed to delete the save file at path: " + fullPath + "\n" + e);
        }
    }
}
