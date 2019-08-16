using CharaBreeding;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace CharaBreeding.EditorScript
{
    public class CreateCharaUmiushis : MonoBehaviour
    {
      
        private const string masterFilePath = "Assets\\CharaBreeding\\MasterData\\";
        private const string masterFileName = "CharaUmiushis.json";
       
        [MenuItem("DataCreate/ScriptableObject/Umiushi Master")]
        static void CreateAssetInstance()
        {
            string fileData = File.ReadAllText(masterFilePath + masterFileName);
            Debug.Log("file " + fileData);
            CharaMasters charaMaster = JsonUtility.FromJson<CharaMasters>(fileData);
            foreach (var chara in charaMaster.charaList)
            {
                if (chara == null)
                {
                    Debug.LogError("not data");
                }
                else
                {
                    var umiushi = ScriptableObject.CreateInstance<ScriptableCharaMaster>();
                    umiushi.InitMaster(chara);
                    AssetDatabase.CreateAsset(umiushi, "Assets/CharaBreeding/Test/" + chara.assetName + ".asset");
                    AssetDatabase.Refresh();
                }
            }

            Debug.Log("Create ScriptableObjectMaster end");


        }
        
    }
}