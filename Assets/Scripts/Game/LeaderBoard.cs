using UnityEngine;

namespace MiniGame.PuzzleGeometri
{
    public class LeaderBoard : MonoBehaviour
    {
        [SerializeField] private GameObject _parentContent;

        private void Start()
        {
            SaveLoadData.Load();

            for (int i = 0; i < SaveLoadData.leaderBoard.leaderBoardData.Count; i++)
            {
                var obj = Resources.Load("TextLeaderBoard");
                if (obj != null)
                {
                    GameObject textObj = Instantiate(obj) as GameObject;
                    textObj.transform.SetParent(_parentContent.transform);
                    textObj.transform.localScale    = Vector3.one;
                    textObj.transform.localPosition = Vector3.zero;

                    UnityEngine.UI.Text text = textObj.GetComponent<UnityEngine.UI.Text>();
                    if (text != null)
                    {
                        text.text = (i + 1) + ". " + Meta.Tools.StopWatch.GetRuntimeText(SaveLoadData.leaderBoard.leaderBoardData[i]);
                    }
                }
            }
        }

        
    }
}