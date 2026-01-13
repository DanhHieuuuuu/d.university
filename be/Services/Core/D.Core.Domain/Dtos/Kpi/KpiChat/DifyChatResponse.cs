using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Kpi.KpiChat
{
    public class DifyChatResponse
        {
            [JsonPropertyName("answer")]
            public string Answer { get; set; }

            [JsonPropertyName("conversation_id")]
            public string ConversationId { get; set; }

            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }
    }
}
