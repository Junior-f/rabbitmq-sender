using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Sender.Models;

namespace Sender.Service
{
    public class TemplateService : ITemplateService
    {

        public TemplateMessageViewModel ToCreateMessageTemplateViewModel(TemplateMessageViewModel templateMessageViewModel)
        {

            var templateData = MountTemplateDataByTemplateName(
                templateMessageViewModel.TemplateData?.Body ?? new Dictionary<string, string>(),
                templateMessageViewModel.TemplateData?.Buttons ?? new Dictionary<string, Button>(),
                templateMessageViewModel.TemplateData?.Header
            );


            return new TemplateMessageViewModel
            {
                PhoneNumberTo = templateMessageViewModel.PhoneNumberTo,
                Language = templateMessageViewModel.Language,
                TemplateName = templateMessageViewModel.TemplateName,
                TemplateData = templateData
            };

        }

        private TemplateData MountTemplateDataByTemplateName(Dictionary<string, string> additionalBodyData, Dictionary<string, Button> additionalButtonData, Header additionalHeader)
        {



            var bodyData = new Dictionary<string, string>();
            var buttonData = new Dictionary<string, Button>();


            if (additionalBodyData != null && additionalBodyData.Count > 0)
            {
                foreach (var n in additionalBodyData)
                {
                    bodyData.Add(n.Key, n.Value);
                }
            }

            if (additionalButtonData != null && additionalButtonData.Count > 0)
            {
                foreach (var n in additionalButtonData)
                {
                    buttonData.Add(n.Key, n.Value);
                }
            }

            var mountedTemplateData = new TemplateData();

            if (bodyData.Count > 0)
                mountedTemplateData.Body = bodyData;

            if (buttonData.Count > 0)
                mountedTemplateData.Buttons = buttonData;

            if (additionalHeader != null && !string.IsNullOrEmpty(additionalHeader.Type) && !string.IsNullOrEmpty(additionalHeader.Placeholder))
            {
                mountedTemplateData.Header = additionalHeader;
            }

            return mountedTemplateData;
        }

    }
}
