using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.LanguageChoose
{
    public class DeathMessagesTranslations : MonoBehaviour
    {
        [SerializeField] private List<DeathMessagePair> deathMessages = new();

        public string GetRuMessage(string enMessage) => deathMessages.Find(deathMessage => deathMessage.EnDeathMessage == enMessage).RuDeathMessage;

        [Serializable]
        private class DeathMessagePair
        {
            [field: SerializeField] public string EnDeathMessage { get; private set; }
            [field: SerializeField] public string RuDeathMessage { get; private set; }
        }
    }
}