/// Owner: Dongjin Kuk
/// Description: The structure is a set of the blocks. It will be spawned in the game scene.

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

        [SerializeField]
        private Transform m_StarRoot;
        public Transform StarRoot => m_StarRoot;
        private List<Star> m_Stars = new List<Star>();

        public void Initialize()
        {
            m_Blocks = m_BlockRoot.GetComponentsInChildren<Block>().ToList();
            m_Blocks.ForEach(block => block.Initialize());

            m_Stars = m_StarRoot.GetComponentsInChildren<Star>().ToList();
        }
    }
}
