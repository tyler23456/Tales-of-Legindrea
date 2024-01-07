using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Independent
{
    public class LargeDestruction : MonoBehaviour, IEnabler
    {
        IFactory factory;

        [SerializeField] CharacterController characterController;
        [SerializeField] MeshFilter meshFilter;
        [SerializeField] float explosionRateInSeconds = 0.5f;
        [SerializeField] float explosionCount = 10f;
        [SerializeField] float fallSpeedInMetersPerSecond = 9.8f;

        float startTime = 0f;
        int explosionIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
            characterController.enabled = true;
            startTime = Time.time;
            explosionIndex = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - startTime > explosionRateInSeconds && explosionIndex < explosionCount)
            {
                //create an explosion
                var vertices = meshFilter.mesh.vertices;
                int vertIndex = Random.Range(0, vertices.Length);
                Vector3 worldPt = transform.TransformPoint(vertices[vertIndex]);

                Destroy(Instantiate(factory.props["Explosion"], worldPt, Quaternion.identity), 10f);
                characterController.Move(Vector3.down * fallSpeedInMetersPerSecond * Time.deltaTime);

                startTime = Time.time;
                explosionIndex++;
            }
        }
    }
}