using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using Monstruos;
using Guardado;

public static class SaveLoadManager{

	public static void SaveGame(GameManager gm){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Path.GetFullPath(".") + "/scene.sav", FileMode.Create);

		SceneData data = new SceneData (gm);

		bf.Serialize(stream, data);
		stream.Close ();
	}

	public static SceneData LoadGame(){
		if (File.Exists (Path.GetFullPath(".") + "/scene.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Path.GetFullPath(".") + "/scene.sav", FileMode.Open);

			SceneData data = bf.Deserialize (stream) as SceneData;

			stream.Close ();
			return data;
		} else {
			Debug.LogError ("File does not exist.");
			return new SceneData();
		}
	}

}

namespace Guardado {
    [Serializable]
    public class SceneData
    {

        public string nivel;
        public int[] monstruosMuertos,
            monstruosHuidos,
            monstruosIgnorados;
        public int numeroDeMuertes = 0, numeroDeDroga;

        public SceneData()
        {
            nivel = "NivelTutorial";
            int n = Enum.GetValues(typeof(TipoMonstruo)).Length;
            monstruosMuertos = new int[n];
            monstruosIgnorados = new int[n];
            monstruosHuidos = new int[n];
            numeroDeMuertes = 0;
            numeroDeDroga = 0;
        }

        public SceneData(GameManager gm)
        {

            nivel = gm.Nivel;
            monstruosMuertos = gm.monstruosMuertos;
            monstruosIgnorados = gm.monstruosIgnorados;
            monstruosHuidos = gm.monstruosHuidos;
            numeroDeMuertes = gm.NumeroDeMuertes;
            numeroDeDroga = gm.drogaConsumida;

        }

    }
}