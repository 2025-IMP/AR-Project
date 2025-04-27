using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IMP.Core
{
    public class Structure : MonoBehaviour
    {
        [SerializeField]
        private Transform m_BlockRoot;
        private List<Block> m_Blocks = new List<Block>();

        public void Initialize()
        {
            m_Blocks = m_BlockRoot.GetComponentsInChildren<Block>().ToList();
            m_Blocks.ForEach(block => block.Initialize());
        }
    }
}
