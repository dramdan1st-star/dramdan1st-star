using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace MiniGame.PuzzleGeometri
{
    [System.Serializable]
    public class DataLeaderBoard
    {
        public List<float> leaderBoardData = new List<float>();
    }

    public class SaveLoadData : MonoBehaviour
    {
        public static DataLeaderBoard leaderBoard = new DataLeaderBoard();

        public static void Save(float newTime)
        {
            Load();

            leaderBoard.leaderBoardData.Add(newTime);

            string path = Application.streamingAssetsPath + "/dataLeaderBoard.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            string leaderBoardText = JsonUtility.ToJson(leaderBoard);
            File.WriteAllText(path, leaderBoardText);
        }

        public static void Load()
        {
            string path = Application.streamingAssetsPath + "/dataLeaderBoard.json";
            if (File.Exists(path))
            {
                string leaderBoardDataText = File.ReadAllText(path);
                leaderBoard = JsonUtility.FromJson<DataLeaderBoard>(leaderBoardDataText);
            }
        }
    }
}