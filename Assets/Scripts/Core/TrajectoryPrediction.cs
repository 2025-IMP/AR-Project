/// Owner: Dongjin Kuk
/// Description: This script simulates the trajectory of the ball.
/// By creating virtual physics scene, we can draw the ball's trajectory by LineRenderer.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMP.Core
{
    public class TrajectoryPrediction : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_LineRenderer;
        [SerializeField] private int m_MaxPhysicsFrameIterations = 100;
        //[SerializeField] private Transform m_ColliderRoot;

        private Scene m_SimulationScene;
        private PhysicsScene m_PhysicsScene;
        private Dictionary<Transform, Transform> m_Gobjs = new Dictionary<Transform, Transform>();

        private void Start()
        {
            CreatePhysicsScene();
        }

        private void CreatePhysicsScene()
        {
            m_SimulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
            m_PhysicsScene = m_SimulationScene.GetPhysicsScene();

            //foreach (Transform tr in m_ColliderRoot)
            //{
            //    var gobj = Instantiate(tr.gameObject, tr.position, tr.rotation);
            //    gobj.GetComponent<Renderer>().enabled = false;
            //    SceneManager.MoveGameObjectToScene(gobj, m_SimulationScene);
            //    if (!gobj.isStatic)
            //    {
            //        m_Gobjs.Add(tr, gobj.transform);
            //    }
            //}
        }

        public void StopSimulation()
        {
            m_LineRenderer.positionCount = 0;
        }

        public void Simulate(Ball ballPrefab, Vector3 pos, Vector3 velocity)
        {
            var ball = Instantiate(ballPrefab, pos, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(ball.gameObject, m_SimulationScene);

            ball.Simulate(velocity);

            m_LineRenderer.positionCount = m_MaxPhysicsFrameIterations;

            for (int i = 0; i < m_MaxPhysicsFrameIterations; i++)
            {
                m_PhysicsScene.Simulate(Time.fixedDeltaTime);
                m_LineRenderer.SetPosition(i, ball.transform.position);
            }

            Destroy(ball.gameObject);
        }
    }
}