// WeakBlock.cs
using UnityEngine;

namespace IMP.Core
{
    public class WeakBlock : Block
    {
        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                base.Initialize();
            }
        }
    }
}
