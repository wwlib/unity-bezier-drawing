using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameData {
    public string polygon;
}                                                     // The System.IO namespace contains functions related to loading and saving files

public class FileUtils : MonoBehaviour
{
    public string gameDataFilename = "polygon.json";

    void Start()
    {
      GameData gameData;
      gameData = LoadGameData(gameDataFilename);
      Debug.Log(gameData);
      writeJSONFile();
    }

    public GameData LoadGameData(string filename)
    {
      // Path.Combine combines strings into a file path
      // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
      string filePath = Path.Combine(Application.streamingAssetsPath, filename);
      GameData loadedData = null;

      if(File.Exists(filePath))
      {
        // Read the json from the file into a string
        string dataAsJson = File.ReadAllText(filePath);
        Debug.Log(dataAsJson);
        // Pass the json to JsonUtility, and tell it to create a GameData object from it
        loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

        // Retrieve the allRoundData property of loadedData
        string polygonString = loadedData.polygon;
        Debug.Log(polygonString);
      }
      else
      {
        Debug.LogError("Cannot load game data!");
      }

      return loadedData;
    }

    public void writeJSONFile() {
        string filename = "new-polygon.json";
        string filePath = Path.Combine(Application.streamingAssetsPath, filename);
        GameData gameData = new GameData();
        gameData.polygon = "A new polygon";
        string str = JsonUtility.ToJson(gameData);
        using (FileStream fs = new FileStream(filePath, FileMode.Create)){
            using (StreamWriter writer = new StreamWriter(fs)){
                writer.Write(str);
            }
        }
    }

    public void writeBinaryFile() {
      byte[] save = null;
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
      bf.Serialize(file, save);
      file.Close();
    }
 }
