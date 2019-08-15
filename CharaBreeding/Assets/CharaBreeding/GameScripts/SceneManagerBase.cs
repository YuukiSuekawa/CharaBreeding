using CharaBreeding.GameScripts.Interface;
using CharaBreeding.GameScripts.Room;
using CharaBreeding.GameScripts.UI;
using UnityEngine;

namespace CharaBreeding.GameScripts
{
    public class SceneManagerBase : MonoBehaviour,IMasterObj,IUpdateByFrame
    {
        protected IGameObj[] m_gameObject;

        protected void Awake()
        {
            m_gameObject = InterfaceUtils.FindObjectOfInterfaces<IGameObj>();
        }

        public void UpdateByFrame()
        {
            foreach (var list in m_gameObject)
            {
                if (list is IUpdateByFrame update)
                {
                    update.UpdateByFrame();
                }
            }
        }
    }
}