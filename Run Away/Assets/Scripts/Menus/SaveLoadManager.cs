using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoadManager{

	public static void SaveGame(GameManager gm){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.dataPath + "/Saves/scene.sav", FileMode.Create);

		SceneData data = new SceneData (gm);

		bf.Serialize(stream, data);
		stream.Close ();
	}

	public static int[] LoadGame(){
		if (File.Exists (Application.dataPath + "/Saves/scene.sav")) {
			Debug.Log("El archivo existe: " + Application.persistentDataPath);
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.dataPath + "/Saves/scene.sav", FileMode.Open);

			SceneData data = bf.Deserialize (stream) as SceneData;

			stream.Close ();
			return data.stats;
		} else {
			Debug.LogError ("File does not exist.");
			return new int[2];
		}
	}

}


[Serializable]
public class SceneData {

	public int[] stats;

	public SceneData(GameManager gm){
		stats = new int[2];
		stats[0] = gm.nivel;
		stats[1] = gm.drogaConsumida;
		//stats[2];
	}

}