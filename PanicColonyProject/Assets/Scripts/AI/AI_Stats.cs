using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [System.Serializable]
    public class AI_Stats
    {
        /// <summary>
        /// The maximum health value of this entity
        /// </summary>
        [Tooltip("The amount of health this entity starts with")]
        public int initialHealth = 100;

        /// <summary>
        /// The current health value of this entity
        /// </summary>
        [HideInInspector]
        public int currentHealth = 100;


    }
}
