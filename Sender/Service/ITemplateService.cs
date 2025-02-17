using System;
using Sender.Models;
using System.Threading.Tasks;

namespace Sender.Service
{
    public interface ITemplateService
    {
        public TemplateMessageViewModel ToCreateMessageTemplateViewModel(TemplateMessageViewModel templateMessageViewModel);
    }
}