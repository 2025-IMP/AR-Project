using System.Collections.Generic;
using System.Linq;
using IMP.Core;
using UnityEngine;

namespace IMP.UI
{
    public class UIBallSelection : MonoBehaviour
    {
        [SerializeField] private UIBallCell m_CellPrefab;

        private List<UIBallCell> m_Cells = new List<UIBallCell>();
        public List<UIBallCell> Cells => m_Cells;

        public void Initialize()
        {
            foreach (Transform tr in transform)
            {
                Destroy(tr.gameObject);
            }
            m_Cells.Clear();
        }

        public void Config(Dictionary<BallType, int> ballDict)
        {
            for (int i = 0; i < ballDict.Count; i++)
            {
                BallType ballType = ballDict.ElementAt(i).Key;
                int ballCount = ballDict.ElementAt(i).Value;

                UIBallCell newBallCell = Instantiate(m_CellPrefab);
                newBallCell.transform.SetParent(transform);
                newBallCell.Config(ballType, ballCount);

                int index = i;
                newBallCell.Button.onClick.AddListener(delegate { OnBallCellPressed(index); });

                m_Cells.Add(newBallCell);
            }
        }

        public void Synchronize(Dictionary<BallType, int> ballDict)
        {
            for (int i = 0; i < ballDict.Count; i++)
            {
                BallType ballType = ballDict.ElementAt(i).Key;
                int ballCount = ballDict.ElementAt(i).Value;

                UIBallCell ballCell = m_Cells.Find(cell => cell.BallType == ballType);
                ballCell.SetCount(ballCount);
            }
        }

        public void OnBallCellPressed(int index)
        {
            UIBallCell selectedCell = m_Cells[index];
            if (selectedCell.Selected) return;

            foreach (UIBallCell cell in m_Cells)
            {
                if (cell == selectedCell) continue;
                cell.Deselect();
            }
            selectedCell.Select();
        }
    }
}