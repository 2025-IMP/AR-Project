/// Owner: Dongjin Kuk
/// Description: It manages the stage datas, and saves the current stage data.

using UnityEngine;

namespace IMP.Core
{
    public class StageManager : Singleton<StageManager>
    {
        [SerializeField] private StageData[] m_StageDatas;

        private StageData m_CurrStageData;
        public StageData CurrStageData => m_CurrStageData;

        public void SetStageData(int index)
        {
            m_CurrStageData = m_StageDatas[index];
        }
        public void SetStageData(StageData sdata)
        {
            m_CurrStageData = sdata;
        }

        public StageData GetStageData(int index)
        {
            return m_StageDatas[index];
        }
    }
}
