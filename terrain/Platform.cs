using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace jumpAndLearn.terrain
{
    [RequireComponent(typeof(Collider))]
    public class Platform : MonoBehaviour
    {
        protected static List<Platform> platforms = new List<Platform>();
        public static List<Platform> Platforms { get { return platforms; } }

        public static Platform CurrentPlatform { get; private set; }

        public int group;
        protected bool isUsed;

        public static List<Platform> GetPlatformsByGroup(int group)
        {
            List<Platform> platformGroup = new List<Platform>();
            foreach (Platform p in platforms)
            {
                if (p.group == group)
                    platformGroup.Add(p);
            }
            return platformGroup;
        }

        protected virtual void Start()
        {
            platforms.Add(this);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isUsed)
            {
                isUsed = true;
                CurrentPlatform = this;
                DestroyOldPlatforms();
            }
        }

        private void DestroyOldPlatforms()
        {
            List<Platform> platformToRemove = new List<Platform>();

            foreach (Platform p in platforms)
            {
                if (p.group <= this.group && p != this)
                {
                    platformToRemove.Add(p);
                }
            }
            foreach (Platform p in platformToRemove)
            {
                if(p != null)
                {
                    //platforms.Remove(p);
                    p.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }

        public void DestroyNextPlatforms()
        {
            List<Platform> platformToRemove = GetPlatformsByGroup(group + 1);
            foreach (Platform p in platformToRemove)
            {
                if(p != null)
                {
                    platforms.Remove(p);
                    p.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }
}