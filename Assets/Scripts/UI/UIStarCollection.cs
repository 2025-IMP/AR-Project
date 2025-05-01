using System.Collections.Generic;
using UnityEngine;

namespace IMP.UI
{
    public class UIStarCollection : MonoBehaviour
    {
        [SerializeField] private UIStar m_StarPrefab;
        
        private List<UIStar> m_Stars = new List<UIStar>();

        public void Initialize()
        {
            foreach (Transform tr in transform)
            {
                Destroy(tr.gameObject);
            }
            m_Stars.Clear();
        }

        public void Config(int starCount)
        {
            for (int i = 0; i < starCount; i++)
            {
                UIStar newStar = Instantiate(m_StarPrefab);
                newStar.transform.SetParent(transform);
                newStar.SetActive(false);
                
                m_Stars.Add(newStar);
            }
        }

        public void SetStarsActive(int starCount)
        {
            for (int i = 0; i < starCount; i++)
            {
                m_Stars[i].SetActive(true);
            }
            for (int i = starCount + 1; i < m_Stars.Count; i++)
            {
                m_Stars[i].SetActive(false);
            }
        }
    }
}
